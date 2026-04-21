using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class FolderPanel : MonoBehaviour
    {
        [SerializeField] private Button[] fileButtons;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }

        public void Activate() 
        {
            gameObject.SetActive(true);
        }

        private void Close() 
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            closeButton.onClick.RemoveAllListeners();
        }
    }
}