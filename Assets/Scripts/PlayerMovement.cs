using Unity.Netcode;
using UnityEngine;


public class PlayerMovement : NetworkBehaviour
{
    private Vector2 spawnOne = new Vector2(6f, 0f);
    private Vector2 spawnTwo = new Vector2(0f, 10f);

    private float horizontal;
    private bool isFacingRight = true;
    private bool hasItem = false;
    private bool inBucketRange = false;
    private bool bucketHasItem = false;

    private GameObject Mush;
    private GameObject Bucket;
    private Camera camera;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        camera = Camera.main;
        Mush = GameObject.FindGameObjectWithTag("Mush");
        Bucket = GameObject.FindGameObjectWithTag("Bucket");
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
        if (!IsOwner) return;

        if(playerCount == 1)
        {
            transform.position = spawnOne;
            camera.transform.position = new Vector3(0.49f, -2.92f, -10f);
        } else
        {
            transform.position = spawnTwo;
            camera.transform.position = new Vector3(0.49f, 14.37f, -10f);
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

        if(hasItem)
        {
            Mush.transform.position = transform.position;
        }

        if(inBucketRange || bucketHasItem)
        {
            Mush.transform.position = Bucket.transform.position;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if(!inBucketRange && bucketHasItem)
        {
            Bucket.transform.position = Vector2.MoveTowards(Bucket.transform.position, new Vector2(-11.04f, 10.58f), 6f * Time.deltaTime);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal > 0f || !isFacingRight && horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Mush") && Input.GetKey(KeyCode.E))
        {
            hasItem = true;
        }

        if (collision.CompareTag("Bucket") && hasItem && Input.GetKey(KeyCode.E))
        {
            inBucketRange = true;
            bucketHasItem = true;
            hasItem = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mush"))
        {
            hasItem = false;
        }

        if (collision.CompareTag("Bucket"))
        {
            inBucketRange = false;
        }
    }
}
