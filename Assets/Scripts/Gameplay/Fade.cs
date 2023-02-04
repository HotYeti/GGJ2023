using System.Collections;
using UnityEngine;
public class Fade : MonoBehaviour
{

    public float fadeDuration = 1.0f;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(DoFade());
    }

    private IEnumerator DoFade()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = 1.0f - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

}
