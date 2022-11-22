using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.collider.CompareTag("a"))
        {
            Debug.Log("a");

        }
        else if (target.collider.CompareTag("b"))
        {
            Debug.Log("b");

        }
    }
}
