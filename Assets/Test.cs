using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    //public GameObject Cube;

    void Start()
    {
        transform.DOMove(new Vector3(0, 3, 0), 3).From();
    }
}
