[System.Serializable]
public class SaveData
{
    public int tdScore; // 탑다운 스코어
    public int planeScore; // 비행기 스코어

    // 새 게임을 시작할 때 부여할 '초기값' 세팅
    public SaveData()
    {
        tdScore = 0;
        planeScore = 0;
    }
}

[System.Serializable]
public class SystemData
{
    public int lastPlayedSlot; // 마지막으로 플레이 한 슬롯

    public SystemData()
    {
        lastPlayedSlot = 1; // 기본값
    }
}
