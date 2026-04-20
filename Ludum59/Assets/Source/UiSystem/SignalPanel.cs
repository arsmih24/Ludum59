using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class SignalPanel : MonoBehaviour
    {
        [SerializeField] private Image signalImage;
        [Space]
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }

        public void Setup(Sprite siganlSprite) 
        {
            signalImage.sprite = siganlSprite;
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