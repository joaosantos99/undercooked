using UnityEngine;

public class Simple_Movement : MonoBehaviour
{

    public float moveSpeed = 5;


    void Start()
    {

    }


    void Update()
    {

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.right * -moveSpeed * Time.deltaTime;

        }

    }
}