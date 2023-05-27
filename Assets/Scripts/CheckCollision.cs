using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private GameObject player;
    private bool isEKeyPressed = false;

    private void Start()
    {
        // Find the player object by tag or assign it through the Inspector
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isEKeyPressed)
        {
            // Update the object's position to the player's position - 1
            transform.position = player.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            // Set the flag to indicate that the E key is pressed
            isEKeyPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset the flag when the collision is exited
            isEKeyPressed = false;
        }
    }
}