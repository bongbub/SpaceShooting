//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MonKingCtrl : MonoBehaviour
//{
//    private Transform target;      
//    private int rotAngle;
//    private float amtToRot;
//    private float distance;
//    private Vector3 direction;
//    private float moveSpeed;        
//    private int hitCount;
//    private Transform tr;
//    //시간 담당 변수
//    float currTime;
    
//    //public new AudioSource audio;   
//    public GameObject expEffect;

//    private UnityEngine.AI.NavMeshAgent nvAgent;



//    void Start()
//    {
//        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
//        moveSpeed = 1.0f; 
//        rotAngle = 60;

//        tr = GetComponent<Transform>();
//        nvAgent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

//        gameObject.SetActive(false);

//    }


//    void Update()
//    {
//        direction = target.transform.position - this.transform.position;
//        distance = Vector3.Distance(target.transform.position, this.transform.position);
//        if (distance < 10.0f) 
//        {
//            nvAgent.destination = target.position;
//        }
//        //시간이 흐르게 함
//        currTime += Time.deltaTime;
//        if (currTime > 20)
//        {
            
//            Debug.Log("spawn monster");
//            //오브젝트를 몇 초마다 생성할 것인지 랜덤하게 생성
//            float newX = Random.Range(-10f, 10f), newY = Random.Range(-50f, 50f), newZ = Random.Range(-100f, 10f);
//            //생성할 오브젝트 불러오기
//            GameObject monster = Instantiate(gameObject);
//            //불러온 옵젝을 랜덤 생성 좌표값으로 위치 옮기기
//            //monster.transform.position = new Vector3(newX, newY, newZ);
//            gameObject.SetActive(true);
//            //시간을 0으로 돌려서 반복
//            currTime = 0;
//        }
//    }


//    void OnCollisionEnter(Collision col)
//    {
//        if (col.collider.CompareTag("Bullet"))
//        {
//            if(++hitCount == 30)
//            {
//                Explosion();
//            }
//        }
//    }

//    void Explosion()
//    {
     
//        GameObject boom = Instantiate(expEffect, tr.position, Quaternion.identity);
//        Destroy(boom, 5.0f);
//        Destroy(this.gameObject);
//        Debug.Log("GameClear");
//    }
//}
