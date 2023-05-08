using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire(); // 마우스를 눌렀을 때 fire()함수 호출
        }
    }

    void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        //firePos 위치값과 로테이션값에서 bullet을 생성하겠다
    }
}
