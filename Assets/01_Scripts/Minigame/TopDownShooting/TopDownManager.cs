using System;
using UnityEngine;

public class TopDownManager : MonoBehaviour
{
    public static TopDownManager Instance { get; private set; }

    public int CurrentScore { get; private set; } // 현재 스코어
    public int HighScore { get; private set; } // 최고 스코어

    public event Action<int> OnScoreChanged; // 점수UI 변경 이벤트

    private void Awake()
    {
        if (Instance == null) Instance = this;

        CurrentScore = 0;

        UIManager.Instance.OnTopDownGameUI();
    }

    /// <summary>
    /// 적 저치시 점수 추가
    /// </summary>
    /// <param name="score">해당 적 점수</param>
    public void AddKillScore(EnemyType type)
    {
        int scoreToAdd = 0;

        switch (type)
        {
            case EnemyType.Normal:
                scoreToAdd = 5;
                break;
            case EnemyType.Fast:
                scoreToAdd = 10;
                break;
            case EnemyType.Tank:
                scoreToAdd = 20;
                break;
        }

        CurrentScore += scoreToAdd;
        Debug.Log($"{type}처치 / {scoreToAdd} 획득 / 현재 점수 {CurrentScore}");

        if (HighScore < CurrentScore) HighScore = CurrentScore;

        OnScoreChanged?.Invoke(CurrentScore);
    }

    /// <summary>
    /// 플레이어가 죽어서 게임 오버일 때
    /// </summary>
    public void GameOver()
    {
        UIManager.Instance.OnGameOverUI();

        int mySlot = SaveManager.Instance.CurrentSlot;

        if (mySlot != -1) // 만약 슬롯이 정상적으로 세팅되어 있다면 (-1이 아니라면)
        {
            // 해당 슬롯의 데이터를 불러와서, 최신 점수로 덮어씌우고 다시 저장
            SaveData data = SaveManager.Instance.Load(mySlot);
            // 최고 점수 갱신 로직 (Mathf.Max를 쓰면 둘 중 큰 값을 알아서 넣어줌)
            data.tdScore = Mathf.Max(data.tdScore, CurrentScore);
            SaveManager.Instance.Save(mySlot, data); // 파일로 최종 저장
            Debug.Log($"{mySlot}번 슬롯에 최고 점수 {data.tdScore} 저장 완료!");
        }
    }
}
