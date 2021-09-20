using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleManager : MonoBehaviour
{
    [HideInInspector] public int length;
    [HideInInspector] public int strength;
    [HideInInspector] public int offlineEarnings;
    [HideInInspector] public int lengthCost;
    [HideInInspector] public int strengthCost;
    [HideInInspector] public int offlineEarningsCost;
    [HideInInspector] public int wallet;
    [HideInInspector] public int totalGain;


    private int[] costs = new int[] { 120, 151, 197, 250, 324, 414, 537, 687, 892, 1145, 1484, 1911, 2479, 3196, 4148, 5359, 6954, 9000, 11687 };

    public static IdleManager instance;

    void Awake()
    {
        if (IdleManager.instance)
            object.Destroy(gameObject);
        else
            IdleManager.instance = this;

        length = PlayerPrefs.GetInt("Length", 30);
    }

    void Update()
    {
        
    }
}
