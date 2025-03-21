using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeZijlen.SceneSelector
{
    public class ButtonPopulator : MonoBehaviour
    {
        public Button button;

        public void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            Debug.Log("Pressed button");
        }
    }
}
