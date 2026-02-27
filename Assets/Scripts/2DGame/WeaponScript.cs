using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public SpritCharScript playerScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damageScript;
        if(collision.TryGetComponent<IDamagable>(out damageScript))
        {
            playerScript.SendDMG(collision);
        }
    }
}
