using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using System;
using System.Collections;
public class SpritCharScript : MonoBehaviour, IDamagable
{
    InputAction move, attack, interact;
    public float movementSpeed, attackCD;
    public Vector2 movementValue;
    public event Action<Vector2> OnMove;
    private SpritCharAnimationScript animationScript;
    public GameObject weapon;
    private bool attacking;
    public IInteractable interactableObject;
    //stats
    public int HP, MaxHP, ATK, DEF;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        attack = InputSystem.actions.FindAction("Attack");
        interact = InputSystem.actions.FindAction("Interact");
        move.performed += GetMovementVector;
        move.canceled += GetMovementVector;
        animationScript = GetComponent<SpritCharAnimationScript>();
        attacking = false; 
        weapon.SetActive(false);
        MainUIScript.instance.UpdateCharHud(gameObject);
    }
    public void GetMovementVector(InputAction.CallbackContext c)
    {
        movementValue = c.ReadValue<Vector2>();
        OnMove?.Invoke(movementValue);
    }
    // Update is called once per frame
    void Update()
    {
        if(!attacking && attack.WasCompletedThisFrame())
        {
            Attack();
        }
        if(interact.WasPressedThisFrame() && interactableObject != null)
        {
            interactableObject.Interact(gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(movementValue.x, movementValue.y, 0) * movementSpeed * Time.deltaTime);
    }
    private void Attack()
    {
        switch (animationScript.currentState)
        {
            case PlayerAnimationState.Idle_Down:
            case PlayerAnimationState.Walk_Down:
                {
                    weapon.transform.localPosition = new Vector3(0,-0.25f,0);
                    weapon.transform.localRotation = Quaternion.Euler(0,0,180);
                    weapon.SetActive(true);
                    StartCoroutine(ResetAttack());
                    break;
                }
            case PlayerAnimationState.Idle_Up:
            case PlayerAnimationState.Walk_Up:
                {
                    weapon.transform.localPosition = new Vector3(0, 0.2f, 0);
                    weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    weapon.SetActive(true);
                    StartCoroutine(ResetAttack());
                    break;
                }
            case PlayerAnimationState.Idle_Right:
            case PlayerAnimationState.Walk_Right:
                {
                    weapon.transform.localPosition = new Vector3(0.2f, -0.075f, 0);
                    weapon.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    weapon.SetActive(true);
                    StartCoroutine(ResetAttack());
                    break;
                }
            case PlayerAnimationState.Idle_Left:
            case PlayerAnimationState.Walk_Left:
                {
                    weapon.transform.localPosition = new Vector3(-0.2f, -0.075f, 0);
                    weapon.transform.localRotation = Quaternion.Euler(0, 0, 90);
                    weapon.SetActive(true);
                    StartCoroutine(ResetAttack());
                    break;
                }
            default:
                {

                    break;
                }
        }
    }
    public void TakeDamage(int incomingDamage)
    {
        int damageTaken = incomingDamage - DEF;
        damageTaken = Mathf.Clamp(damageTaken, 0, 9999);
        HP -= damageTaken;
        MainUIScript.instance.UpdateCharHud(gameObject);
    }
    public void SendDMG(Collider2D collider)
    {
        collider.GetComponent<IDamagable>().TakeDamage(ATK);
    }
    IEnumerator ResetAttack()
    {
        attacking = true;
        yield return new WaitForSeconds(attackCD);
        weapon.SetActive(false);
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<IInteractable>(out interactableObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable newCollision;
        collision.TryGetComponent<IInteractable>(out newCollision);
        if (newCollision == interactableObject)
        {
            interactableObject = null;
        }
    }
    public void PlayerRest()
    {
        HP = MaxHP;
        MainUIScript.instance.UpdateCharHud(gameObject);
    }
    private void OnDestroy()
    {
        move.performed -= GetMovementVector;
        move.canceled -= GetMovementVector;
    }
}
