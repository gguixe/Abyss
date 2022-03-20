using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            this.gameObject.SetActive(false); //Destroy
        }
    }

    public void Knock(Rigidbody2D myRigibody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigibody, knockTime));
        TakeDamage(damage);
    }
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null) //check it doesn't die
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.GetComponent<Enemy>().currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
            //print(enemy.velocity);
        }
    }
}
