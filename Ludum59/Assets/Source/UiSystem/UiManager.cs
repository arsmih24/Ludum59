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
        [SerializeField] private Button photoButton;

        private void Awake()
        {
            loadPanel.gameObject.SetActive(true);
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

        public Button ReturnPhotoButton() 
        {
            return photoButton;
        }
    }
}