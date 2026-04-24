using TMPro;
using UnityEngine;

public class PortolOpener : MonoBehaviour, IInteractable
{
    public GameObject portolToOpen, objectToHide, interactText;
    public bool interactable;
    public int requiredObjective, objectiveToChange;
    public string requireText;
    public void Interact(GameObject sentGameObject)
    {
        if (interactable && TwoDGameState.Instance.treasureChests[requiredObjective])
        {
            OpenPortol();
            if (objectiveToChange >= 0)
            {
                TwoDGameState.Instance.treasureChests[objectiveToChange] = true;
            }
        }
        else
        {
            interactText.GetComponentInChildren<TextMeshProUGUI>().text = requireText;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(objectiveToChange >= 0 && TwoDGameState.Instance.treasureChests[objectiveToChange])
        {
            OpenPortol();
        }
    }
    public void OpenPortol()
    {
        portolToOpen.SetActive(true);
        objectToHide.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable = true;
            interactText.SetActive(true);
            interactText.GetComponentInChildren<TextMeshProUGUI>().text = "[E]";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactable = false;
            interactText.SetActive(false);
        }
    }
}
