using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float speed = 1f;
    private float speedJump = 70f;
    private Animator animator;
    private int score = 0;
    public int numberMushroom = 5;
    public GameObject canvasWin;
    public Text scoreText;

    public bool isGrounded = true;

    private void Start()
    {
        canvasWin.SetActive(false);
        ResumeGame();
        animator = this.GetComponent<Animator>();
        scoreText.text = ("Score: " + score +" / "+numberMushroom);
    }

    private void Update()
    {
        if (score == numberMushroom)
        {
            isWin();    
        }
    }

    void isWin()
    {
        canvasWin.SetActive(true);
        PauseGame();
    }

    void FixedUpdate()
    {
        
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed);
            animator.SetBool("isRunningR", true);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            animator.SetBool("isRunningR", false);
        }
        

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.left * speed);
            animator.SetBool("isRunningL", true);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            animator.SetBool("isRunningL", false);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            //transform.Translate(Vector3.up * speedJump );
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * speedJump;
            animator.SetBool("isJumping", true);
            isGrounded = false;
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isLanding", true);

        }
        else
        {
            animator.SetBool("isLanding", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("collectible"))
        {
            score++;
            scoreText.text = ("Score: " + score +" / "+numberMushroom);
            Destroy(other.gameObject);
        }
    }
    
    void PauseGame ()
    {
        Time.timeScale = 0;
    }

    void ResumeGame ()
    {
        Time.timeScale = 1;
    }
}
