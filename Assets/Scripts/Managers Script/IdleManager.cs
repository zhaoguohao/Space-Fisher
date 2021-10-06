using UnityEngine;
using System;


/* IdleManager workes with buttons from MAIN and RETURN sceens
 * 
 * Script created by @Mykola Kalchuk.
 */

public class IdleManager : MonoBehaviour
{
    [HideInInspector] public int length;
    [HideInInspector] public int lengthCost;

    [HideInInspector] public int strength;
    [HideInInspector] public int strengthCost;

    [HideInInspector] public int offlineEarnings;
    [HideInInspector] public int offlineEarningsCost;

    [HideInInspector] public int wallet;
    [HideInInspector] public int totalGain;

    private int[] costs = new int[] { 120, 151, 197, 250, 324, 414, 537, 687, 892, 1145, 1484, 1911, 2479, 3196, 4148, 5359, 6954, 9000, 11687 };

    public static IdleManager instance;

    private void Awake()
    {
        if (IdleManager.instance)
            UnityEngine.Object.Destroy(gameObject);
        else
            IdleManager.instance = this;

        //PlayerPrefs.DeleteAll();

        // Saved information getting:
        length = -PlayerPrefs.GetInt("Length", 30);
        strength = PlayerPrefs.GetInt("Strength", 3);
        offlineEarnings = PlayerPrefs.GetInt("Offline", 3);
        wallet = PlayerPrefs.GetInt("Wallet", 0);

        lengthCost = costs[-length / 10 - 3];
        strengthCost = costs[strength - 3];
        offlineEarningsCost = costs[offlineEarnings - 3];

    }

    private void OnApplicationPause(bool paused)
    {
        if(paused)
        {
            DateTime now = DateTime.Now; // .NET System time;
            PlayerPrefs.SetString("Date", now.ToString()); // System time saving;
            //MonoBehaviour.print(now.ToString());
        }
        else
        {
            string @string = PlayerPrefs.GetString("Date", string.Empty);
            if(@string != string.Empty)
            {
                DateTime d = DateTime.Parse(@string);
                totalGain = (int)((DateTime.Now - d).TotalMinutes * offlineEarnings + 1.0);
                ScreensManager.instance.ChangeScreen(Screens.RETURN);
                //print("Total Gain: " + totalGain);
            }
        }
    }

    private void OnApplicationQuit()
    {
        OnApplicationPause(true);
    }

    public void BuyLength() // Length Button
    {
        length -= 10;
        wallet -= lengthCost;
        lengthCost = costs[-length / 10 - 3];
        PlayerPrefs.SetInt("Length", -length);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);
    }

    public void BuyStrength() // Strength Button
    {
        strength++;
        wallet -= strengthCost;
        strengthCost = costs[strength - 3];
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);
    }

    public void BuyOfflineEarnings() // OfflineEarnings Button
    {
        offlineEarnings++;
        wallet -= offlineEarningsCost;
        strengthCost = costs[offlineEarnings - 3];
        PlayerPrefs.SetInt("Offline", offlineEarnings);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);
    }

    public void CollectMoney()
    {
        wallet += totalGain;
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);
    }

    public void CollectDoubleMoney()
    {
        wallet += totalGain * 2;
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreensManager.instance.ChangeScreen(Screens.MAIN);
    }

}
