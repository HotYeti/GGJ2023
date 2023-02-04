using System.Collections;
using UnityEngine;

namespace Helpers
{
    public class Fade : MonoBehaviour
    {

        [SerializeField] private AnimationCurve m_FadeCurve;
        [SerializeField] private float m_FadeDuration = 1.0f;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StartCoroutine(DoFade());
        }

        private IEnumerator DoFade()
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < m_FadeDuration)
            {
                canvasGroup.alpha = m_FadeCurve.Evaluate(elapsedTime / m_FadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0.0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

    }
}
