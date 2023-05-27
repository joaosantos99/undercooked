using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GetPosition : MonoBehaviour
{

    private void Start()
    {
       
        Vector2 objectPosition = transform.position;
             
        Debug.Log("Object's X Coordinate: " + objectPosition.x);
        Debug.Log("Object's Y Coordinate: " + objectPosition.y);
        

    }

    public void Update()
    {

        Vector2 objectPosition = transform.position;

        Debug.Log("Object's X Coordinate: " + objectPosition.x);
        Debug.Log("Object's Y Coordinate: " + objectPosition.y);

    }
}

  

