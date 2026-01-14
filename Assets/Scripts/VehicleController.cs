using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class VehicleController : MonoBehaviour
{
    public InputAction move, accelerate, brake;

    public float accelerationValue, brakeValue, steerValue, decelerationValue;
    const float ACCELERATION_FACTOR = 5.0f, BRAKE_FACTOR = 5.0f, STEER_FACTOR = 10.0f;

    public float currentSpeed, maxSpeed;

    Rigidbody myRigidBody;



    private void Start()
    {
        move.Enable();
        accelerate.Enable();
        brake.Enable();
        move = InputSystem.actions.FindAction("Move");
        accelerate = InputSystem.actions.FindAction("Jump");
        brake = InputSystem.actions.FindAction("Interact");

        accelerate.performed += AccelrateInput;
        accelerate.canceled += AccelrateInput;
        move.performed += MoveInput;
        move.canceled += MoveInput;
        brake.performed += BrakeInput;
        brake.canceled += BrakeInput;

        myRigidBody = GetComponent<Rigidbody>();
    }

    public void AccelrateInput(InputAction.CallbackContext c)
    {
        accelerationValue = c.ReadValue<float>() * ACCELERATION_FACTOR;
    }
    public void MoveInput(InputAction.CallbackContext c)
    {
        steerValue = c.ReadValue<Vector2>().x * STEER_FACTOR;
    }
    public void BrakeInput(InputAction.CallbackContext c)
    {
        brakeValue = c.ReadValue<float>() * BRAKE_FACTOR;
    }

    private void FixedUpdate()
    {
        //currentSpeed += (accelerationValue - decelerationValue - brakeValue) * Time.deltaTime;
        //currentSpeed = Mathf.Clamp(currentSpeed,0,maxSpeed);

        currentSpeed = Mathf.Sqrt(Mathf.Pow( myRigidBody.linearVelocity.x, 2) + Mathf.Pow(myRigidBody.linearVelocity.z, 2));
         
        if(currentSpeed < maxSpeed)
        {
            myRigidBody.AddForce( transform.forward * (accelerationValue - brakeValue) * Time.deltaTime, ForceMode.Force);
            Debug.Log(myRigidBody.linearVelocity);
        }


        if(Mathf.Abs(currentSpeed) > 0.01f)
        {
            float steer = steerValue * Mathf.Sin(currentSpeed);
            transform.Rotate(0,steer * Time.deltaTime, 0);
        }



        //Vector3 tmp = transform.forward * currentSpeed;
        //tmp.y = myRigidBody.linearVelocity.y;
        //myRigidBody.linearVelocity = tmp;
    }
}
