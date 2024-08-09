using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float accelaration = 10;
    public float maxAccelaration = 10;
    public float maxVelocityX = 50;
    public float distance = 0;
    public float groundHeight = -3;
    public float jumpVelocity = 20;
    public bool isGrounded = false;

    private Animator playerAnimator;
    private BoxCollider2D playerCollider;
    private float normalColliderHeight;

    public AudioSource footstepSound;
    public float footstepVolumeMin = 0.5f;
    public float footstepVolumeMax = 1f;
    public float footstepPitchMin = 0.8f;
    public float footstepPitchMax = 1.2f;

    public AudioSource heartBeatSound;
    public float heartBeatVolumeMin = 0.4f;
    public float heartBeatVolumeMax = 0.8f;
    public float heartBeatPitchMin = 1f;
    public float heartBeatPitchMax = 1.3f;

    private Vector2 touchStartPos;

    private GameManager gameManager;
    public PauseManager pauseManager;


    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        normalColliderHeight = playerCollider.size.y;

        gameManager = FindObjectOfType<GameManager>();
        isGrounded = true;
    }

    void Update()
    {
        if (gameManager.currentState == GameManager.GameState.GameStart)
        {
            transform.position = new Vector2(-4.3f, -2.54f);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Slide();
            }

            // Mendeteksi input dari layar sentuh
            foreach (Touch touch in Input.touches)
            {
                // Jika layar disentuh
                if (touch.phase == TouchPhase.Began)
                {
                    // Simpan posisi awal sentuhan
                    touchStartPos = touch.position;
                }
                // Jika layar dilepas
                else if (touch.phase == TouchPhase.Ended)
                {
                    // Hitung perbedaan antara posisi awal dan akhir sentuhan
                    float swipeDistance = touch.position.y - touchStartPos.y;

                    // Jika perbedaan positif, berarti swipe ke atas
                    if (swipeDistance > 0)
                    {
                        Jump();
                    }
                    // Jika perbedaan negatif, berarti swipe ke bawah
                    else if (swipeDistance < 0)
                    {
                        Slide();
                    }
                }
            }

            if (isGrounded && pauseManager != null && pauseManager.isPaused == false)
            {
                float footstepVolume = Mathf.Lerp(footstepVolumeMin, footstepVolumeMax, Mathf.Abs(velocity.x) / maxVelocityX);
                float footstepPitch = Mathf.Lerp(footstepPitchMin, footstepPitchMax, Mathf.Abs(velocity.x) / maxVelocityX);
                footstepSound.volume = footstepVolume;
                footstepSound.pitch = footstepPitch;

                if (!footstepSound.isPlaying)
                {
                    footstepSound.Play();
                }
            }
            else
            {
                footstepSound.Stop();
            }

            float heartBeatVolume = Mathf.Lerp(heartBeatVolumeMin, heartBeatVolumeMax, Mathf.Abs(velocity.x) / maxVelocityX);
            float heartBeatPitch = Mathf.Lerp(heartBeatPitchMin, heartBeatPitchMax, Mathf.Abs(velocity.x) / maxVelocityX);
            heartBeatSound.volume = heartBeatVolume;
            heartBeatSound.pitch = heartBeatPitch;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            velocity.y = jumpVelocity;
            isGrounded = false;

            playerAnimator.SetBool("IsJumping", true);
        }
    }

    void Slide()
    {
        playerCollider.size = new Vector2(playerCollider.size.x, normalColliderHeight / 2f);

        playerCollider.offset = new Vector2(playerCollider.offset.x, -2.5f);

        playerAnimator.SetBool("IsSliding", true);

        StartCoroutine(ResetSlideAnimation());
    }

    IEnumerator ResetSlideAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        playerCollider.size = new Vector2(playerCollider.size.x, normalColliderHeight);

        playerCollider.offset = new Vector2(playerCollider.offset.x, 0f);

        playerAnimator.SetBool("IsSliding", false);
    }

    private void FixedUpdate()
    {
        if (gameManager.currentState != GameManager.GameState.GameStart)
        {
            return;
        }

        Vector2 pos = transform.position;

        if (!isGrounded && gameManager.currentState == GameManager.GameState.GameStart)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;

            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.gameObject.GetComponent<Ground>();
                if (ground != null)
                {
                    groundHeight = ground.groundHeight;
                    pos.y = groundHeight;
                    isGrounded = true;

                    playerCollider.size = new Vector2(playerCollider.size.x, normalColliderHeight);
                    playerAnimator.SetBool("IsJumping", false);
                    playerAnimator.SetBool("IsSliding", false);
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded && gameManager.currentState == GameManager.GameState.GameStart)
        {
            float veloRatio = velocity.x / maxVelocityX;
            accelaration = maxAccelaration * (1 - veloRatio);

            velocity.x += accelaration * Time.fixedDeltaTime;
            if (velocity.x >= maxVelocityX)
            {
                velocity.x = maxVelocityX;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider == null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
        }

        transform.position = pos;
    }
}