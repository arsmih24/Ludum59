using UnityEngine;

public class SignalsManager : MonoBehaviour
{
    [SerializeField] private GameObject firstSignal;
    [SerializeField] private GameObject secondSignal;
    [SerializeField] private GameObject thirdSignal;
    [SerializeField] private GameObject fourthSignal;
    [SerializeField] private GameObject fifthSignal;
    [Space]
    [SerializeField] private Sprite firstSignalSprite;
    [SerializeField] private Sprite secondSignalSprite;
    [SerializeField] private Sprite thirdSignalSprite;
    [SerializeField] private Sprite fourthSignalSprite;
    [SerializeField] private Sprite fifthSignalSprite;

    private Invoker _invoker;
    private bool _firstSignalCollected = false;
    private bool _secondSignalCollected = false;
    private bool _thirdSignalCollected = false;
    private bool _fourthSignalCollected = false;
    private bool _fifthSignalCollected = false;

    public bool FirstSignalCollected => _firstSignalCollected;
    public bool SecondSignalCollected => _secondSignalCollected;
    public bool ThirdSignalCollected => _thirdSignalCollected;
    public bool FourthSignalCollected => _fourthSignalCollected;
    public bool FifthSignalCollected => _fifthSignalCollected;

    public void Construct(Invoker invoker) 
    {
        _invoker = invoker;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("FirstSignalCollected"))
            CollectFirstSignal();

        if (PlayerPrefs.HasKey("SecondSignalCollected"))
            CollectSecondSignal();

        if (PlayerPrefs.HasKey("ThirdSignalCollected"))
            CollectThirdSignal();

        if (PlayerPrefs.HasKey("FourthSignalCollected"))
            CollectFourthSignal();

        if (PlayerPrefs.HasKey("FifthSignalCollected"))
            CollectFifthSignal();
    }

    public void CollectFirstSignal()
    {
        _firstSignalCollected = true;
        PlayerPrefs.SetString("FirstSignalCollected", "True");
        PlayerPrefs.SetString("HasSave", "True");
        firstSignal.gameObject.SetActive(false);
        _invoker.InvokeActivateSignalPanel(firstSignalSprite);

        CheckCollectedSignals();
    }

    public void CollectSecondSignal()
    {
        _secondSignalCollected = true;
        PlayerPrefs.SetString("SecondSignalCollected", "True");
        PlayerPrefs.SetString("HasSave", "True");
        secondSignal.gameObject.SetActive(false);
        _invoker.InvokeActivateSignalPanel(secondSignalSprite);

        CheckCollectedSignals();
    }

    public void CollectThirdSignal()
    {
        _thirdSignalCollected = true;
        PlayerPrefs.SetString("ThirdSignalCollected", "True");
        PlayerPrefs.SetString("HasSave", "True");
        thirdSignal.gameObject.SetActive(false);
        _invoker.InvokeActivateSignalPanel(thirdSignalSprite);

        CheckCollectedSignals();
    }

    public void CollectFourthSignal()
    {
        _fourthSignalCollected = true;
        PlayerPrefs.SetString("FourthSignalCollected", "True");
        PlayerPrefs.SetString("HasSave", "True");
        fourthSignal.gameObject.SetActive(false);
        _invoker.InvokeActivateSignalPanel(fourthSignalSprite);

        CheckCollectedSignals();
    }

    public void CollectFifthSignal()
    {
        _fifthSignalCollected = true;
        PlayerPrefs.SetString("FifthSignalCollected", "True");
        PlayerPrefs.SetString("HasSave", "True");
        fifthSignal.gameObject.SetActive(false);
        _invoker.InvokeActivateSignalPanel(fifthSignalSprite);

        CheckCollectedSignals();
    }

    private void CheckCollectedSignals() 
    {
        if (_fifthSignalCollected && _secondSignalCollected && _thirdSignalCollected &&
            _fourthSignalCollected && _fifthSignalCollected)
            _invoker.InvokeActivateSendFilesButton();
    }
}
