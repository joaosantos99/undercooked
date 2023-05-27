using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraScript : NetworkBehaviour
{
    private Vector3 CameraPositionOne = new Vector3(0f, -0.2f, -10f);
    private Vector3 CameraPositionTwo = new Vector3(0f, 9.5f, -10f);

    void Start()
    {
        transform.position = CameraPositionOne;   
    }
}
