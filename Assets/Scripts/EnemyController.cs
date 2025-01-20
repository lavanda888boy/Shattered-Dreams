using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    public float walkSpeed = 20f;
    public float runSpeed = 17f;
    public float closeRange = 3f;
    public float idleThreshold = 0.1f;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        FindPlayer();
    }

    void Update()
    {
        if (player == null)
        {
            FindPlayer();
        }

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            Vector2 directionToPlayer = (player.position - transform.position).normalized;

            if (distanceToPlayer > 15.0f)
            {
                TeleportToPlayer();
                return;
            }

            if (distanceToPlayer <= closeRange)
            {
                rb.linearVelocityX = directionToPlayer.x * walkSpeed;
            }
            else
            {
                rb.linearVelocityX = directionToPlayer.x * runSpeed;
            }

            animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));

            if (directionToPlayer.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (directionToPlayer.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
        }

        if (Mathf.Abs(rb.linearVelocityX) < idleThreshold)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));
        }
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void TeleportToPlayer()
    {
        if (player != null)
        {
            Debug.Log("Teleporting to player...");
            transform.position = new Vector3(player.position.x - 5.0f, player.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RestartScene();
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
