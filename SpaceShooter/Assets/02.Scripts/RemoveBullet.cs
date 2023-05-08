using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {

         
                //파티클 생성
                ContactPoint cp = coll.GetContact(0);  //파티클 생성을 위한 위치 정보 구하기
                Quaternion rot = Quaternion.LookRotation(-cp.normal);  //파티클 생성을 위한 회전 정보 구하기
                                                                       //회전값을 실수값으로 만들어서 적용
                GameObject spark = Instantiate(sparkEffect, cp.point, rot); //cp.point 위치와 회전각도로 sparkEffect 생성

            
                //총알 삭제
                Destroy(coll.gameObject);
            
            //파티클 삭제
            Destroy(spark, 1.0f);  //총알 먼저 삭제하고 파티클 삭제
        }
    }
}
