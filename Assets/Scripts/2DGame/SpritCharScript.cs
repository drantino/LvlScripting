using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
public class SpritCharScript : MonoBehaviour, IDamagable
{
    InputAction move, attack, interact;
    public float movementSpeed, attackCD, invincibleDuration, invincibleTill;
    public Vector2 movementValue;
    public event Action<Vector2> OnMove;
    private SpritCharAnimationScript animationScript;
    public GameObject weapon;
    private bool attacking, dead, invincible;
    public List<IInteractable> interactableObjects;
    private Animator myAnimator;
    //stats
    public int HP, MaxHP;
    public int ATK
    {
        get
        {
            int atk = 5 + EquipmentManager.instance.equipmentATK;
            return atk;
        }
    }
    public int DEF
    {
        get
        {          
            int def = EquipmentManager.instance.equipmentDEF;
            return def;
        }
    }
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
        MainUIScript.instance.player = this.gameObject;
        MainUIScript.instance.UpdateCharHud(); 
        dead = false;
        interactableObjects = new List<IInteractable>();
        enabled = false;
        enabled = true;
        myAnimator = GetComponent<Animator>();
    }
    public void GetMovementVector(InputAction.CallbackContext c)
    {
        movementValue = c.ReadValue<Vector2>();
        OnMove?.Invoke(movementValue);
    }
    // Update is called once per frame
    void Update()
    {
        if (!attacking && attack.WasCompletedThisFrame() && !dead)
        {
            Attack();
        }
        if (interact.WasPressedThisFrame() && interactableObjects.Count > 0)
        {
            if(interactableObjects[0] == null)
            {
                interactableObjects.RemoveAt(0);
            }
            else
            {
                interactableObjects[0].Interact(gameObject);
            }  
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(movementValue.x, movementValue.y, 0) * movementSpeed * Time.deltaTime);

        if(invincible && invincibleTill < Time.time)
        {
            myAnimator.SetBool("Invincible", false);
            invincible = false;
        }
    }
    private void Attack()
    {
        switch (animationScript.currentState)
        {
            case PlayerAnimationState.Idle_Down:
            case PlayerAnimationState.Walk_Down:
                {
                    weapon.transform.localPosition = new Vector3(0, -0.25f, 0);
                    weapon.transform.localRotation = Quaternion.Euler(0, 0, 180);
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
        SoundEffectManager.Instance.PlaySoundByName("PlayerSwordSwing");
    }
    public void TakeDamage(int incomingDamage)
    {
        if (!invincible)
        {
            int damageTaken = incomingDamage - DEF;
            damageTaken = Mathf.Clamp(damageTaken, 0, 9999);
            HP -= damageTaken;
            MainUIScript.instance.UpdateCharHud();
            SoundEffectManager.Instance.PlaySoundByName("PlayerTakeDamage");
            if (HP <= 0)
            {
                TwoDGameState.Instance.PlayerKilled();
                move.performed -= GetMovementVector;
                move.canceled -= GetMovementVector;
                dead = true;
                StartCoroutine(DeathAnimation());
            }
            invincibleTill = Time.time + invincibleDuration;
            myAnimator.SetBool("Invincible", true);
            invincible = true;
        }
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
    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(1);
        transform.Rotate(0, 0, -90);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable;
        collision.TryGetComponent<IInteractable>(out interactable);
        if(!interactableObjects.Contains(interactable))
        {
            interactableObjects.Add(interactable);
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable newCollision;
        collision.TryGetComponent<IInteractable>(out newCollision);
        if (interactableObjects.Contains(newCollision))
        {
            interactableObjects.Remove(newCollision);
        }
    }
    public void PlayerRest()
    {
        HP = MaxHP;
        MainUIScript.instance.UpdateCharHud();
    }
    private void OnDestroy()
    {
        move.performed -= GetMovementVector;
        move.canceled -= GetMovementVector;
    }
    private void OnEnable()
    {
        if(move != null)
        {
            move.Enable();
        }
        if(attack!=null)
        {
            attack.Enable();
        }
        if (interact != null)
        {
            interact.Enable();
        }

    }
    private void OnDisable()
    {
        move.Disable();
        attack.Disable();
        interact.Disable();
    }
}
