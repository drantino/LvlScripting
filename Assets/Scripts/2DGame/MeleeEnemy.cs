using UnityEngine;

public class MeleeEnemy : Enemy
{
    public CircleCollider2D attackHitBox;
    public Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public override void Attack()
    {
        myAnimator.Play("MeleeAttack");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamagable>().TakeDamage(10);
        }
    }
}
