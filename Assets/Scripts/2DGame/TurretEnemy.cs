using UnityEngine;

public class TurretEnemy : Enemy
{
    public GameObject attackPivot;

    public override void Attack()
    {
        
        Debug.Log("attack");
    }
    public override void Update()
    {
        base.Update();
        if(attackRange.CircleOverLapCheck())
        {
            attackPivot.transform.LookAt(new Vector3( playerPosition.x,playerPosition.y) - transform.position);
            Debug.Log( new Vector3(playerPosition.x, playerPosition.y) - transform.position);
        }
    }
   
}
