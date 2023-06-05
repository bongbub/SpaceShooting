using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{

    //몬스터의 상태 정보
    public enum State
    {
        IDLE,
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }
    //몬스터의 현재상태
    //State에 state라는 변수를 선언하고 그 안에 State enum 안에있는 IDLE로 초기화
    public State state = State.IDLE;
    //추적 사정거리
    public float traceDist = 10.0f;
    //공격 사정 거리
    public float attackDist = 2.0f;
    //몬스터 사망 여부
    public bool isDie = false;

    //GameUI에 접근하기 위한 변수
    private GameUI gameUI;


    public GameObject bloodEffect; //프리펩 : 혈흔효과
    public GameObject bloodDecal;   //프리펩 :데칼효과

    //컴포넌트 캐쉬를 처리할 변수
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator anim;


    //Hp
    private int hp = 100;


    //스크립트가 활성될 때 호출 
    //Start() 보다 먼저 호출! 우선순위 높음
    void OnEnable()
    {
        PlayerCtrl.OnPlayerDie += this.OnPlayerDie; //괄호 안썼음 함수 포인터를 등록
    }

    void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;  //함수 등록을 이벤트에서 제거
    }



    void Start()
    {
        //몬스터의 Transform 할당
        monsterTr = this.gameObject.GetComponent<Transform>(); //this.gameObject 생략해도 됨
        //추적 대상인 Player를 FindWithTag로 찾아서 transform컴포넌트를 가져옴
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //NavMeshAgent할당
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();

        //추적 대상의 위치를 설정하면 바로 추적
        //nvAgent.destination = playerTr.position;
        //nvAgent.SetDestination(playerTr.position);

        anim = GetComponent<Animator>();

        //GameUI 게임 오브젝트의 GameUI 스크립트 할당
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();

        //동시에 실행 (상태를 조사, 액션 처리 - 애니메이션, 따라가기)
        //코루틴 함수로 동시에 실행한다는 것이 포인트
        //몬스터의 상태를 체크하는 코루틴 함수를 구동
        StartCoroutine(CheckMonsterState());
        //상태에 따라 몬스터의 행동을 수행하는 코루틴 함수 호출
        StartCoroutine(MonsterAction());

    }

    void Update()
    {
        //nvAgent.destination = playerTr.position;
    }

    IEnumerator CheckMonsterState()  // 거리에따른상태체크
    {
        while (!isDie)//: true 면 무한루프 죽기전까진 
        {
            //0.3초 동안 중지(대기) 하는 동안 제어권을 메세지 루프에게 전달
            yield return new WaitForSeconds(0.3f);

            //몬스터와 주인공 캐릭터 사이의 거리 측정
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (state == State.DIE) yield break;

            //공격 사정거리 안으로 들어왔는지 확인
            if (distance <= attackDist)
            {
                state = State.ATTACK;  //상태전환 -> 공격상태
            }

            //추적 사정거리 범위로 들어왔는지 확인
            else if (distance <= traceDist)
            {
                state = State.TRACE;
            }

            else  //idle 상태로 전환
            {
                state = State.IDLE;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie) //: true 면 무한루프
        {
            yield return new WaitForSeconds(0.3f);
            switch (state)
            {
                case State.IDLE:  //IDLE상태
                    //추적애니메이션 -> 아이들애니메이션
                    anim.SetBool("IsTrace", false); 
                    nvAgent.isStopped = true;  //IDEL상태일 땐 따라가기 멈춤
                    break;

                case State.TRACE:
                    //Animator에 있던 변수명과 같아야함
                    anim.SetBool("IsTrace", true);  //IDLE 애니메이션 -> TRACE 애니메이션
                    anim.SetBool("IsAttack", false); //공격애니메이션 중지
                    //TRACE상태일 땐 따라가기
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false; //플레이어의 위치를 갱신하며 따라가기
                    break;

                case State.ATTACK:
                    anim.SetBool("IsAttack", true);  //공격 애니메이션
                    break;


                //몬스터 사망 -> 죽는 애니메이션
                case State.DIE:
                    isDie = true;
                    nvAgent.isStopped = true;
                    anim.SetTrigger("MonDie");
                    GetComponent<CapsuleCollider>().enabled = false; //콜라이더제거
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnDrawGizmos()
    {
        //추적 사정거리 표시
        if (state == State.TRACE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, traceDist);  //와이어프레임그리기(위치, 반지름)
        }
        if (state == State.ATTACK)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }

    void OnCollisionEnter(Collision coll) //충돌이 됐을 때 호출되는 함수
    {
        if (coll.collider.CompareTag("Bullet")) //총알이랑 부딪혔을 때
        {
            Destroy(coll.gameObject); //총알을 없애주고
            anim.SetTrigger("Hit"); //히트애니메이션

            //혈흔효과(총알 맞은 위치에서)
            Vector3 pos = coll.GetContact(0).point;  //위치저장
            //노멀벡터를 만들어서 실제적으로 부딪혀서 나오는 벡터를 만듦
            Quaternion rot = Quaternion.LookRotation(-coll.GetContact(0).normal);  
            ShowBloodEffect(pos, rot);   //효과가 보여짐(혈흔효과)

            CreateBloodEffect(pos);  //Decal효과를 주는 함수 호출

            //GameUI의 스코어 누적과 스코어 표시 함수 호출
            gameUI.DispScore(100);

            hp -= 10;
            if (hp <= 0)
            {
                state = State.DIE;
            }

        }
    }
    //몸에 혈흔효과
    void ShowBloodEffect(Vector3 pos, Quaternion rot)
    {
        //                               프리펩     위치  회전정보  타입변환
        GameObject blood = Instantiate(bloodEffect, pos, rot) as GameObject;
        Destroy(blood, 1.0f);  //1.0f초 뒤 삭제
    }

    //바닥에혈흔효과
    void CreateBloodEffect(Vector3 pos)
    {
        //                     몬스터위치     +   바닥에서 약간 띄어서
        Vector3 decalPos = monsterTr.position + (Vector3.up * 0.01f);
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));
        GameObject blood = Instantiate(bloodDecal, pos, decalRot) as GameObject;
        float scale = Random.Range(1.5f, 3.5f);
        blood.transform.localScale = Vector3.one * scale;   //one=(1,1,1)
        Destroy(blood, 2.0f);
    }


    //플레이어가 죽었을 때 몬스터가 춤 추는 행동
    void OnPlayerDie()
    {
        Debug.Log("플레이어 사망");
        StopAllCoroutines();  //모든 코루틴중지
        nvAgent.isStopped = true;  //따라가기 중지
        //트리거를 발생시켜서 죽었다고 하는 애니메이션 연출

        anim.SetFloat("Speed", Random.Range(0.8f, 1.2f));
        anim.SetTrigger("IsDie");
    }
}