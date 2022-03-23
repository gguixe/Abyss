using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{

    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogActivation) 
        {
            if(!isOpen)
            {
                //Open the chest
                OpenChest();
            }
            else
            {
                //Chest is already open
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        //Dialog window on
        dialogBox.SetActive(true);
        //dialog text = contents text
        dialogText.text = "You found a " + contents.itemDescription;
        //add contents to the invetory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        //Raise the signal to the player to animate
        raiseItem.Raise();
        //raise the context clue
        context.Raise();
        //set the chest to opened
        isOpen = true;
        //animation
        anim.SetBool("opened", true);
    }

    public void ChestAlreadyOpen()
    {
        //Dialog off
        dialogBox.SetActive(false);
        //Raise the signal to the player to stop animating
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            dialogActivation = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            dialogActivation = false;
        }
    }

}
