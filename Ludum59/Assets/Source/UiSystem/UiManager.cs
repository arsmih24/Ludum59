using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Image loadPanel;
        [SerializeField] private float fadeDuration = 1f;
        [Space]
        [SerializeField] private Button radarButton;
        [SerializeField] private Button folderButton;
        [Space]
        [SerializeField] private GameObject workspacePanel;
        [SerializeField] private RadarPanel radarPanel;
        [SerializeField] private MemoryMiniGame miniGamePanel;
        [SerializeField] private FolderPanel folderPanel;
        [Space]
        [SerializeField] private SignalPanel signalPanel;

        private void Awake()
        {
            loadPanel.gameObject.SetActive(true);
            radarButton.onClick.AddListener(ActivateRadarPanel);
            folderButton.onClick.AddListener(ActivateFolderPanel);
        }

        private void Start()
        {
            loadPanel.DOFade(0, fadeDuration);
        }

        public void ReloadGame()
        {
            loadPanel.DOFade(1, fadeDuration).OnComplete(() =>
            {
                Level.ReloadLevel();
            });
        }
        public void EndGame()
        {
            loadPanel.DOFade(1, fadeDuration).OnComplete(() =>
            {
                Level.LoadNextLevel();
            });
        }

        private void ActivateRadarPanel() 
        {
            workspacePanel.SetActive(false);
        }
        public void DeactivateRadarPanel() 
        {
            workspacePanel.SetActive(true);
        }

        public void ActivateSignalPanel(Sprite signalSprite)
        {
            signalPanel.Setup(signalSprite);
            signalPanel.gameObject.SetActive(true);
        }

        private void ActivateFolderPanel() 
        {
            folderPanel.Activate();
        }

        public Button ReturnPhotoButton() 
        {
            return radarPanel.ReturnPhotoButton();
        }
        public void PhotoButtonActivate() 
        {
            radarPanel.PhotoButtonActivate();
        }
        public void PhotoButtonDeactivate()
        {
            radarPanel.PhotoButtonDeactivate();
        }

        public void StartBlackHoleLightBlinkCoroutine() 
        {
            radarPanel.StartBlackHoleLightBlinkCoroutine();
        }
        public void StopBlackHoleLightBlinkCoroutine() 
        {
            radarPanel.StopBlackHoleLightBlinkCoroutine();
        }

        public void StartStarLightBlinkCoroutine() 
        {
            radarPanel.StartStarLightBlinkCoroutine();
        }
        public void StopStarLightBlinkCoroutine() 
        {
            radarPanel.StopStarLightBlinkCoroutine();
        }

        public void StartMiniGame() 
        {
            miniGamePanel.StartMiniGame();
        }

        private void OnDestroy()
        {
            radarButton.onClick.RemoveAllListeners();
            folderButton.onClick.RemoveAllListeners();
        }
    }
}