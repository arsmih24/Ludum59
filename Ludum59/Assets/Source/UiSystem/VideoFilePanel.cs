using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UiSystem
{
    public class VideoFilePanel : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;      
        [SerializeField] private VideoClip videoClip;
        [Space]
        [SerializeField] private Button closeButton;
        [Space]
        [SerializeField] private AudioSource ambientSource;

        private VideoPlayer _videoPlayer;
        private RenderTexture _renderTexture;

        private void Awake()
        {
            closeButton.onClick.AddListener(Close);
        }

        void OnEnable()
        {
            ambientSource.Pause();

            _renderTexture = new RenderTexture(1920, 1080, 0);
            _renderTexture.Create();

            rawImage.texture = _renderTexture;

            _videoPlayer = gameObject.AddComponent<VideoPlayer>();
            _videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            _videoPlayer.targetTexture = _renderTexture;
            _videoPlayer.clip = videoClip;
            _videoPlayer.isLooping = true;
            _videoPlayer.playOnAwake = false;
            _videoPlayer.SetDirectAudioVolume(0, 0.6f);

            _videoPlayer.Play();
        }

        void Cleanup()
        {
            _videoPlayer.Stop();
            Destroy(_videoPlayer);
            _videoPlayer = null;

            rawImage.texture = null;

            _renderTexture.Release();
            Destroy(_renderTexture);
            _renderTexture = null;
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            ambientSource.Play();
            Cleanup();
        }

        private void OnDestroy()
        {
            closeButton.onClick.RemoveAllListeners();
        }
    }
}