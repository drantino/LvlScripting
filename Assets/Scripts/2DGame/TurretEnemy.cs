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
            
            aimPivot.transform.rotation = Quaternion.Euler(aimPivot.transform.eulerAngles.x,90,0);

            attackPivot.transform.rotation = Quaternion.Slerp(attackPivot.transform.rotation,aimPivot.transform.rotation,aimSpeed);
            
        }
    }
   
}
