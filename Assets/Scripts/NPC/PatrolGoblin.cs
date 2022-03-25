using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGoblin : Goblin
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;

    public float CloseToPoint;

    public bool LinearPatrol;
    private bool reverse = false;

    public override void CheckDistance()
    {
        anim.SetBool("trigger", true); //always walking

        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius) //Chasing player
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                //ChangeState(EnemyState.walk);
            }
        }
        else //Not chasing player (patrol)
        {
            if(Vector3.Distance(transform.position, path[currentPoint].position) > CloseToPoint)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
            }
            else
            {
                ChangeGoal(); 
            }
          
        }
    }

    private void ChangeGoal()
    {

        if (LinearPatrol == false)
        {
            if (currentPoint == path.Length - 1) //We reach last point
            {
                currentPoint = 0;
                currentGoal = path[0];
            }
            else
            {
                currentPoint++;
                currentGoal = path[currentPoint];
            }
        }
        else
        {
            if (currentPoint == path.Length - 1) //We reach last point
            {
                reverse = true; //reverse patrol
                currentPoint--;
                currentGoal = path[currentPoint];

            }
            else
            {
                if (reverse == false) { currentPoint++; } else { currentPoint--; }
                currentGoal = path[currentPoint];

                if (reverse == true && currentPoint == 0)
                {
                    reverse = false;
                }
            }
        }
    }
}
