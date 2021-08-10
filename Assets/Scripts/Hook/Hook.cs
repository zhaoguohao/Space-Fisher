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

private Collider2D coll;
private bool canMove = true;


private Tweener cameraTween;

void Awake()
{
    mainCamera = Camera.main;
    coll = GetComponent<Collider2D>();
}


    void Start()
    {
        
    }

    void Update()
    {
        if(canMove && Input.GetMouseButton(0))
        {
            Vector3 vector = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            position.x = vector.x;
            transform.position = position;
        }
    }
}
