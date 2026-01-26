using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class VehicleController : MonoBehaviour
{
    public InputAction move, accelerate, brake;

    public float accelerationValue, brakeValue, steerValue, decelerationValue;
    const float ACCELERATION_FACTOR = 5.0f, BRAKE_FACTOR = 5.0f, STEER_FACTOR = 10.0f;

    public float currentSpeed, maxSpeed, accelerationMulti, steerMulti;

    Rigidbody myRigidBody;

    public GameState gameState;

    public GameObject sphere, cube, cone;

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
        if(gameState == null)
        {
            gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        }
        UpdateVehicleLooks();
        
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

        currentSpeed = Mathf.Sqrt(Mathf.Pow(myRigidBody.linearVelocity.x, 2) + Mathf.Pow(myRigidBody.linearVelocity.z, 2));

        if (currentSpeed < maxSpeed)
        {   
            myRigidBody.AddForce(transform.forward * (accelerationValue - brakeValue) * Time.deltaTime * accelerationMulti, ForceMode.Force);

        }


        if (Mathf.Abs(currentSpeed) > 0.01f)
        {
            float steer = steerValue * (currentSpeed / maxSpeed);

            transform.Rotate(0, steer * Time.deltaTime * steerMulti, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    public void OnEnable()
    {
        move.Enable();
        accelerate.Enable();
        brake.Enable();
    }
    public void OnDisable()
    {
        move.Disable();
        accelerate.Disable();
        brake.Disable();
    }
    public void UpdateVehicleLooks()
    {
        sphere.SetActive(false);
        cube.SetActive(false);
        cone.SetActive(false);
        switch (gameState.currentProfile.vehicleType)
        {
            case 0:
                {
                    sphere.SetActive(true);
                    sphere.GetComponent<MeshRenderer>().material.color = new Color(gameState.currentProfile.vehicleColorR / 255f, gameState.currentProfile.vehicleColorG / 255f, gameState.currentProfile.vehicleColorB / 255f);
                    break;
                }
            case 1:
                {
                    cube.SetActive(true);
                    cube.GetComponent<MeshRenderer>().material.color = new Color(gameState.currentProfile.vehicleColorR / 255f, gameState.currentProfile.vehicleColorG / 255f, gameState.currentProfile.vehicleColorB / 255f);
                    break;
                }
            case 2:
                {
                    cone.SetActive(true);
                    cone.GetComponent<MeshRenderer>().material.color = new Color(gameState.currentProfile.vehicleColorR / 255f, gameState.currentProfile.vehicleColorG / 255f, gameState.currentProfile.vehicleColorB / 255f);
                    break;
                }
        }
    }
}
