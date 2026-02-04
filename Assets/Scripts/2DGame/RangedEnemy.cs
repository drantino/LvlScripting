using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectilePreFab;
    public Transform projectileSpawnLocation;
    public override void Attack()
    {
        //instantiate ea projectile
        // give the projectile a direction + veolcity
        //projecitle handles collisions
        GameObject obj = Instantiate(projectilePreFab, projectileSpawnLocation.position,Quaternion.identity);
        SimpleProjeectile projectile = obj.GetComponent<SimpleProjeectile>();
        projectile.InstantiateProjectile(new Vector2(0,-1));
    }

    public override void Die()
    {
        
    }




    public override void TakeDamage(float dmg_)
    {
        
    }
}
