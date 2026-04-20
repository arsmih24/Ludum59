using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class RadarPanel : MonoBehaviour
{
    [SerializeField] private Image blackHoleLight;
    [SerializeField] private Sprite blackHoleLightOn;
    [SerializeField] private Sprite blackHoleLightOff;
    [SerializeField] private float blackHoleLightBlinkPeriod = 0.5f;
    [Space]
    [SerializeField] private Image starLight;
    [SerializeField] private Sprite starLightOn;
    [SerializeField] private Sprite starLightOff;
    [SerializeField] private float starLightBlinkPeriod = 0.5f;
    [Space]
    [SerializeField] private Button photoButton;
    [SerializeField] private Sprite photoButtonActivated;
    [SerializeField] private Sprite photoButtonDeactivated;
    [Space]
    [SerializeField] private Button closeButton;

    private Image _photoButtonImage;
    private Coroutine _blackHoleLigtBlinkCoroutine;
    private Coroutine _starLigtBlinkCoroutine;

    private void Awake()
    {
        _photoButtonImage = photoButton.GetComponent<Image>();
        closeButton.onClick.AddListener(Close);
    }

    private void Start()
    {
        PhotoButtonDeactivate();
        blackHoleLight.sprite = blackHoleLightOff;
        starLight.sprite = starLightOff;
    }

    public Button ReturnPhotoButton()
    {
        return photoButton;
    }
    public void PhotoButtonActivate()
    {
        photoButton.interactable = true;
        _photoButtonImage.sprite = photoButtonActivated;
    }
    public void PhotoButtonDeactivate()
    {
        photoButton.interactable = false;
        _photoButtonImage.sprite = photoButtonDeactivated;
    }

    private IEnumerator BlackHoleLightBlinkCoroutine() 
    {
        while (true) 
        {
            blackHoleLight.sprite = blackHoleLightOn;
            yield return new WaitForSeconds(blackHoleLightBlinkPeriod);

            blackHoleLight.sprite = blackHoleLightOff;
            yield return new WaitForSeconds(blackHoleLightBlinkPeriod);
        }
    }
    public void StartBlackHoleLightBlinkCoroutine() 
    {
        _blackHoleLigtBlinkCoroutine = StartCoroutine(BlackHoleLightBlinkCoroutine());
    }
    public void StopBlackHoleLightBlinkCoroutine()
    {
        StopCoroutine(_blackHoleLigtBlinkCoroutine);
        _blackHoleLigtBlinkCoroutine = null;
        blackHoleLight.sprite = blackHoleLightOff;
    }

    private IEnumerator StarLightBlinkCoroutine()
    {
        while (true)
        {
            starLight.sprite = starLightOn;
            yield return new WaitForSeconds(starLightBlinkPeriod);

            starLight.sprite = starLightOff;
            yield return new WaitForSeconds(starLightBlinkPeriod);
        }
    }
    public void StartStarLightBlinkCoroutine()
    {
        _starLigtBlinkCoroutine = StartCoroutine(StarLightBlinkCoroutine());
    }
    public void StopStarLightBlinkCoroutine()
    {
        StopCoroutine(_starLigtBlinkCoroutine);
        _starLigtBlinkCoroutine = null;
        starLight.sprite = starLightOff;
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
