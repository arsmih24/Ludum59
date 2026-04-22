using System;
using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class SignalPanel : MonoBehaviour
    {
        [SerializeField] private Image signalImage;
        [Space]
        [SerializeField] private Button closeButton;
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip keyboardSound;
        [SerializeField, Range(0f, 1f)] private float keyboardVolume;
        [SerializeField] private float soundDelay = 0.3f;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }

        private void OnEnable()
        {
            Invoke(nameof(PlayKeyboardSound), soundDelay);
        }

        public void Setup(Sprite siganlSprite) 
        {
            signalImage.sprite = siganlSprite;
        }

        private void PlayKeyboardSound() 
        {
            audioSource.volume = keyboardVolume;
            audioSource.PlayOneShot(keyboardSound);
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