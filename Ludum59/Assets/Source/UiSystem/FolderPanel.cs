using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class FolderPanel : MonoBehaviour
    {
        [SerializeField] private Button firstFileButton;
        [SerializeField] private Button secondFileButton;
        [SerializeField] private Button thirdFileButton;
        [SerializeField] private Button fourthFileButton;
        [SerializeField] private Button fifthFileButton;
        [Space]
        [SerializeField] private FilePanel firstFilePanel;
        [SerializeField] private FilePanel secondFilePanel;
        [SerializeField] private FilePanel thirdFilePanel;
        [SerializeField] private FilePanel fourthFilePanel;
        [SerializeField] private FilePanel fifthFilePanel;
        [Space]
        [SerializeField] private Button audioFileButton;
        [SerializeField] private FilePanel audioFilePanel;
        [Space]
        [SerializeField] private Button closeButton;

        private SignalsManager _signalsManager;

        public void Construct(SignalsManager signalsManager) 
        {
            _signalsManager = signalsManager;
        }

        private void Awake()
        {
            firstFileButton.onClick.AddListener(FirstSignalButton);
            secondFileButton.onClick.AddListener(SecondSignalButton);
            thirdFileButton.onClick.AddListener(ThirdSignalButton);
            fourthFileButton.onClick.AddListener(FourthSignalButton);
            fifthFileButton.onClick.AddListener(FifthSignalButton);
            audioFileButton.onClick.AddListener(AudioFileButton);

            closeButton.onClick.AddListener(Close);
        }

        private void OnEnable()
        {
            if (!firstFileButton.gameObject.activeInHierarchy) firstFileButton.gameObject.SetActive(true);

            if (_signalsManager.FirstSignalCollected && !secondFileButton.gameObject.activeInHierarchy)
                secondFileButton.gameObject.SetActive(true);

            if (_signalsManager.FirstSignalCollected && !audioFileButton.gameObject.activeInHierarchy)
                audioFileButton.gameObject.SetActive(true);

            if (_signalsManager.SecondSignalCollected && !thirdFileButton.gameObject.activeInHierarchy)
                thirdFileButton.gameObject.SetActive(true);

            if (_signalsManager.ThirdSignalCollected && !fourthFileButton.gameObject.activeInHierarchy)
                fourthFileButton.gameObject.SetActive(true);

            if (_signalsManager.FourthSignalCollected && !fifthFileButton.gameObject.activeInHierarchy)
                fifthFileButton.gameObject.SetActive(true);
        }

        private void FirstSignalButton() 
        {
            firstFilePanel.Setup(_signalsManager.FirstSignalCollected);
            firstFilePanel.Open();
        }
        private void SecondSignalButton()
        {
            secondFilePanel.Setup(_signalsManager.SecondSignalCollected);
            secondFilePanel.Open();
        }
        private void ThirdSignalButton()
        {
            thirdFilePanel.Setup(_signalsManager.ThirdSignalCollected);
            thirdFilePanel.Open();
        }
        private void FourthSignalButton()
        {
            fourthFilePanel.Setup(_signalsManager.FourthSignalCollected);
            fourthFilePanel.Open();
        }
        private void FifthSignalButton()
        {
            fifthFilePanel.Setup(_signalsManager.FifthSignalCollected);
            fifthFilePanel.Open();
        }

        private void AudioFileButton() 
        {
            audioFilePanel.Open(); 
        }

        private void Close() 
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            firstFileButton.onClick.RemoveAllListeners();
            secondFileButton.onClick.RemoveAllListeners();
            thirdFileButton.onClick.RemoveAllListeners();
            fourthFileButton.onClick.RemoveAllListeners();
            fifthFileButton.onClick.RemoveAllListeners();
            audioFileButton.onClick.RemoveAllListeners();

            closeButton.onClick.RemoveAllListeners();
        }
    }
}