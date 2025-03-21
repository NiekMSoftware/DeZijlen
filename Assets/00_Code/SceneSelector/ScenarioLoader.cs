using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DeZijlen.SceneSelector
{
    public enum TransitionIn
    {
        FadeIn
    }

    public enum TransitionOut
    {
        FadeOut
    }

    public class ScenarioLoader : MonoBehaviour
    {
        [SerializeField, Tooltip("CanvasGroup is needed to handle transitions.")] private CanvasGroup canvasGroup;
        [SerializeField] private LevelContainer levelContainer;

        [SerializeField] private Transform parent;
        [SerializeField] private Button levelSelectorButtonPrefab;

        public static ScenarioLoader Instance { get; private set; }

        private void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            foreach(var info in levelContainer.buttonInfo)
            {
                Button button = Instantiate(levelSelectorButtonPrefab, parent);
                button.name = info.scene.name;
                button.GetComponentInChildren<Text>().text = info.scene.name;
                button.onClick.AddListener(() => LoadScene(info.scene.name, 1f, TransitionIn.FadeIn, TransitionOut.FadeOut));
            }
        }

        private void LateUpdate()
        {
            if (parent == null)
            {
                GameObject parentObject = GameObject.FindGameObjectWithTag("UI_Parent");

                if (parentObject != null)
                    parent.position = parentObject.transform.position;
                else
                    Debug.LogError("GRRR");
            }
        }

        /// <summary>
        /// Loads in a scene with a given transition.
        /// </summary>
        /// <param name="sceneName">The name of the scene to transition to.</param>
        /// <param name="duration">The duration of the transition.</param>
        /// <param name="transIn">The in transition.</param>
        /// <param name="transOut">The out transition.</param>
        public void LoadScene(string sceneName, float duration, TransitionIn transIn, TransitionOut transOut)
        {
            StartCoroutine(Transition(transIn, transOut, duration, sceneName));
        }

        /// <summary>
        /// The transition between the active scene and the next scene with a transition.
        /// </summary>
        /// <param name="transIn">The in transition.</param>
        /// <param name="transOut">The out transition.</param>
        /// <param name="duration">The duration of the transition.</param>
        /// <param name="sceneName">The name of the scene to transition to.</param>
        /// <returns></returns>
        private IEnumerator Transition(TransitionIn transIn, TransitionOut transOut, float duration, string sceneName)
        {
            yield return transOut switch
            {
                TransitionOut.FadeOut => FadeOut(duration)
            };

            yield return SceneManager.LoadSceneAsync(sceneName);

            yield return transIn switch
            {
                TransitionIn.FadeIn => FadeIn(duration)
            };
        }

        private IEnumerator FadeIn(float duration)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 1;

            // Makes the screen fade in from black.
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

            // Makes the screen fade in to black.
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
