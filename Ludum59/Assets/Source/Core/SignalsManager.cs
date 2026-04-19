using UnityEngine;

public class SignalsManager : MonoBehaviour
{
    [SerializeField] private GameObject firstSignal;
    [SerializeField] private GameObject secondSignal;
    [SerializeField] private GameObject thirdSignal;
    [SerializeField] private GameObject fourthSignal;
    [SerializeField] private GameObject fifthSignal;

    private bool _firstSignalCollected = false;
    private bool _secondSignalCollected = false;
    private bool _thirdSignalCollected = false;
    private bool _fourthSignalCollected = false;
    private bool _fifthSignalCollected = false;

    public bool FirstSignalCollected => _firstSignalCollected;
    public bool SecondSignalCollected => _secondSignalCollected;
    public bool ThiedSignalCollected => _thirdSignalCollected;
    public bool FourthSignalCollected => _fourthSignalCollected;
    public bool FifthSignalCollected => _fifthSignalCollected;

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
        firstSignal.gameObject.SetActive(false);
    }

    public void CollectSecondSignal()
    {
        _secondSignalCollected = true;
        PlayerPrefs.SetString("SecondSignalCollected", "True");
        secondSignal.gameObject.SetActive(false);
    }

    public void CollectThirdSignal()
    {
        _thirdSignalCollected = true;
        PlayerPrefs.SetString("ThirdSignalCollected", "True");
        thirdSignal.gameObject.SetActive(false);
    }

    public void CollectFourthSignal()
    {
        _fourthSignalCollected = true;
        PlayerPrefs.SetString("FourthSignalCollected", "True");
        fourthSignal.gameObject.SetActive(false);
    }

    public void CollectFifthSignal()
    {
        _fifthSignalCollected = true;
        PlayerPrefs.SetString("FifthSignalCollected", "True");
        fifthSignal.gameObject.SetActive(false);    
    }
}
