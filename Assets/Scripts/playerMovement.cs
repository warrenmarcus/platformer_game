using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{


    private  Rigidbody2D player;
    private Collider2D coll;
    public Animator anim;
    private float playerH = .5f;
    private float playerW = .5f;
    private string currentState;
    private bool attacking;
    private Vector2 moveForce;

    // player states
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_FALL = "Fall";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_HURT = "Hurt";
    const string PLAYER_ATTACK = "Attack";



    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int pumpkin = 0;
    [SerializeField] private Text pumpkinText;
    [SerializeField] private float hurtForce = 10f;

  

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        ChangeAnimationState(PLAYER_IDLE);
    }

    private void Update()
    {

        if (currentState != PLAYER_HURT)
        {
            Movement();

        }
        Animation();
    }


    private void FixedUpdate()
    {


        
       // Move();
    }


    private void Movement()
    {
        float xDirection = Input.GetAxis("Horizontal");


            if (xDirection != 0)
            {
                if (xDirection < 0)
                {
                    player.velocity  = new Vector2(-speed, player.velocity.y);
                    transform.localScale = new Vector2(-playerW, playerH);

                }
                if (xDirection > 0)
                {
                player.velocity = new Vector2(speed, player.velocity.y);
                    transform.localScale = new Vector2(playerW, playerH);
                }

            }
            
          else if (xDirection == 0 && coll.IsTouchingLayers(ground))  
        {
            player.velocity = new Vector2(0 ,player.velocity.y);
        }
        

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
        // Attacking
        if (Input.GetKeyDown(KeyCode.F))
        {
            // need to make it flase after pressing
            attacking = true;
            ChangeAnimationState(PLAYER_ATTACK);
    
        }
        // Falling

    }


    private void Animation ()
    {

        if (player.velocity.y > 4f && !coll.IsTouchingLayers(ground))
        {
            ChangeAnimationState(PLAYER_JUMP);
        }


        else if (player.velocity.y < -4f && !coll.IsTouchingLayers(ground))
        {
            ChangeAnimationState(PLAYER_FALL);
        }
        else if (Mathf.Abs(player.velocity.x) > .3 && coll.IsTouchingLayers(ground))
        {
            ChangeAnimationState(PLAYER_RUN);
        }

        else if (player.velocity.x == 0 && player.velocity.y == 0 )
        {
            ChangeAnimationState(PLAYER_IDLE);
            
        }




    }

    private void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, jumpForce);

            ChangeAnimationState(PLAYER_JUMP);
       
    }

    private void Move()
    {
        player.AddForce(moveForce * speed);
   
    }

    public void ChangeAnimationState(string newState)
    {

        if (newState == currentState) return;

        anim.Play(newState);
        currentState = newState;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            pumpkin++;
            pumpkinText.text = pumpkin.ToString();
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            if (currentState == PLAYER_FALL)
            {
                Destroy(other.gameObject);
                Jump();
            }
            else if (attacking)
            {
                Destroy(other.gameObject);
                
            }
            else
            {
                currentState = PLAYER_HURT;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    player.velocity = new Vector2(-hurtForce, player.velocity.y);

                }
                else
                {
                    // move player to the right and take damage
                    player.velocity = new Vector2(hurtForce, player.velocity.y);
                }
                
            }
            
        }

        

    }

}
