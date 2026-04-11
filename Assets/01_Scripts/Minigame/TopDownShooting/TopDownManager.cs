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

        OnScoreChanged?.Invoke(CurrentScore);
    }
}
