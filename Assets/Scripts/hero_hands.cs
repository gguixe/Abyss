using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_hands : MonoBehaviour
{

    public GameObject left_hand;
    public GameObject right_hand;

    //public float radius;
    private Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 orbVector = Camera.main.WorldToScreenPoint(transform.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

        if(angle < 90 && angle > -90) //Angulos de la derecha
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else //Angulos de la izquierda
        {
            transform.rotation = Quaternion.AngleAxis(180+angle, Vector3.forward);
            //right_hand.transform.rotation = Quaternion.Inverse(right_hand.transform.rotation); //does nothing?
        }

        //print(angle);

    }
}
