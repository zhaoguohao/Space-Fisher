using UnityEngine;
using UnityEngine.UI;

public class ScreensManager : MonoBehaviour
{
    
    public static ScreensManager instance { get; private set; } // singleton
    private GameObject _currentScreen;

    [Header("Game screens from UI elements: ")]
    public GameObject endScreen;
    public GameObject gameScreen;
    public GameObject mainScreen;
    public GameObject returnScreen;

    [Space(2)][Header("Game buttons from UI elements: ")]
    public Button lengthButton;
    public Button strengthButton;
    public Button offlineButton;

    [Space(2)][Header("Game text from UI elements: ")]
    public Text gameScreenMoney;
    public Text endScreenMoney;
    public Text returnScreenMoney;
    [Header("Length:")]
    public Text lengthCostText;
    public Text lengthValueText;
    [Header("Strength:")]
    public Text strengthCostText;
    public Text strengthValueText;
    [Header("Offline:")]
    public Text offlineCostText;
    public Text offlineValueText;
   

    private int gameCount;


    private void Awake() // Singleton. Awake workes for the situation where in editor will created 2 objects with ScreensManager. In editor can be only 1 object with this script.
    {
        if (ScreensManager.instance)
            Destroy(base.gameObject);
        else
        {
            ScreensManager.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
            

        _currentScreen = mainScreen;
    }

    void Start()
    {
        CheckIdles();
        UpdateTexts();
    }

    public void CheckIdles()
    {
        int lengthCost = IdleManager.instance.lengthCost;
        int strengthCost = IdleManager.instance.strengthCost;
        int offlineEarningsCost = IdleManager.instance.offlineEarningsCost;
        int wallet = IdleManager.instance.wallet;

        if (wallet < lengthCost)
            lengthButton.interactable = false; // button can not be use no more
        else
            lengthButton.interactable = true;

        if (wallet < strengthCost)
            strengthButton.interactable = false; // button can not be use no more
        else
            strengthButton.interactable = true;

        if (wallet < offlineEarningsCost)
            offlineButton.interactable = false; // button can not be use no more
        else
            offlineButton.interactable = true;
    }

    public void UpdateTexts()
    {
        gameScreenMoney.text = "$" + IdleManager.instance.wallet;
        lengthCostText.text = "$" + IdleManager.instance.lengthCost;
        lengthValueText.text = "$" + IdleManager.instance.length + "m";
        strengthCostText.text = "$" + IdleManager.instance.strengthCost;
        strengthValueText.text = "$" + IdleManager.instance.strength + "fishes";
        offlineCostText.text = "$" + IdleManager.instance.offlineEarningsCost;
        offlineValueText.text = "$" + IdleManager.instance.offlineEarnings + "/min";

    }

    public void SetEndScreenMoney()
    {
        endScreenMoney.text = "$" + IdleManager.instance.totalGain;
    }

    public void SetReturnScreenMoney()
    {
        returnScreenMoney.text = "$" + IdleManager.instance.totalGain + "gained while waiting!";

    }

    public void ChangeScreen(Screens screen)
    {
        _currentScreen.SetActive(false);
        switch (screen)
        {
            case Screens.MAIN:
                _currentScreen = mainScreen;
                UpdateTexts();
                CheckIdles();
                break;

            case Screens.GAME:
                _currentScreen = gameScreen;
                gameCount++;
                break;

            case Screens.END:
                _currentScreen = endScreen;
                SetEndScreenMoney();
                break;

            case Screens.RETURN:
                _currentScreen = returnScreen;
                SetReturnScreenMoney();
                break;

        }
        _currentScreen.SetActive(true);
    }



}
