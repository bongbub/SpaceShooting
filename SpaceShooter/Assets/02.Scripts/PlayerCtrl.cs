using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private float h = 0.0f; //전진
    private float v = 0.0f;  //좌후진
    //전진 좌후진 값을 계산하기 위해 h, v를 만듦
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    //컴포넌트를 캐시처리할 변수
    private Transform tr;
    // Animation 컴포넌트를 저장할 변수  
    private Animation anim;

    void Start()
    {
        tr = GetComponent<Transform>();  //객체 할당되어서 트랜스폼이라는 컴포넌트를 사용할 수 있음
        anim = GetComponent<Animation>();  //플레이어에 추가된 animation 컴포넌트를 가져오는 작업
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
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        if (v >= 0.1f) //up
        {
            anim.CrossFade("RunF", 0.25f);  //25%만 블렌딩 실행 하는 것
        }
        else if(v<=-0.1f) //down
        {
            anim.CrossFade("RunB", 0.25f);  //후진애니메이션
        }
        else if(h>=0.1f)  //Right
        {
            anim.CrossFade("RunR", 0.25f);  //오른쪽 이동 애니메이션
        }
        else if(h<=-0.1f) //left
        {
            anim.CrossFade("RunL", 0.25f);   //왼쪽 이동 애니메이션
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);   //정지시 Idle 애니메이션 실행
        }
    }
}
