using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//반드시 필요한 컴포넌트를 명시해 해당 컴포넌트가 삭제되는 것을 방지하는 어트리뷰트
[RequireComponent(typeof(AudioSource))]

public class FireCtrl : MonoBehaviour
{
    //총알 프리펩
    public GameObject bullet;
    //퐁알 발사 좌표
    public Transform firePos;
    //총알에 사용되는 음원
    public AudioClip fireSfx;
    //AudioSource 컴포넌트를 저장할 변수
    private new AudioSource audio;
    //Muzzle Flash 의 MeshRenderer 컴포넌트
    private MeshRenderer muzzleFlash;

    #region 내가 음원 넣으려고 시도했던 흔적
    //private Transform target;
    #endregion

    void Start()
    {
        #region 내가 음원 넣으려고 했던 코드
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        #endregion

        //컴포넌트에 Audio Source 저장
        audio = GetComponent<AudioSource>();

        //FirePos 하위에 있는 MuzzleFlash의 Material 컴포넌트 추출
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        //처음 시작할 때 비활성화
        muzzleFlash.enabled = false;
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
        //Bullet 프리펩을 동적으로 생성
        //firePos 위치값과 로테이션값에서 bullet을 생성하겠다
        Instantiate(bullet, firePos.position, firePos.rotation);

        //총소리 생성
        audio.PlayOneShot(fireSfx, 1.0f);

        //총구 화염 효과 코루틴 함수 호출
        StartCoroutine(ShowMuzzleFlash());


        #region 내가 총알음원 넣으려고 아주 시도햇던 노력...
        //AudioSource.PlayClipAtPoint(sound, target.transform.position);
        #endregion
    }

    IEnumerator ShowMuzzleFlash()
    {
        //오프셋 좌표값을 랜덤 함수로 사용
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        //텍스쳐의 오프셋 값 설정
        muzzleFlash.material.mainTextureOffset = offset;

        //MuzzleFlash의 회전 반경
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);

        //MuzzleFlash의 크기 조절
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        //Muzzle Flash 활성화
        muzzleFlash.enabled = true;
        //0.2 초 동안 대기(정지) 하는 동안 메세지 루프로 제어권 양보
        yield return new WaitForSeconds(0.2f);
        //Muzzle Flash 비활성화
        muzzleFlash.enabled = false;
    }
}
