using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    public DreamcatcherManager DreamcatcherManager;
    public SoulPortionManager SoulPortionManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocityX = moveInput * moveSpeed;

        bool isRunning = Mathf.Abs(moveInput) > 0;
        animator.SetBool("isRunning", isRunning);

        if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            isGrounded = false;
        }
    }

    private void InitializeManagers()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            DreamcatcherManager = gameManager.GetComponent<DreamcatcherManager>();
            SoulPortionManager = gameManager.GetComponent<SoulPortionManager>();

            if (DreamcatcherManager == null && SoulPortionManager == null)
            {
                Debug.LogWarning("Managers not found on GameManager object.");
            }
        }
        else
        {
            Debug.LogWarning("GameManager object not found in the scene.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint = GameObject.Find("SpawnPoint");

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }

        if (scene.name.Contains("Dream"))
        {
            jumpForce = 17f;
        }
        else
        {
            jumpForce = 12f;
        }

        InitializeManagers();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DreamcatcherTag"))
        {
            Destroy(other.gameObject);
            DreamcatcherManager.dreamcatcherCount++;
        }
        else if (other.gameObject.CompareTag("SoulPortion"))
        {
            Destroy(other.gameObject);
            SoulPortionManager.soulPortionCount++;
        }
    }
}

