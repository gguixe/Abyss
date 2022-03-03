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
    private SpriteRenderer mySpriteRenderer;

    //public GameObject hitbox_right;
    //public GameObject hitbox_left; //Hitboxes activated in animation

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetFloat("LastDirection", -1);
        currentState = PlayerState.walk;
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");   //Digital Value
        change.y = Input.GetAxisRaw("Vertical");     //Digital Value

        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(AttackCo());
        }
       
        UpdateAnimationAndMove();
        flip_sprite();

    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        //yield return new WaitForSeconds(.3f); //Mi animación es mas corta
        //currentState = PlayerState.walk;
    }

    void flip_sprite()
    {
        //Change animation when player looks left or right
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

        if (mouse.x < playerScreenPoint.x)
        {
            //print("Mouse is on left side of screen.");
            mySpriteRenderer.flipX = true;  // flip the sprite
            animator.SetFloat("side", 1);
        }
        else
        {
            //print("Mouse is on right side of screen.");
            mySpriteRenderer.flipX = false;  // flip the sprite
            animator.SetFloat("side", 0);
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
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
