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
        pivot = transform;
        //left_hand.transform.position += Vector3.up * radius;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 orbVector = Camera.main.WorldToScreenPoint(transform.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

        pivot.position = transform.position;
        pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //print(pivot.rotation);
        print(angle);

    }
}
