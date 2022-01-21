using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : MonoBehaviour
{
    [SerializeField] private float leftStop;
    [SerializeField] private float rightStop;

    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;


    private Collider2D coll;
    private Rigidbody2D rigFrog;
    private Animator anim;
    private bool facingleft = true;


    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rigFrog = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (anim.GetBool("Jumping"))
        {
            if (rigFrog.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }

        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }
    void moveFrog()
    {
        if (facingleft)
        {
            if (transform.position.x > leftStop)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }


                if (coll.IsTouchingLayers(ground))
                {
                    rigFrog.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingleft = false;

            }
        }

        else
if (transform.position.x < rightStop)
        {
            if (transform.localScale.x != -1)
            {
                transform.localScale = new Vector3(-1, 1);
            }


            if (coll.IsTouchingLayers(ground))
            {
                rigFrog.velocity = new Vector2(jumpLength, jumpHeight);
                anim.SetBool("Jumping", true);
            }
        }
        else
        {
            facingleft = true;

        }
    }
}   

