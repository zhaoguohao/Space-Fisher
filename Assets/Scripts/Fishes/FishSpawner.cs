using System;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private Fish fishPrefab;
    [SerializeField] private Fish.FishType[] fishTypes; // Creating an array with a data type from a script Fish.

    void Awake()
    {
        for(int i = 0; i < fishTypes.Length; i++)
        {
            int num = 0;
            while(num < fishTypes[i].fishCount)
            {
                Fish fish = Instantiate<Fish>(fishPrefab); // using Generics to instantiate object. 
                fish.Type = fishTypes[i];
                fish.ResetFish();
                num++;
            }
        }
    }
}
