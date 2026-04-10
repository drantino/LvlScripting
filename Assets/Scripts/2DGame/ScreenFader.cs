using System.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;
public class ScreenFader : MonoBehaviour
{
    public static ScreenFader instance;
    [SerializeField] private Image screenFade;
    private void Awake()
    {
        if(instance == null )
        {
            instance = this;
        }
        //ensure initial transperency
        if(screenFade != null)
        {
            Color c =screenFade.color;
            c.a = 0f;
            screenFade.color = c;
        }
    }
    public void BeginScreenFade(float duration)
    {
        if(screenFade != null)
        {
            StartCoroutine(FadeScreen(duration));
            TwoDGameState.Instance.EnableControls(false);
        } 
    }
    private IEnumerator FadeScreen(float duration)
    {
        TwoDGameState.Instance.player.GetComponent<SpritCharScript>().enabled = true;
        //immediately set screen to black
        SetAlpha(1f);
        Time.timeScale = 0;
        float timer = 0f;
        while(timer < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer/duration);
            SetAlpha(alpha);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
        TwoDGameState.Instance.player.GetComponent<SpritCharScript>().enabled = true;
        //ensure it fully transparent
        SetAlpha(0f);
    }
    private void SetAlpha(float alpha)
    {
        if(screenFade != null)
        {
            Color c = screenFade.color;
            c.a = alpha;
            screenFade.color = c;
        }
    }
}
