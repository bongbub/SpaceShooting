using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float damage = 20.0f;    //총알 파괴력
    public float force = 1500.0f;   //총알발사 힘
    private Rigidbody rb;   //리지드바디 컴포넌트 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force);  //총알의 전진 방향으로 힘을 가함
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
