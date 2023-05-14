using System.Collections;
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


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moveSpeed = 1.0f;
        rotAngle = 60;

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        direction = target.transform.position - this.transform.position;
        distance = Vector3.Distance(target.transform.position, this.transform.position);
        Debug.Log(direction);

        if (distance < 20.0f)
        {
            nvAgent.destination = target.position;
        }
    }
}
