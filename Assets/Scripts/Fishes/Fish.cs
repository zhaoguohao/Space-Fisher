using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fish : MonoBehaviour
{
    [Serializable] // great for classes that simply store information in themselves.
    public class FishType
    {
        public int price;
        public float fishCount;
        public float minLength;
        public float maxLength;
        public float colliderRadius;
        public Sprite sprite;
    }

    private Fish.FishType type; // Our fish type.
    private CircleCollider2D coll;
    private SpriteRenderer rend;
    private float screenLeft;
    private Tweener tweener;

    public Fish.FishType Type
    { get { return type; } set { type = value; coll.radius = type.colliderRadius; rend.sprite = type.sprite; } }


    void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        rend = GetComponentInChildren<SpriteRenderer>(); // Method take spriteRenderer from fish child.
        screenLeft = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
    }

    public void ResetFish() 
    {
        if (tweener != null) // if animation is active line of code destroy it. 
            tweener.Kill(false);

        float num = UnityEngine.Random.Range(type.minLength, type.maxLength);
        coll.enabled = true; // collider switched on
        Vector3 position = transform.position;
        //Debug.Log("position: " + transform.position);
        position.y = num;
        position.x = screenLeft; // 0
        transform.position = position; // (0, Random.Range(), 0).

        float num2 = 1;
        float y = UnityEngine.Random.Range(num - num2, num + num2);
        Vector2 v = new Vector2(-position.x, y);

        float num3 = 3;
        float delay = UnityEngine.Random.Range(0, 2 * num3);
        tweener = transform.DOMove(v, num3, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetDelay(delay).OnStepComplete(delegate
        {
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x;
            transform.localScale = localScale;
        });

    }

    public void Hooked()
    {
        coll.enabled = false;
        tweener.Kill(false);
    }

}
