using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public string enemyName;
    public int HP;
    public int ATK;
    public int DEF;

    public float attackDelay;

    public CircleOverlap sightLine;
    public CircleOverlap attackRange;

    public Vector2 playerPositoin;

    private Coroutine attackCoroutine;
    public abstract void Patrol();
    public abstract void Attack();
    public abstract void TakeDamage(float dmg_);
    public abstract void Die();
    public abstract void Pursue();
    private void Awake()
    {
        sightLine.OnOverLap += SetPlayerPosition;
        attackRange.OnOverLap += SetPlayerPosition;

    }

    private void Update()
    {
        if(sightLine.CircleOverLapCheck())
        {
            Pursue();
        }
        if (attackRange.CircleOverLapCheck())
        {
            StartAttackCoroutine();
        }
    }
    public void SetPlayerPosition(Vector2 pos_)
    {
        playerPositoin = pos_;
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
        //yield return null;
    }
}
