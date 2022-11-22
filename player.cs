using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float playerMoveSpeed = 5;
    public float gravity = -20;
    float yVelocity;
    public GameObject bulletFac;
    public Transform firePos;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * playerMoveSpeed * Time.deltaTime);
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletFac);
        bullet.transform.position = firePos.position;
        bullet.transform.forward = firePos.forward;



    }
}
