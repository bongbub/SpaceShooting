using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region 설정한 콜라이더 범위 내에서 랜덤한 위치의 Vector3값 반환
    public GameObject rangeObject;
    BoxCollider rangeCollider;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        //콜라이더 사이즈를 가져오는 bound.size사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPosition = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPosition;
        return respawnPosition;
    }
    #endregion

    #region 오브젝트 1초마다 소환
    //소환할 오브젝트
    public GameObject MonsterPref;
    private void Start()
    {
        StartCoroutine(RandomRespawn_Coroutine());
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            //생성 위치 부분에 위에서 만든 함수 Return_RandomPosition() 함수 대입
            GameObject instantMon = Instantiate(MonsterPref, Return_RandomPosition(), Quaternion.identity);
        }
    }
    #endregion

    //public Transform[] points;  //몬스터가 출현할 위치를 담을 배열
    //public GameObject monsterPref;   //몬스터 프리펩

    //public float createTime;   //몬스터 발생주기
    //public int maxMonster = 5;    //몬스터의 최대 발생 개수
    //public bool isGameOver = false;    //게임 종료 여부

    //void Start()
    //{
    //    //하이라키 뷰의 SpawnPoint를 찾아 하위에 있는 모든 Transform 컴포넌트를 찾아옴
    //    points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

    //    if (points.Length > 0)
    //    {
    //        //몬스터 생성 코루틴 함수 호출
    //        StartCoroutine(this.CreateMonster());
    //    }
    //}


    //IEnumerator CreateMonster()
    //{
    //    //게임 종료때까지 무한 루프
    //    while (!isGameOver)
    //    {
    //        int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;

    //        if (monsterCount < maxMonster)
    //        {
    //            yield return new WaitForSeconds(createTime);

    //            //블규칙적인 위치 산출
    //            int idx = Random.Range(1, points.Length);
    //            //몬스터의 동적 생성
    //            Instantiate(monsterPref, points[idx].position, points[idx].rotation);
    //        }
    //        else
    //        {
    //            yield return null;
    //        }
    //    }
    //}



    //void Update()
    //{

    //}
}
