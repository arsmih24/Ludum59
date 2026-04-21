using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UiSystem
{
    public class MemoryMiniGame : MonoBehaviour
    {
        [SerializeField] private Image symbolDisplay;
        [SerializeField] private float displayDuration = 1f;
        [SerializeField] private float betweenSymbolsDelay = 0.3f;
        [SerializeField] private float startDelay = 1f;
        [Space]
        [SerializeField] private Sprite[] symbolSprites = new Sprite[9];
        [Space]
        [SerializeField] private Button[] symbolButtons = new Button[9];
        [Space]
        [SerializeField] private Image[] progressLights = new Image[5];
        [SerializeField] private Sprite lightOffSprite;
        [SerializeField] private Sprite lightOnSprite;
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip showSymbolSound;
        [SerializeField] private AudioClip correctAnswerSound;
        [SerializeField] private AudioClip wrongAnswerSound;
        [SerializeField] private AudioClip winGameSound;
        [Space]
        [SerializeField] private int maxSequenceLength = 5;

        private List<int> _sequence = new List<int>();
        private int _currentPlayerIndex = 0;
        private int _currentDisplayIndex = 0;
        private bool _isShowingSequence = false;
        private bool _isPlayerTurn = false;
        private bool _isGameActive = false;
        private bool _isWaitingForRestart = false;
        private Coroutine _gameCoroutine;
        private UiManager _uiManager;

        public void Construct(UiManager uiManager) 
        {
            _uiManager = uiManager;
        }

        private void Awake()
        {
            for (int i = 0; i < symbolButtons.Length; i++)
            {
                int symbolIndex = i;
                if (symbolButtons[i] != null)
                    symbolButtons[i].onClick.AddListener(() => OnSymbolButtonClicked(symbolIndex));
            }
        }

        public void StartMiniGame()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;

            _isGameActive = true;
            _isWaitingForRestart = false;
            _currentPlayerIndex = 0;
            _currentDisplayIndex = 0;

            GenerateNewSequence();
            ResetLights();

            if (_gameCoroutine != null)
                StopCoroutine(_gameCoroutine);

            _gameCoroutine = StartCoroutine(GameLoop());
        }

        private void GenerateNewSequence()
        {
            _sequence.Clear();
            for (int i = 0; i < maxSequenceLength; i++)
            {
                _sequence.Add(Random.Range(0, 9));
            }
        }

        private void ResetLights()
        {
            foreach (var light in progressLights)
            {
                if (light != null)
                    light.sprite = lightOffSprite;
            }
        }

        private void LightUpProgress(int index)
        {
            if (index >= 0 && index < progressLights.Length && progressLights[index] != null)
            {
                progressLights[index].sprite = lightOnSprite;
            }
        }

        private void SetButtonsInteractable(bool interactable)
        {
            foreach (var button in symbolButtons)
            {
                if (button != null)
                    button.interactable = interactable;
            }
        }

        private IEnumerator GameLoop()
        {
            while (_isGameActive)
            {
                if (_isWaitingForRestart)
                {
                    yield return new WaitUntil(() => !_isWaitingForRestart);
                }

                _isShowingSequence = true;
                _isPlayerTurn = false;
                SetButtonsInteractable(false);

                yield return StartCoroutine(ShowSequence());

                _isShowingSequence = false;
                _isPlayerTurn = true;
                _currentPlayerIndex = 0;
                SetButtonsInteractable(true);

                while (_isPlayerTurn && _isGameActive && !_isWaitingForRestart)
                {
                    yield return null;
                }

                if (!_isGameActive)
                    yield break;

                if (_isWaitingForRestart)
                    continue;

                if (_currentDisplayIndex >= maxSequenceLength - 1 && _currentPlayerIndex >= maxSequenceLength)
                {
                    yield return StartCoroutine(WinGame());
                    yield break;
                }

                if (_currentDisplayIndex < maxSequenceLength - 1)
                {
                    _currentDisplayIndex++;
                }
            }
        }

        private IEnumerator ShowSequence()
        {
            if (_currentDisplayIndex == 0 && _currentPlayerIndex == 0)
            {
                yield return WaitRealtime(startDelay);
            }

            int showCount = _currentDisplayIndex + 1;

            for (int i = 0; i < showCount; i++)
            {
                int symbolIndex = _sequence[i];

                symbolDisplay.sprite = symbolSprites[symbolIndex];
                symbolDisplay.gameObject.SetActive(true);
                PlaySound(showSymbolSound);

                yield return WaitRealtime(displayDuration);

                symbolDisplay.gameObject.SetActive(false);

                if (i < showCount - 1)
                {
                    yield return WaitRealtime(betweenSymbolsDelay);
                }
            }
        }

        private IEnumerator WaitRealtime(float duration)
        {
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        private void OnSymbolButtonClicked(int symbolIndex)
        {
            if (!_isPlayerTurn || _isShowingSequence || !_isGameActive || _isWaitingForRestart) return;

            int expectedIndex = _sequence[_currentPlayerIndex];

            if (symbolIndex == expectedIndex)
            {
                _currentPlayerIndex++;
                LightUpProgress(_currentPlayerIndex - 1);
                PlaySound(correctAnswerSound);

                if (_currentPlayerIndex > _currentDisplayIndex)
                {
                    _isPlayerTurn = false;
                }
            }
            else
            {
                StartCoroutine(WrongAnswer());
            }
        }

        private IEnumerator WrongAnswer()
        {
            _isPlayerTurn = false;
            _isShowingSequence = true;
            _isWaitingForRestart = true;
            SetButtonsInteractable(false);
            PlaySound(wrongAnswerSound);

            for (int i = 0; i < 3; i++)
            {
                ResetLights();
                yield return WaitRealtime(0.2f);

                foreach (var light in progressLights)
                    if (light != null) light.sprite = lightOnSprite;

                yield return WaitRealtime(0.2f);
            }

            ResetLights();

            yield return WaitRealtime(0.5f);

            GenerateNewSequence();
            _currentDisplayIndex = 0;
            _currentPlayerIndex = 0;

            _isWaitingForRestart = false;
        }

        private IEnumerator WinGame()
        {
            _isPlayerTurn = false;
            _isShowingSequence = true;
            SetButtonsInteractable(false);
            PlaySound(winGameSound);

            for (int i = 0; i < 3; i++)
            {
                ResetLights();
                yield return WaitRealtime(0.2f);

                foreach (var light in progressLights)
                    if (light != null) light.sprite = lightOnSprite;

                yield return WaitRealtime(0.2f);
            }

            foreach (var light in progressLights)
                if (light != null) light.sprite = lightOnSprite;

            yield return WaitRealtime(0.5f);

            CloseMiniGame();
        }

        private void CloseMiniGame()
        {
            _isGameActive = false;
            _uiManager.MiniGameClosed();
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            SetButtonsInteractable(true);
        }

        private void PlaySound(AudioClip clip)
        {
            if (audioSource != null && clip != null)
                audioSource.PlayOneShot(clip);
        }

        public void ForceClose()
        {
            _isGameActive = false;
            StopAllCoroutines();
            CloseMiniGame();
        }

        private void OnDestroy()
        {
            foreach (var button in symbolButtons)
            {
                if (button != null)
                    button.onClick.RemoveAllListeners();
            }
        }
    }
}