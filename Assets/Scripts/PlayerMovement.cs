using Unity.Netcode;
using UnityEngine;


public class PlayerMovement : NetworkBehaviour
{
    public Vector2 Speed = new Vector2(1, 1);

    void Update()
    {
        if (!IsOwner) return;

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(Speed.x * inputX, Speed.y * inputY);

        transform.Translate(movement);
    }
}
