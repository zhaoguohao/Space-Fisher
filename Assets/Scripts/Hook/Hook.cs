using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hook : MonoBehaviour
{
    //public Transform hookedTransform;
    private Vector3 _mainCameraPosition;
    private Camera mainCamera;
    private int length;
    //private int strength;
    //private int fishCount;

    private CircleCollider2D coll;
    private bool canMove = false;


    private Tweener cameraTween;


    void Awake()
    {
        mainCamera = Camera.main;
        coll = GetComponent<CircleCollider2D>();
        _mainCameraPosition = mainCamera.transform.position;
        
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
        //strength = 3; // IdleManager
        //fishCount = 0;
        float time = (-length) * 0.1f;
        cameraTween = mainCamera.transform.DOMoveY(length, 1 + time * 0.25f, false).OnUpdate(delegate // Camera movement on Y-axis DOWN with DOTween animation
        {
            if (_mainCameraPosition.y <= -11)
                transform.SetParent(mainCamera.transform); // Assigning a parent
        }).OnComplete(delegate
        {
            coll.enabled = true; // Hook is visible
            cameraTween = mainCamera.transform.DOMoveY(0, time * 5, false).OnUpdate(delegate // Camera movement on Y-axis UP with DOTween animation
            {
                if (_mainCameraPosition.y >= -25f)
                    StopFishing();
            });
        });

        // Screen(GAME)
        coll.enabled = false;
        canMove = true;
        //Clear

    }

    void StopFishing()
    {

    }
}
