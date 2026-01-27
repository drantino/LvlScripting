using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SpritCharScript : MonoBehaviour
{
    InputAction move;
    public float movementSpeed;
    public Vector2 movementValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        move.performed += GetMovementVector;
        move.canceled += GetMovementVector;
    }
    public void GetMovementVector(InputAction.CallbackContext c)
    {
        movementValue = c.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(movementValue.x, movementValue.y, 0) * movementSpeed * Time.deltaTime);
    }

}
