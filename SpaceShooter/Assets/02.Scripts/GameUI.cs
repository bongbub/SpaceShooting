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
        DispScore(0);
    }

    public void DispScore(int score)
    {
        totScore += score;
        txtScore.text = "Score <color=#ff0000>"
            + totScore.ToString()
            + "</color>";
    }


    void Update()
    {
        
    }
}
