using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Numerics;

public class Player : MonoBehaviour
{
      private float speed;
      private float horizontal;
      public float jump = 7f;
      public Transform GroundCheck;
  public float GroundCheckRadius = 0.2f;
      public LayerMask GroundLayer;
      public AudioClip jumpClip;
      public AudioClip damageClip;
     private Rigidbody2D rb;
      private bool isGrounded;
     private Animator animator;
     private int extraJumps;
     private SpriteRenderer spriteRenderer;
     private AudioSource audioSource;
     public GameObject winUI;
   public TMPro.TextMeshProUGUI Lose;
      private Camera mainCam;
     private bool movingCamera = false;
      private UnityEngine.Vector3 targetPos;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
           audioSource = GetComponent<AudioSource>();
            mainCam = Camera.main;
         }

        void Update()
         {
             horizontal = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new UnityEngine.Vector2(horizontal * speed, rb.linearVelocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
         {
                 if (isGrounded)
             {
                    rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocity.x, jump);
                    Debug.Log("Jumped!");
                    playSFX(jumpClip);
               }

                SetAnimation(horizontal);
             }
         }

       void FixedUpdate()
       {
            isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
         }

      private void SetAnimation(float horizontal)
         {
            if (isGrounded)
             {
                 if (horizontal == 0)
               {
                    animator.Play("Player_Idle");
                 }
                 else
                 {
                 animator.Play("Player_Run");
             }
             }

        else
         {
             if (rb.linearVelocity.y > 0)
             {
                    animator.Play("Player_Jump");
             }
                 else
                 {
                     animator.Play("Player_Fall");
                 }
             }
         }
         private void OnTriggerEnter2D(Collider2D collision)
         {
            //  if (collision.gameObject.tag == "Damage")
            //  {
            //       playSFX(damageClip);
            //      rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocity.x, jump);
            //      StartCoroutine(BinkRed());

            //         if (health <= 0)
            //         {
            //           Debug.Log("Player Died!");
            //          animator.Play("Player_Death");
            //           Die();
            //     }
            //  }

             if (collision.gameObject.CompareTag("Background"))
            {
                 UnityEngine.Vector3 bgPos = collision.gameObject.transform.position;
                 mainCam.transform.position = new UnityEngine.Vector3
                 (
                    bgPos.x,
                   bgPos.y,
                    mainCam.transform.position.z
                );
            }
         }
         private IEnumerator BinkRed()
         {
             spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
         }
        private void Die()
         {
           UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
         public void playSFX(AudioClip audioClip, float volume = 1f)
         {
             audioSource.clip = audioClip;
              audioSource.volume = volume;
            audioSource.Play();
      }
         
    
}

