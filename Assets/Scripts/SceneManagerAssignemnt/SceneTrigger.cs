using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTrigger : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    public string sceneName;
    bool triggeredLoad;
    public GameObject loadUI;
    public Slider progressBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggeredLoad = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggeredLoad && collision != null && collision.CompareTag("Player"))
        {
            triggeredLoad=true;
            StartCoroutine(AdditiveSceneLoad(sceneName));
            loadUI.SetActive(true);
        }
    }
    public IEnumerator AdditiveSceneLoad(string sceneNameToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNameToLoad, LoadSceneMode.Additive);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        loadUI.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
