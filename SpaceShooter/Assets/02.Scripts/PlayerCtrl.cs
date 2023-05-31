using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f;   //전진
    private float v = 0.0f;   //좌후진
    //전진 좌후진 값을 계산하기 위해 h, v를 만듦
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    //컴포넌트를 캐시처리할 변수
    private Transform tr;
    // Animation 컴포넌트를 저장할 변수  
    private Animation anim;

    private float turnSpeed = 80.0f;
    private readonly float initHp = 100.0f;
    public float currHp = 100.0f;



    //델리게이트 객체 선언
    public delegate void PlayerDieHandler();
    //이벤트 객체 선언
    public static event PlayerDieHandler OnPlayerDie;
    //이벤트는 return(반환) 타입이 delegate타입 - >무조건 delegate를 선언해줘야함




    /*void Start()
    {
        tr = GetComponent<Transform>();  //객체 할당되어서 트랜스폼이라는 컴포넌트를 사용할 수 있음
        anim = GetComponent<Animation>();  //플레이어에 추가된 animation 컴포넌트를 가져오는 작업
    }
    */

    IEnumerator Start()  //코루틴 형식의 Start() 함수 -> 게임엔진에서 호출을 자동으로 함
    {
        tr = GetComponent<Transform>();  //객체 할당되어서 트랜스폼이라는 컴포넌트를 사용할 수 있음
        anim = GetComponent<Animation>();  //플레이어에 추가된 animation 컴포넌트를 가져오는 작업
        anim.Play("Idle");   //플레이어의 애니메이션을 아이들로 실행(0.3초)

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);   //0.3초뒤에 turnSpeed를 80으로
        turnSpeed = 80.0f;
    }


    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");   //-0.1 ~ 0.1까지

        //Debug.Log("H= " + h.ToString());
        //Debug.Log("V= " + v.ToString());

        //tr.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime);  -->전진 후진만 됨


        //방향을 만들어주기 
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //단위 벡터 * v + 단위 벡터 * h -> 방향이 나옴
        //결과(회전은아직 하지 않음)->

        //Translate(이동방향 * 속력 * Time.deltaTime)
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        //tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
        tr.Rotate(Vector3.up * Time.deltaTime * turnSpeed * Input.GetAxis("Mouse X"));
        //turnSpeed 와 rotSpeed의 차이는 속도가 80이냐 100이냐차이밖에없음
        //코루틴Start()함수때문에 0.3초뒤에 turnSpeed가 80이 반영됨


        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        if (v >= 0.1f) //up
        {
            anim.CrossFade("RunF", 0.25f);  //25%만 블렌딩 실행 하는 것
        }
        else if (v <= -0.1f) //down
        {
            anim.CrossFade("RunB", 0.25f);  //후진애니메이션
        }
        else if (h >= 0.1f)  //Right
        {
            anim.CrossFade("RunR", 0.25f);  //오른쪽 이동 애니메이션
        }
        else if (h <= -0.1f) //left
        {
            anim.CrossFade("RunL", 0.25f);   //왼쪽 이동 애니메이션
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);   //정지시 Idle 애니메이션 실행
        }
    }

    //몬스터 양 팔에 istrigger세팅되어있음
    void OnTriggerEnter(Collider other)
    {
        if (currHp >= 0.0f && other.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {

        /*    성능에 부담이 되는 코딩이니까 delegate와 event를 이용해서 해결
        //Debug.Log("Die!!!!");
        //플레이어가 죽었음을 몬스터가알아야함
        //가져오는 값이 GameObject자체임 -> 직관적인 코딩이지만, 덩어리가 커서 성능이 다운 
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach(GameObject monster in monsters)
        {
            //메세지를 뿌리는 역할(반환값은 받지 않음)
            monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
            //OnPlayerDie 는 몬스터Ctrl 스크립트 안에 들어있는 함수를 가르키는 포인터
            //만약 OnPlayerDie() 였다면 호출함수
            //SendMessageOptions.DontRequireReceiver 함수가없어도 메세지를 받지않음
        }
    }
        */

        //델리게이트 객체 OnPlayerDie => 괄호가없을 땐 이벤트, 괄호가 있을 땐 이벤트 호출
        //이벤트발생
        OnPlayerDie(); //델리게이트 객체 (함수 포인터)
    }
}