using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hook : MonoBehaviour
{
    public Transform hookedTransform;
    private Camera mainCamera;
    private int length;
    private int strength;
    private int fishCount;

    private CircleCollider2D coll;
    private bool canMove = false;

    private List<Fish> hookedFishes;

    private Tweener cameraTween;


    void Awake()
    {
        mainCamera = Camera.main;
        coll = GetComponent<CircleCollider2D>();
        hookedFishes = new List<Fish>();
    }

    void Update()
    {
        if(canMove && Input.GetMouseButton(0)) // Hook movement along the X-axis when a left mouse key is pressed
        {
            Vector3 vector = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position; // Hook position
            position.x = vector.x; // change the position of the object along the X axis to the position by clicking on the screen
            transform.position = position;

            //Debug.Log("Vector X-axis: " + vector.x); Debug.Log("Vector Y-axis: " + vector.y);
            //Debug.Log("position Name:" + gameObject.name);
        }
    }

    public void StartFishing()
    {
        length = -50; // IdleManager
        strength = 3; // number of fishes on Hook.
        fishCount = 0;
        float time = (-length) * 0.1f;
        cameraTween = mainCamera.transform.DOMoveY(length, 1 + time * 0.25f, false).OnUpdate(delegate // Camera movement on Y-axis DOWN with DOTween animation
        {
            if (mainCamera.transform.position.y <= -11)
            {
                //Debug.Log("I'm working");
                transform.SetParent(mainCamera.transform); // Assigning a parent
            }
                
        }).OnComplete(delegate
        {
            coll.enabled = true; // Hook is visible
            cameraTween = mainCamera.transform.DOMoveY(0, time * 5, false).OnUpdate(delegate // Camera movement on Y-axis UP with DOTween animation
            {
                if (mainCamera.transform.position.y >= -25f)
                {
                    StopFishing();
                }
                    
            });
        });

        // Screen(GAME)
        coll.enabled = false;
        canMove = true;
        //Clear

    }

    void StopFishing()
    {
        canMove = false;
        cameraTween.Kill(false);
        cameraTween = mainCamera.transform.DOMoveY(0, 2, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y >= -11)
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x, -6);
            }
        }).OnComplete(delegate
        {
            transform.position = Vector2.down * 6;
            coll.enabled = true;
            HookClearing();
            
            // IdleManager Totalgain = num
            // Sceenmanager End Screen
        });
    }

    void HookClearing() // Clearing out the hook from the fishes
    {
        int salary = 0;
        for (int i = 0; i < hookedFishes.Count; i++) 
        {
            hookedFishes[i].transform.SetParent(null);
            hookedFishes[i].ResetFish();
            salary += hookedFishes[i].Type.price;
        }
    }



    private void OnTriggerEnter2D(Collider2D target) 
    {
        if(target.CompareTag("Fish") && fishCount != strength)
        {
            fishCount++;
            Fish component = target.GetComponent<Fish>();
            component.Hooked(); // collider - off, animation - off
            hookedFishes.Add(component);
            target.transform.SetParent(transform); // for hooked fishes new parent is a hook. So rotation and position will be equal with hookedTransform.
            target.transform.position = hookedTransform.position; 
            target.transform.rotation = hookedTransform.rotation;
            target.transform.localScale = Vector3.one; // (1,1,1)

            target.transform.DOShakeRotation(5, Vector3.forward * 45, 10, 90, false).SetLoops(1, LoopType.Yoyo).OnComplete(delegate
            {
                target.transform.rotation = Quaternion.identity;//(0,0,0)
            }); // Fish will be shake after hooking and when animation swithed off fish will have (0,0,0) rotation data.
            if (fishCount == strength)
                StopFishing();
        }
    }



}
