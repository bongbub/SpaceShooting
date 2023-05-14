using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform targetTr;  //따라가야 할 대상 
    private Transform camTr; //카메라의 위치 업데이트

    //카메라의 위치 범위 설정
    [Range(2.0f, 20.0f)]  //밑은 영향을 받게 되어있음
    public float distance = 10.0f; //위의 제약 조건에 따라 조정을 해도 제한이 걸려있음

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;

    //damping용 변수만들기
    private Vector3 velocity = Vector3.zero;
    public float damping = 10.0f;
    public float targetOffset = 2.0f;

    void Start()
    {
        camTr = GetComponent<Transform>();  
        //private로 할당해줬기 때문에  여기서 할당

    }

    void Update()
    {
        
    }
    
  
    void LateUpdate()
    {

        #region 방법1
        //카메라의 위치 설정
        camTr.position = targetTr.position
            + (-targetTr.forward * distance)
            + (Vector3.up * height); 
        //카메라가 조금 숙이는 것 설정
        camTr.LookAt(targetTr.position);
         
        #endregion


        #region 방법2-가장많이쓰임
        
        //카메라의 위치 pos로 잡음 slerp 함수 떄문에 윗함수와 느낌이 다르게 쫓아감
        /*
        Vector3 pos = targetTr.position
            + (-targetTr.forward * distance)
            + (Vector3.up * height);
        camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime);
        */
        #endregion


           /*
        #region 방법3 damping
        //추적할 대상의 뒤쪽으로 distance만큼 이동
        //높이를 height만큼 이동
        Vector3 pos = targetTr.position //플레이어위치
            + (-targetTr.forward * distance)  //플레이어로부터 거리
            + (Vector3.up * height);    //(0,2,0)
        camTr.position = Vector3.SmoothDamp(
            camTr.position,         //시작위치
            pos,                    //목표위치
            ref velocity,           //현재속도
            damping);               //목표 위치까지의 도달 시간
        //카메라를 피벗좌표를향해 회전
        //camTr.LookAt(targetTr.position);
        #endregion
        */


        //카메라를 피벗 좌표를 향해 회전 (+Offset을 사용해 시야각을 넓힘)

        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));


    }
}


