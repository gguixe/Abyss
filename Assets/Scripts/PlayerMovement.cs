using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

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
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        //change.x = Input.GetAxis("Horizontal");   //Analog Value
        //change.y = Input.GetAxis("Vertical");     //Analog Value

        change.x = Input.GetAxisRaw("Horizontal");   //Digital Value
        change.y = Input.GetAxisRaw("Vertical");     //Digital Value

        UpdateAnimationAndMove();

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
