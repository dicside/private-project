using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occupation : MonoBehaviour
{
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "cc")
        {

        }
        else
        {
            time = 0;
        }
    }
}
