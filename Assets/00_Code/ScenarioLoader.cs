using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeZijlen
{
    public class ScenarioLoader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        //coroutines 
        //levels laden (async)
        //level transities (in en uit)

        public enum TransitionIn
        {
            FadeIn
        }

        public enum TransitionOut
        {
            FadeOut
        }

        public void LoadScene(string sceneName, float duration, TransitionIn transIn, TransitionOut transOut)
        {
            StartCoroutine(Transition(transIn, transOut, duration, sceneName));
        }

        private IEnumerator Transition(TransitionIn transIn, TransitionOut transOut, float duration, string sceneName)
        {
            switch (transOut)
            {
                case TransitionOut.FadeOut:
                    FadeOut(duration);
                    break;
            }

            yield return SceneManager.LoadSceneAsync(sceneName);

            switch (transIn)
            {
                case TransitionIn.FadeIn:
                    FadeIn(duration); 
                    break;
            }
        }

        private IEnumerator FadeIn(float duration)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 1;

            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = 1- (time/duration);
                yield return null;
            }
            canvasGroup.gameObject.SetActive(false);
        }

        private IEnumerator FadeOut(float duration)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 0;

            float time = 0;
            while(time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = time/duration;
                yield return null;
            }
            canvasGroup.gameObject.SetActive(false);
        }
    }
}
