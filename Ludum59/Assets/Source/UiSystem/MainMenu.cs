using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UiSystem
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Image loadPanel;
        [SerializeField] private float fadeDuration = 1.5f;
        [Space]
        [SerializeField] private Button startButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button exitButton;
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonClip;

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            continueButton.onClick.AddListener(ContinueGame);
            exitButton.onClick.AddListener(ExitGame);

            if (!PlayerPrefs.HasKey("HasSave"))
                continueButton.interactable = false;
        }

        private void StartGame()
        {
            PlayerPrefs.DeleteAll();
            ContinueGame();
        }

        private void ContinueGame() 
        {
            audioSource.PlayOneShot(buttonClip);

            loadPanel.DOFade(1, fadeDuration).OnComplete(() =>
            {
                Level.LoadNextLevel();
            });
        }

        private void ExitGame()
        {
            audioSource.PlayOneShot(buttonClip);

            Application.Quit();
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveAllListeners();
            continueButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}