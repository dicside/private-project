using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Transform explosionPrefab;
    public float bulletSpeed;
    private Vector3 oldPosition;
    private Vector3 currentPosition;
    private double oldvelocity;
    private double currentvelocity;
    private double acceleration;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        oldPosition = transform.position;
        oldvelocity = bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        currentPosition = transform.position;
        var dis = (currentPosition - oldPosition);
        var distance = Math.Sqrt(Math.Pow(dis.x, 2) + Math.Pow(dis.y, 2) + Math.Pow(dis.z, 2));
        currentvelocity = distance / Time.deltaTime;
        acceleration = Math.Abs((currentvelocity - oldvelocity) / Time.deltaTime);
        Debug.Log(acceleration);
        Debug.Log(currentvelocity);
        oldPosition = currentPosition;
        oldvelocity = currentvelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("a"))
        {
            Destroy(gameObject, .5f);
            ContactPoint contact = collision.contacts[0];

            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        }
        else if (collision.collider.CompareTag("b"))
        {
            
            ContactPoint contact = collision.contacts[0];
            Quaternion rot  = Quaternion.FromToRotation(Vector3.up, contact.normal);;

            if (rot.y<= 0.65|| rot.y <= -0.65)
            {

            }
            else
            {
                Destroy(gameObject, .0f);
            }
        }
    }

}
