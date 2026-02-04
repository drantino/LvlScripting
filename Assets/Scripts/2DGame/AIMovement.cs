using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class AIMovement : MonoBehaviour
{
    public float range, moveSpeed;
    Rigidbody2D myRigidBody;
    public event UnityAction OnArrive;
    Vector3 newPosition;
    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public void InitializeMovement(Vector3 newPosition_)
    {
        newPosition = newPosition_;
        StartCoroutine(MoveToPosition());
    }
    public void SetNewPosition(Vector3 newPosition_)
    {
        newPosition = newPosition_;
    }
    public void StopMovement()
    {
        StopAllCoroutines();
        myRigidBody.linearVelocity = Vector3.zero;
    }
    private IEnumerator MoveToPosition()
    {
        bool inRange = false;
        while (!inRange)
        {
            Vector2 moveDir = newPosition - transform.position;
            moveDir.Normalize();
            myRigidBody.linearVelocity = moveDir * moveSpeed;
            inRange = (Vector2.Distance(transform.position, newPosition) < range);

            if (inRange)
            {
                myRigidBody.linearVelocity = Vector3.zero;
                OnArrive?.Invoke();
            }
            yield return null;
        }
        
        yield return null;
    }

}
