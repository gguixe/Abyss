using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("LastDirection", -1);
        currentState = PlayerState.walk;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        //change.x = Input.GetAxis("Horizontal");   //Analog Value
        //change.y = Input.GetAxis("Vertical");     //Analog Value

        change.x = Input.GetAxisRaw("Horizontal");   //Digital Value
        change.y = Input.GetAxisRaw("Vertical");     //Digital Value

        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
        
        else if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();

        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f); //Mi animación es mas corta
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            if (change.x == 0.0) //If we're moving UP or DOWN
            {
                animator.SetBool("UpDown", true); //We're facing UP or Down
                animator.SetBool("moving", true);
            }
            else //We're not moving up or down
            {
                animator.SetFloat("moveX", change.x);
                animator.SetFloat("moveY", change.y);
                animator.SetBool("moving", true);
                animator.SetFloat("LastDirection", change.x);
                animator.SetBool("UpDown", false);

            }

            MoveCharacter();
        }
        else 
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
    }
}
