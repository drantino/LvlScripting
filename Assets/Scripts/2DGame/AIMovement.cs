using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class AIMovement : MonoBehaviour
{
    public float range, moveSpeed;
    Rigidbody2D myRigidBody;
    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public void InitializeMovement(Vector3 newPosition)
    {
        StartCoroutine(MoveToPosition(newPosition));
    }
    private IEnumerator MoveToPosition(Vector3 newPosition_)
    {
        bool inRange = false;
        while(!inRange)
        {
            Vector2 moveDir= newPosition_ - transform.position;
            moveDir.Normalize();
            myRigidBody.linearVelocity = moveDir * moveSpeed;
            inRange = (Vector2.Distance(transform.position, newPosition_)< range);
            
            if(inRange )
            {
                myRigidBody.linearVelocity = Vector3.zero;
            }
            yield return null;
        }
        yield return null;
    }
    
}
