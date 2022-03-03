using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    holster,
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

    public GameObject weapon;
    private SpriteRenderer weapon_sprite;

    //public GameObject hitbox_right;
    //public GameObject hitbox_left; //Hitboxes activated in animation

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetFloat("LastDirection", -1);
        currentState = PlayerState.attack;
        weapon_sprite = weapon.GetComponent<SpriteRenderer>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", 0);
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");   //Digital Value
        change.y = Input.GetAxisRaw("Vertical");     //Digital Value

        UpdateAnimationAndMove();
        flip_sprite();

        if (Input.GetButtonDown("Attack") && currentState == PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }

        if (Input.GetButtonDown("Holster") || (Input.GetButtonDown("Fire1") && currentState == PlayerState.holster))
        {
            if(currentState == PlayerState.holster)
            {
                currentState = PlayerState.attack;
                weapon.SetActive(true);
            }
            else
            {
                currentState = PlayerState.holster;
                weapon.SetActive(false);
            }
        }


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
            weapon_sprite.sortingOrder = 0;
        }
        else
        {
            //print("Mouse is on right side of screen.");
            mySpriteRenderer.flipX = false;  // flip the sprite
            animator.SetFloat("side", 0);
            weapon_sprite.sortingOrder = 2;
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
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
    }
}
