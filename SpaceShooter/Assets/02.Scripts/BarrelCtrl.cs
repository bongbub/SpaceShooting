using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //폭발 효과 파티클 연결 함수
    public GameObject expEffect;
    //컴포넌트 저장변수
    private Transform tr;
    private Rigidbody rb;
    //총알 맞은 횟수 누적시키는 변수
    private int hitCount = 0;

    #region 텍스쳐적용
    //무작위로 적용할 텍스쳐 배열
    public Texture[] textures;
    //하위에 있는 Mesh Renderer 컴포넌트를 저장할 변수          //!!!!!메쉬 렌더러로 접근해서 
    private new MeshRenderer renderer;
    #endregion


    //폭발 반경
    public float radius = 10.0f;


    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        //하위에 있는 MeshRenderer 컴포넌트를 추출
        //여러 객체를 가져올 떈 GetComponenet's' s가 붙음!
        //지금은 단일 객체를 가져오는 거니까 s가 붙지않음
        renderer = GetComponentInChildren<MeshRenderer>();       //!!!!!객체 받아오고 굉장히 중요 기말출제

        //난수 발생
        int idx = Random.Range(0, textures.Length);        //!!!!!머 이것도 중요
        //텍스쳐 저장
        renderer.material.mainTexture = textures[idx];      //!!!!!객체로 받아와서 일케 일케 쓰는거 중요!
                                                            //!!!!!공식이라고 생각하면서 사용법 익히기
    }


    //충돌 시 발생하는 콜백함수
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            //총알 맞은 횟수 증가 시키고 3회 이상이면 제거
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }
    //드럼통 폭발 함수
    void ExpBarrel()
    {
        //폭발효과 파티클 생성
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        //폭발 효과 파티클 5초 후 제거
        Destroy(exp, 3.0f);

        #region 폭발효과파티클 ~> 연속폭발하느라 주석
        //Rigidbody 컴포넌트의 mass를 1.0으로 수정해서 무게를 가볍게 함
        //rb.mass = 1.0f;
        //위로 솟구치는 힘 가하기
        //rb.AddForce(Vector3.up * 1500.0f);
        #endregion

        //간접 폭발력 전달
        IndirectDamage(tr.position);
        //3초 후 드럼통 제거
        Destroy(gameObject, 3.0f);
    }


    //폭발력을 주변에 전달하는 함수
    void IndirectDamage(Vector3 pos)
    {
        //주변에 있는 드럼통을 모두 추출
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1 << 7);    //!!!!!!이것도 굉장히 중요한 구문

        foreach(var coll in colls)
        {
            //폭발 범위에 포함된 드럼통의 Rigidbody 컴포넌트 추철
            rb = coll.GetComponent<Rigidbody>();
            //드럼통의 무게 가볍게 하기
            rb.mass = 1.0f;
            //freezeRotation 제한 값 해제
            rb.constraints = RigidbodyConstraints.None;
            //폭발력 전달
            rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f);
        }
    }

    void Update()
    {
        
    }
}
