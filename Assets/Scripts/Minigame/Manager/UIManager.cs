using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI startText;

    public void Start()
    {
        restartText.gameObject.SetActive(false); // 처음에는 "다시 시작" 텍스트를 숨겨둠
    }

    public void SetRestart() // 게임 오버 상태에서 다시 시작 메시지를 보이게 함
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score) // 현재 점수를 UI에 업데이트
    {
        scoreText.text = score.ToString(); // 정수를 문자열로 변환하여 출력
    }
}
