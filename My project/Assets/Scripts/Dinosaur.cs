using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class Dinosaur : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float radius;
    [SerializeField] public int lives = 3;
    [SerializeField] private TMP_Text LivesText;

    private Rigidbody2D dinoRb;
    private Animator dinoAnimator;
    public Touch my_touch;


    // Start is called before the first frame update
    void Start()
    {
        dinoRb = GetComponent<Rigidbody2D>();
        dinoAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);
        dinoAnimator.SetBool("isGrounded", isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1f)
        {
            if (isGrounded)
            {
                dinoRb.AddForce(Vector2.up * upForce);
            }
            
        }
        if (Input.touchCount > 0)
        {
            my_touch = Input.GetTouch(0);
            if (isGrounded)
            {
                dinoRb.AddForce(Vector2.up * upForce);
            }

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            if ( lives == 1)
            {
                GameManager.Instance.ShowGameOverScreen();
                dinoAnimator.SetTrigger("Die");
                Time.timeScale = 0f;
            }
            else
            {
                lives--;
                LivesText.text = "Vidas : " + lives.ToString();
            }
                    
        }
        else if (collision.gameObject.CompareTag("Life"))
        {
            Destroy(collision.gameObject);
            if (lives < 5)
            {
                lives++;
                LivesText.text = "Vidas : " + lives.ToString();
            }    
        }
    }
}
