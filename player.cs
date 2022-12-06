using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class player : MonoBehaviour
{
    public float playerMoveSpeed = 5;
    public float gravity = -20;
    float yVelocity;
    public GameObject bulletFac;
    public Transform firePos;
    private Vector3 targetPosition;
    CharacterController cc;

    public GameObject bullet;
    public Transform target;
    public Transform shootPoint;
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
        if (Input.GetMouseButtonDown(1))
        {
            LaucherProjecttile();

        }

    }
    private void Fire()
    {
        GameObject bullet = Instantiate(bulletFac);
        bullet.transform.position = firePos.position;
        bullet.transform.forward = firePos.forward;
    }
    void LaucherProjecttile()
    {
        Vector3 Vo = CalculateVelcoity(target.position, transform.position, 1f);
        Rigidbody obj = Instantiate(bullet, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        obj.velocity = Vo;
        //setTrajectoryPoints(transform.position, Vo);
        DrawPath(Vo);
    }

    //이 방법은 목표 벡터와 원점의 시작점이 필요합니다.
    //time : 비행시간
    Vector3 CalculateVelcoity(Vector3 target, Vector3 origin, float time)
    {
        //define the distance x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance; //x와z의 평면이면 기본적으로 거리와 같은 벡터
        distanceXZ.y = 0f;//y는 0으로 설정
        Debug.Log(distance.x);
        Debug.Log(distance.y);
        Debug.Log(distance.z);
        //create a float the represent our distance
        float Sy = distance.y;//세로 높이의 거리를 지정
        float Sxz = distanceXZ.magnitude;

        //속도 계산
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        //계산으로 인해 두축의 초기 속도 가지고 새로운 벡터를 만들수 있음
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }
    void DrawPath(Vector3 velocity)
    {
        Vector3 previousDrawPoint = transform.position;
        int resolution = 30;
        //lineRenderer.positionCount = resolution;
        for (int i = 1; i <= resolution; i++)
        {
            //float simulationTime = i / (float)resolution * launchData.timeToTarget;
            float simulationTime = i / (float)resolution * 1f;

            Vector3 displacement = velocity * simulationTime + Vector3.up * Physics.gravity.y * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = transform.position + displacement;
            DebugExtension.DebugPoint(drawPoint, 1, 1000f);//유니티 에셋스토어 Debug Extension
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            //lineRenderer.SetPosition(i - 1, drawPoint);
            previousDrawPoint = drawPoint;
        }
    }
}
