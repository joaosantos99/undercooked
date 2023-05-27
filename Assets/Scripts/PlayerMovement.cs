using Unity.Netcode;
using UnityEngine;


public class PlayerMovement : NetworkBehaviour
{
    private Vector2 spawnOne = new Vector2(6f, 0f);
    private Vector2 spawnTwo = new Vector2(0f, 10f);

    private float horizontal;
    private bool isFacingRight = true;

    private Camera camera;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        camera = Camera.main;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        calltoclientconectedServerRPC(NetworkManager.Singleton.LocalClientId);
    }


    [ServerRpc(RequireOwnership = false)]
    public void calltoclientconectedServerRPC(ulong clientId)
    {

        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId }
            }
        };


        int playerCount = NetworkManager.Singleton.ConnectedClients.Count;
        ClientConnectedClientRPC(playerCount, clientRpcParams);
    }

    [ClientRpc]
    private void ClientConnectedClientRPC(int playerCount, ClientRpcParams clientRpcParams = default)
    {
        if(playerCount == 1)
        {
            transform.position = spawnOne;
            camera.transform.position = new Vector3(0f, -0.2f, -10f);
        } else
        {
            transform.position = spawnTwo;
            camera.transform.position = new Vector3(0f, 9.5f, -10f);
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
