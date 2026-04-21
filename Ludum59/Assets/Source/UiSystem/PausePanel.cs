using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button exitButton;

        private UiManager _uiManager;

        public void Construct(UiManager uiManager) 
        {
            _uiManager = uiManager;
        }

        private void Awake()
        {
            continueButton.onClick.AddListener(EndPause);
            exitButton.onClick.AddListener(Menu);
        }

        public void StartPause() 
        {
            gameObject.SetActive(true);
            if (_uiManager.IsMiniGameRunning) return;

            Time.timeScale = 0f;
        }

        public void EndPause() 
        {
            gameObject.SetActive(false);
            if (_uiManager.IsMiniGameRunning) return;

            Time.timeScale = 1f;
        }

        private void Menu() 
        {
            Level.MainMenu();
        }

        private void OnDestroy()
        {
            continueButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}