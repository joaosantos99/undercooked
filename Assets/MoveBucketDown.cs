using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBucketDown : MonoBehaviour
{

    [SerializeField] private GameObject ancora;
    [SerializeField] private Transform balde;
    [SerializeField] private float velocidade = 10f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        balde.transform.position = Vector2.MoveTowards(balde.transform.position, ancora.transform.position, Time.deltaTime * velocidade);
    }
}
