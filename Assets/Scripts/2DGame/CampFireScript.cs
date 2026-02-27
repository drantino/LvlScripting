using UnityEngine;
using UnityEngine.Events;

public class CampFireScript : MonoBehaviour,IInteractable
{
    public GameObject restText;
    public bool interactable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            restText.SetActive(true);
            interactable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            restText.SetActive(false);
            interactable = false;
        }
    }
    public void Interact()
    {
        TwoDGameState.Instance.RestEnemies();
        ScreenFader.instance.BeginScreenFade(3);
    }
}
