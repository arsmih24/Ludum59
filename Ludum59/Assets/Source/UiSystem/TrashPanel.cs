using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class TrashPanel : MonoBehaviour
    {
        [SerializeField] private Button crewButton;
        [SerializeField] private Button illuminatiButton;
        [SerializeField] private Button syp9Button;
        [SerializeField] private Button deathFileButton;
        [Space]
        [SerializeField] private GameObject crewFile;
        [SerializeField] private GameObject illuminatiFile;
        [SerializeField] private GameObject syp9File;
        [SerializeField] private GameObject deathFile;
        [Space]
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            crewButton.onClick.AddListener(ActivateCrewFilePanel);
            illuminatiButton.onClick.AddListener(ActivateIlluminatiFilePanel);
            syp9Button.onClick.AddListener(ActivateSyp9FilePanel);
            deathFileButton.onClick.AddListener(ActivateDeathFilePanel);
            closeButton.onClick.AddListener(Close);
        }

        private void ActivateCrewFilePanel() 
        {
            crewFile.SetActive(true);
        }
        private void ActivateIlluminatiFilePanel()
        {
            illuminatiFile.SetActive(true);
        }
        private void ActivateSyp9FilePanel()
        {
            syp9File.SetActive(true);
        }
        private void ActivateDeathFilePanel()
        {
            deathFile.SetActive(true);
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            crewButton.onClick.RemoveAllListeners();
            illuminatiButton.onClick.RemoveAllListeners();
            syp9Button.onClick.RemoveAllListeners();
            deathFileButton.onClick.RemoveAllListeners();
            closeButton.onClick.RemoveAllListeners();
        }
    }
}