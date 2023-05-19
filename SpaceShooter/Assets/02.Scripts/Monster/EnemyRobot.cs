using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRobot : MonoBehaviour
{

    //외부세팅변수

    //내부세팅변수
    private Transform target;
    private int rotAngle;
    private float amtToRot;
    private float distance;
    private Vector3 direction;
    private float moveSpeed;

    private NavMeshAgent nvAgent;

    //맞은 횟수 저장
    private int hitCt = 0;
    //폭발 효과 넣을 컴포넌트
    public GameObject expEffect;
    //위치
    private Transform tr;
    private Rigidbody rb;
    //public new AudioSource audio;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moveSpeed = 1.0f;
        rotAngle = 60;

        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        //audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        direction = target.transform.position - this.transform.position;
        distance = Vector3.Distance(target.transform.position, this.transform.position);
        //Debug.Log(direction);

        if (distance < 10.0f)
        {
            nvAgent.destination = target.position;
        }
    }
    //충돌 했을 때 
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Bullet"))
        {
            Debug.Log("col");
            if (++hitCt == 3)
            {
                Explosion();
                Debug.Log("3");
            }
        }
    }
    void Explosion()
    {
        GameObject boom = Instantiate(expEffect, tr.position,Quaternion.identity);
        //폭발 효과 후 파티클 제거
        Destroy(boom, 3.0f);
        //audio.PlayOneShot();
        Destroy(this.gameObject);
    }
}
