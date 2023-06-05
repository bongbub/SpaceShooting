using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    //Text UI 항목 연결을 위한 변수
    public Text txtScore;
    //누적 점수를 기록하기 위한 변수
    private int totScore = 0;

    void Start()
    {

        //처음 실행 후 저장된 스코어 정보 로드
        totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);

        DispScore(0);
    }

    //정보 누적 및 화면 표시
    public void DispScore(int score)
    {
        totScore += score;
        txtScore.text = "Score <color=#ff0000>"
            + totScore.ToString()
            + "</color>";

        //스코어 저장
        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }


    void Update()
    {
        
    }
}
