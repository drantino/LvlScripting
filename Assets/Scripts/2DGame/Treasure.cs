using UnityEngine;

public class Treasure : MonoBehaviour, IInteractable, IDamagable, IExplode
{
    public int treasureIndex;
    private void Start()
    {
        if (TwoDGameState.Instance.treasureChests[treasureIndex])
        {
            gameObject.SetActive(false);
        }
    }
    public void Explode()
    {
        
    }

    public void Interact()
    {
        
    }

    public void TakeDamage(int incomeingDamage)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            TwoDGameState.Instance.TreasureGet(treasureIndex);
            gameObject.SetActive(false);
        }
    }
}
