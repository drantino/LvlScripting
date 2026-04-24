using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TurretEnemy : Enemy
{
    public GameObject attackPivot, aimPivot, beamAttack;
    public float aimSpeed, attackBuildUp, attackBuildUpMax, beamCDTime;
    public bool beamCD;
    public SpriteRenderer beamImage;

    public override void Attack()
    {
        if (!beamCD && attackRange.CircleOverLapCheck() && attackBuildUp < attackBuildUpMax)
        {
            beamAttack.GetComponent<BoxCollider2D>().enabled = false;
            aimPivot.transform.LookAt(playerPosition, transform.up);
            if (playerPosition.x < transform.position.x)
            {
                float angle;
                if (playerPosition.y > transform.position.y)
                {
                    angle = 270 - (aimPivot.transform.eulerAngles.x % 90);
                }
                else
                {
                    angle = 180 - (aimPivot.transform.eulerAngles.x % 90);
                }
                aimPivot.transform.rotation = Quaternion.Euler(angle, 90, 0);
            }
            attackPivot.transform.rotation = Quaternion.Slerp(attackPivot.transform.rotation, aimPivot.transform.rotation, aimSpeed);
            attackBuildUp += Time.deltaTime;
            //adjust beam opacity
            beamImage.color = new Color(beamImage.color.r,beamImage.color.g,beamImage.color.b,(attackBuildUp/attackBuildUpMax)/2);
        }
        else if(attackBuildUp > attackBuildUpMax)
        {
            //max beam opacity
            beamImage.color = new Color(beamImage.color.r, beamImage.color.g, beamImage.color.b, 1);
            attackBuildUp = 0;
            beamCD = true;
            beamAttack.GetComponent<BoxCollider2D>().enabled = true;
            SoundEffectManager.Instance.PlaySoundByName("BeamAttack");
            StartCoroutine(ResetBeamCD());
        }
        else
        {
            attackBuildUp = 0;
            if(!beamCD)
            {
                beamImage.color = new Color(beamImage.color.r, beamImage.color.g, beamImage.color.b, 0);
            }
        }
    }
    public override void Update()
    {
        base.Update();  
    }
    public IEnumerator ResetBeamCD()
    {
        yield return new WaitForSeconds(0.5f);
        beamAttack.GetComponent<BoxCollider2D>().enabled = false;
        //reduce beam opacity
        float opacity = 1;
        while(opacity > 0)
        {
            opacity -= Time.deltaTime*2;
            beamImage.color = new Color(beamImage.color.r, beamImage.color.g, beamImage.color.b, opacity);
            yield return null;
        }
        beamCD = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IDamagable>().TakeDamage(ATK);
        }
    }
}
