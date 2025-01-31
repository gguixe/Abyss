﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("Player") && collision.isTrigger) //Knockback activation conditions
        {
            if (collision.gameObject.CompareTag("enemy") && gameObject.CompareTag("enemy")) return; //If they're both enemies no interaction
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>(); 
            if(hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (collision.gameObject.CompareTag("enemy") && collision.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                if(collision.gameObject.CompareTag("Player"))
                {
                    if(collision.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        collision.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }
}
