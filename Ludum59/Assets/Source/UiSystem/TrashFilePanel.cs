using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class TrashFilePanel : MonoBehaviour
    {
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
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