using Unity.Mathematics;
using UnityEngine;

public class TurretEnemy : Enemy
{
    public GameObject attackPivot, aimPivot;
    public float aimSpeed;

    public override void Attack()
    {
        
        Debug.Log("attack");
    }
    public override void Update()
    {
        base.Update();
        if(attackRange.CircleOverLapCheck())
        {
            aimPivot.transform.LookAt(playerPosition,transform.up);
            if(playerPosition.x < transform.position.x)
            {
                if (playerPosition.y > transform.position.y)
                {
                    float angle = 270 - (aimPivot.transform.eulerAngles.x % 90);
                    aimPivot.transform.rotation = Quaternion.Euler(angle, 90, 0);
                }
                else
                {
                    float angle = 180-(aimPivot.transform.eulerAngles.x % 90);
                    Debug.Log(angle);
                    aimPivot.transform.rotation = Quaternion.Euler(angle, 90, 0);
                }
            }

            
            attackPivot.transform.rotation = Quaternion.Slerp(attackPivot.transform.rotation,aimPivot.transform.rotation,aimSpeed);
            
        }
    }
   
}
