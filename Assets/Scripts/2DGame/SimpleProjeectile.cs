using System.Collections;
using UnityEngine;

public class SimpleProjeectile : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    public float speed;
    public float duration;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public void InstantiateProjectile(Vector2 direction_)
    {   
        myRigidBody.AddForce (direction_ * speed);
        StartCoroutine(ProjectileTime());
    }

    public IEnumerator ProjectileTime()
    {
        yield return new WaitForSeconds (duration);
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<IDamagable>().TakeDamage(10);
        }
    }
}
