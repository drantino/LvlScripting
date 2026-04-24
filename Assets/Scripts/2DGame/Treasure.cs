using UnityEngine;

public class Treasure : MonoBehaviour, IInteractable
{
    public int treasureIndex;
    public GameObject interactText;
    public bool interactable;
    private void Start()
    {
        if (TwoDGameState.Instance.treasureChests[treasureIndex])
        {
            gameObject.SetActive(false);
        }
    }
    public void Interact(GameObject sentObject)
    {
        if (interactable)
        {
            TwoDGameState.Instance.TreasureGet(treasureIndex);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable = true;
            interactText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable= false;
            interactText.SetActive(true);
        }
    }
}
