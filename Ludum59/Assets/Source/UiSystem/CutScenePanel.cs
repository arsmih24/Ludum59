using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UiSystem
{
    public class CutScenePanel : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;
        [SerializeField] private VideoClip videoClip;
        [SerializeField, Range(0f, 1f)] private float volume;
        [SerializeField] private float timer = 7f;
        [Space]
        [SerializeField] private Image loadPanel;
        [SerializeField] private float fadeDuration = 1.5f;
        [Space]
        [SerializeField] private Button menuButton;
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonClip;
        [SerializeField] private float buttonVolume = 0.5f;
        [Space]
        [SerializeField] private bool IsStartCutScene;

        private VideoPlayer _videoPlayer;
        private RenderTexture _renderTexture;

        private void Awake()
        {
            menuButton?.onClick.AddListener(Menu);

            loadPanel.gameObject.SetActive(true);
        }

        private void Start()
        {
            loadPanel.DOFade(0, fadeDuration);

            if (IsStartCutScene) _renderTexture = new RenderTexture(1920, 1080, 0);
            else if (!IsStartCutScene) _renderTexture = new RenderTexture(1440, 1080, 0);
            _renderTexture.Create();

            rawImage.texture = _renderTexture;

            _videoPlayer = gameObject.AddComponent<VideoPlayer>();
            _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            _videoPlayer.targetTexture = _renderTexture;
            _videoPlayer.clip = videoClip;
            _videoPlayer.isLooping = false;
            _videoPlayer.playOnAwake = false;
            _videoPlayer.SetDirectAudioVolume(0, volume);

            _videoPlayer.Play();

            if (IsStartCutScene) Invoke(nameof(Next), timer);
            else if (!IsStartCutScene) Invoke(nameof(Exit), timer);
        }

        private void Cleanup()
        {
            _videoPlayer.Stop();
            Destroy(_videoPlayer);
            _videoPlayer = null;

            rawImage.texture = null;

            _renderTexture.Release();
            Destroy(_renderTexture);
            _renderTexture = null;
        }

        private void Next() 
        {
            loadPanel.DOFade(1, fadeDuration).OnComplete(() =>
            {
                Level.LoadNextLevel();
            });
        }

        private void Exit() 
        {
            loadPanel.DOFade(1, fadeDuration).OnComplete(() =>
            {
                PlayerPrefs.DeleteAll();
                Level.MainMenu();
            });
        }

        private void Menu()
        {
            audioSource.volume = buttonVolume;
            audioSource.PlayOneShot(buttonClip);

            Exit();
        }

        private void OnDestroy()
        {
            menuButton?.onClick.RemoveAllListeners();

            Cleanup();
        }
    }
}