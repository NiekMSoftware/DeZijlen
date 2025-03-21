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
        [SerializeField] private Image image;
        [SerializeField] private LevelContainer levelContainer;

        [SerializeField, Tooltip("The position that the image is connected to.")] private Transform parent;
        [SerializeField] private Button levelSelectorButtonPrefab;

        public static ScenarioLoader Instance { get; private set; }

        // Singleton instance
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
            PopulateButtons();
        }

        // Gets the GameObject with the "UI_Parent" tag and sets it as parent. Then creates buttons on the location of the parent.
        private void PopulateButtons()
        {
            GameObject parentObject = GameObject.FindGameObjectWithTag("UI_Parent");

            if (parentObject != null)
                parent = parentObject.transform;
            else
                Debug.LogError("GRRR");

            foreach (var info in levelContainer.buttonInfo)
            {
                Button button = Instantiate(levelSelectorButtonPrefab, parent);
                button.name = info.scene.name;
                button.GetComponentInChildren<Text>().text = info.scene.name;
                button.onClick.AddListener(() => LoadScene(info.scene.name, 1f, TransitionIn.FadeIn, TransitionOut.FadeOut));
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

            PopulateButtons();

            yield return transIn switch
            {
                TransitionIn.FadeIn => FadeIn(duration)
            };

        }

        private IEnumerator FadeIn(float duration)
        {
            image.gameObject.SetActive(true);
            Color color = image.color;
            color.a = 1f; 
            image.color = color;

            // Makes the screen fade in from black.
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(1f, 0f, time / duration);
                image.color = color;
                yield return null;
            }
        }

        private IEnumerator FadeOut(float duration)
        {
            image.gameObject.SetActive(true);
            Color color = image.color;
            color.a = 0f;
            image.color = color;

            // Makes the screen fade out from black.
            float time = 0;
            while(time < duration)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, time / duration);
                image.color = color;
                yield return null;
            }
        }
    }
}
