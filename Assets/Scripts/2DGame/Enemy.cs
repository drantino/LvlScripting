using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AIMovement))]
public abstract class Enemy : MonoBehaviour
{
    public string enemyName;
    public int HP;
    public int ATK;
    public int DEF;

    public float attackDelay;

    public CircleOverlap sightLine;
    public CircleOverlap attackRange;

    public Vector2 playerPosition;

    private Coroutine attackCoroutine;

    private Vector2 nextPosition;
    public Vector2 patrolRange, startingPosition;
    private AIMovement aiMovement;

    private bool patroling;

    public abstract void Attack();
    public abstract void TakeDamage(float dmg_);
    public abstract void Die();
    public void Pursue()
    {
        aiMovement.InitializeMovement(playerPosition);
    }
    private void Awake()
    {
        sightLine.OnOverLap += SetPlayerPosition;
        attackRange.OnOverLap += SetPlayerPosition;
        aiMovement = GetComponent<AIMovement>();
        aiMovement.OnArrive += Patrol;
        startingPosition = transform.position;
    }

    private void Update()
    {
        
        if (attackRange.CircleOverLapCheck())
        {
            aiMovement.StopMovement();
            StartAttackCoroutine();
            return;
        }
        if (sightLine.CircleOverLapCheck())
        {
            if(attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }    
            
            Pursue();
            return;
        }
        if(!patroling)
        {
            Patrol();
            patroling = true;
        }

    }
    public void SetPlayerPosition(Vector2 pos_)
    {
        playerPosition = pos_;
    }
    [ContextMenu("Patrol")]

    public void Patrol()
    {
        nextPosition = new Vector2(Random.Range(startingPosition.x - patrolRange.x, startingPosition.x + patrolRange.x), Random.Range(startingPosition.y - patrolRange.y, startingPosition.y + patrolRange.y));
        aiMovement.InitializeMovement(nextPosition);
    }
    public void StartAttackCoroutine()
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
        
    }
    public IEnumerator AttackCoroutine()
    {
        while(true)
        {
            Attack();
            yield return new WaitForSeconds(attackDelay);
        }
        //yield return null; not needed but just incase
    }
}
