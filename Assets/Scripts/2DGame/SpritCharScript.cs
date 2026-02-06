using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using System;
public class SpritCharScript : MonoBehaviour, IDamagable
{
    InputAction move;
    public float movementSpeed;
    public Vector2 movementValue;
    public event Action<Vector2> OnMove;

    //stats
    public int HP, MaxHP, ATK, DEF;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        move.performed += GetMovementVector;
        move.canceled += GetMovementVector;
        HP = MaxHP;
    }
    public void GetMovementVector(InputAction.CallbackContext c)
    {
        movementValue = c.ReadValue<Vector2>();
        OnMove?.Invoke(movementValue);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(movementValue.x, movementValue.y, 0) * movementSpeed * Time.deltaTime);
    }
    public void TakeDamage(int incomingDamage)
    {
        int damageTaken = incomingDamage - DEF;
        damageTaken = Mathf.Clamp(damageTaken, 0, 9999);
        HP -= damageTaken;
    }
}
