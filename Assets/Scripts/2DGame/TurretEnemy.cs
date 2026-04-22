using Unity.Mathematics;
using UnityEngine;

public class TurretEnemy : Enemy
{
    public GameObject attackPivot;
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
            Pose pose = new();
            //pose.position = attackPivot.transform.position;
            
            pose.rotation = Quaternion.LookRotation(new Vector3( playerPosition.x,playerPosition.y,0)- attackPivot.transform.position,attackPivot.transform.up);
            float angle = Mathf.Atan((playerPosition.y - attackPivot.transform.position.y)/ (playerPosition.x - attackPivot.transform.position.x));
            Debug.Log(angle);
            pose.rotation.x = angle;
            pose.rotation = quaternion.Euler(pose.rotation.eulerAngles.x,90,0);
            //attackPivot.transform.
            

            attackPivot.transform.rotation = Quaternion.Slerp(attackPivot.transform.rotation,pose.rotation,aimSpeed);
            
        }
    }
   
}
