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
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    public void Knock(Rigidbody2D myRigibody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigibody, knockTime));
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
