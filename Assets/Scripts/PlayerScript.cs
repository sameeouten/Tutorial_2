using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    
    private Rigidbody2D rd2d;

    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text winText;
    public Text lives;
    private int livesValue = 3;
    public Text loseText;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    public float jumpForce;
    private bool facingRight = true;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

public AudioClip musicClipTwo;




    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
       winText.text = "";
       lives.text = livesValue.ToString();
        loseText.text ="";
        musicSource.clip = musicClipOne;
        musicSource.loop =true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        
        if (hozMovement > 0 && facingRight == true)
        {
            Debug.Log ("Facing Right");
        
        }
        if (hozMovement < 0 && facingRight == false)
        {
            Debug.Log ("Facing Left");
        }

        if (vertMovement > 0 && isOnGround == false)
        {
            Debug.Log ("Jumping");
        }
        else if (vertMovement == 0 && isOnGround == true)
        {
            Debug.Log("Not Jumping");
        }

if(Input.GetKeyDown(KeyCode.Escape))
             {
                 Application.Quit();
             }
             
    }
   private void OnCollisionEnter2D(Collision2D collision)
   {
       if(collision.collider.tag == "Coin")
       {
          scoreValue += 1;
         score.text = scoreValue.ToString();
           Destroy(collision.collider.gameObject);
         if(scoreValue == 4)
      {
          transform.position = new Vector3 (43.36f, 0.0F, 0.0f);
        livesValue =3;
          lives.text=livesValue.ToString();
      }
       }
     
      
      if(scoreValue ==8)
      {
        winText.text = "You Win!Game Created by Samantha Outen";
       musicSource.loop = false;
        musicSource.clip = musicClipTwo;
        musicSource.Play();

      }
      if(collision.collider.tag =="Enemies")
      {
          livesValue -=1;
          lives.text = livesValue.ToString();
          Destroy(collision.collider.gameObject);
      }
      if(livesValue == 0)
      {
      loseText.text = "You Lose!";
       Destroy(gameObject);
      }
      
   }
   private void OnCollisionStay2D(Collision2D collision)
   {
       if(collision.collider.tag == "Ground" && isOnGround)
       {
           if(Input.GetKey(KeyCode.W))
           {
               rd2d.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
           }
       }
   }
       
       void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

       
    
}

