using DG.Tweening;
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
        [SerializeField] private Button prisonerButton;
        [SerializeField] private Button folderButton;
        [SerializeField] private Button trashButton;
        [SerializeField] private Button welcomeFileButton;
        [SerializeField] private Button prisonerFileButton;
        [Space]
        [SerializeField] private GameObject workspacePanel;
        [SerializeField] private RadarPanel radarPanel;
        [SerializeField] private MemoryMiniGame miniGamePanel;
        [SerializeField] private PrisonerPanel prisonerPanel;
        [SerializeField] private FolderPanel folderPanel;
        [SerializeField] private TrashPanel trashPanel;
        [SerializeField] private PausePanel pausePanel;
        [Space]
        [SerializeField] private SignalPanel signalPanel;
        [Space]
        [SerializeField] private GameObject welcomeFile;
        [SerializeField] private GameObject prisonerFile;
        [Space]
        [SerializeField] private GameObject buddy;

        private bool _isMiniGameRunning = false;
        private bool _isPaused = false;

        public bool IsMiniGameRunning => _isMiniGameRunning;

        private void Awake()
        {
            loadPanel.gameObject.SetActive(true);
            radarButton.onClick.AddListener(ActivateRadarPanel);
            prisonerButton.onClick.AddListener(ActivatePrisonerPanel);
            folderButton.onClick.AddListener(ActivateFolderPanel);
            trashButton.onClick.AddListener(ActivateTrashPanel);
            welcomeFileButton.onClick.AddListener(OpenWelcomeFile);
            prisonerFileButton.onClick.AddListener(OpenPrisonerFile);
        }

        private void Start()
        {
            loadPanel.DOFade(0, fadeDuration);

            if (PlayerPrefs.HasKey("HasSave")) return;
            buddy.SetActive(true);

            PlayerPrefs.SetString("HasSave", "True");
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

        private void OpenWelcomeFile() 
        {
            welcomeFile.SetActive(true);
        }
        private void OpenPrisonerFile() 
        {
            prisonerFile.SetActive(true);
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
            folderPanel.gameObject.SetActive(true);
        }

        private void ActivatePrisonerPanel() 
        {
            prisonerPanel.gameObject.SetActive(true);
        }

        private void ActivateTrashPanel() 
        {
            trashPanel.gameObject.SetActive(true);
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
            _isMiniGameRunning = true;
        }

        public void MiniGameClosed() 
        {
            _isMiniGameRunning = false;
        }

        public void ActivateSendFilesButton() 
        {
            folderPanel.ActivateSendFilesButton();
        }

        public void SetPause() 
        {
            if (!_isPaused)
            {
                pausePanel.StartPause();
                _isPaused = true;
            }

            else if (_isPaused) 
            {
                pausePanel.EndPause();
                _isPaused = false;
            }
        }

        private void OnDestroy()
        {
            radarButton.onClick.RemoveAllListeners();
            prisonerButton.onClick.RemoveAllListeners();
            folderButton.onClick.RemoveAllListeners();
            trashButton.onClick.RemoveAllListeners();
            welcomeFileButton.onClick.RemoveAllListeners();
            prisonerFileButton.onClick.RemoveAllListeners();
        }
    }
}