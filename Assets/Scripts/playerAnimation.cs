using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    Animator anim;
    private string currentState;

    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_FALL = "Fall";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_HURT = "Hurt";
    const string PLAYER_ATTACK = "Attack";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {

        if (newState != currentState)
        {


        }
        anim.Play(newState);


        currentState = newState;
    }
}
