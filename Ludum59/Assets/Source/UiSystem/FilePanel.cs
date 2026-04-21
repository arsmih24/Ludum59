using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class FilePanel : MonoBehaviour
    {
        [SerializeField] private GameObject description;
        [SerializeField] private GameObject coordinates;
        [Space]
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }

        public void Setup(bool collected) 
        {
            if (collected)
            {
                coordinates.SetActive(false);
                description.SetActive(true);
            }

            else 
            {
                coordinates.SetActive(true);
                description.SetActive(false);
            }
        }

        public void Open() 
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