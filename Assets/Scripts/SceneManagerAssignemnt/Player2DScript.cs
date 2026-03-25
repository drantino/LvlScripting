using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DScript : MonoBehaviour
{
    InputAction move;
    private Rigidbody2D myRigidBody2D;
    public float movementMultiplyer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        myRigidBody2D.AddForce(move.ReadValue<Vector2>() *  movementMultiplyer * Time.deltaTime);
    }
}
