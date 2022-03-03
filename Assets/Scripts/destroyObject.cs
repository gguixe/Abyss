using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{

    private Animator anim;
    private Vector3 change;
    private Rigidbody2D myRigidbody;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyObject()
    {
        anim.SetBool("destroy", true);
        StartCoroutine(breakCo());
    }
    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("damage"))
        {
            DestroyObject();
        }

        //no sirve de na lo hace todo el rigidbody
        //change.x = Input.GetAxisRaw("Horizontal");   //Digital Value
        //change.y = Input.GetAxisRaw("Vertical");     //Digital Value

        //if (collision.CompareTag("Player") && change != Vector3.zero)
        //{
        //    myRigidbody.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
        //}
    }

}
