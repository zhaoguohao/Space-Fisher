using UnityEngine;
using UnityEngine.UI;

/* ScreensManager workes with UI elements: Text, Buttons and GameObjects from IdleButtons. 
 * Script can checking the availability of money for a purchase and update information on the screen.
 * 
 * Script created by @Mykola Kalchuk.
 */


public class ScreensManager : MonoBehaviour
{
    
    public static ScreensManager instance { get; private set; } // Singleton
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


    private void Awake()
    {
        if (ScreensManager.instance) // insurance in case of creation of 2 elements with ScreensManager.
            Destroy(base.gameObject);
        else
        {
            ScreensManager.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        _currentScreen = mainScreen;
    }

    private void Start()
    {
        CheckIdles();
        UpdateTexts();
    }

    /// <summary>
    /// Ñhecking the availability of money for a purchase
    /// </summary>
    private void CheckIdles()
    {
        int lengthCost = IdleManager.instance.lengthCost;
        int strengthCost = IdleManager.instance.strengthCost;
        int offlineEarningsCost = IdleManager.instance.offlineEarningsCost;
        int wallet = IdleManager.instance.wallet;

        // interactable - button can't be use.

        if (wallet < lengthCost)
            lengthButton.interactable = false;
        else
            lengthButton.interactable = true;
        if (wallet < strengthCost)
            strengthButton.interactable = false;
        else
            strengthButton.interactable = true;
        if (wallet < offlineEarningsCost)
            offlineButton.interactable = false;
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

    private void SetEndScreenMoney()
    {
        endScreenMoney.text = "$" + IdleManager.instance.totalGain;
    }

    private void SetReturnScreenMoney()
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
