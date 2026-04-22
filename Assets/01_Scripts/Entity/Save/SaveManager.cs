using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class SaveManager : Singleton<SaveManager>
{
    public int CurrentSlot { get; private set; } = -1;

    private const string SaveString = "SystemData.json";

    protected override void Awake()
    {
        CurrentSlot = GetLastPlayedSlot();
    }

    #region 경로 관리 메서드

    private string GetSystemPath()
    {
        return Path.Combine(Application.dataPath, SaveString);
    }

    /// <summary>
    /// 슬롯 번호를 넣으면 해당 저장 파일의 경로를 뱉음
    /// </summary>
    /// <param name="slot">슬롯 번호</param>
    /// <returns></returns>
    private string GetPath(int slot)
    {
        return Path.Combine(Application.dataPath, $"SaveSlot_{slot}.json");
    }

    #endregion

    #region 시스템 데이터 관리

    /// <summary>
    /// 현재 슬롯 설정
    /// </summary>
    /// <param name="slot">현재 슬롯</param>
    public void SetCurrentSlot(int slot)
    {
        CurrentSlot = slot;

        SystemData sysData = new SystemData();
        sysData.lastPlayedSlot = CurrentSlot;

        string json = JsonConvert.SerializeObject(sysData, Formatting.Indented);
        File.WriteAllText(GetSystemPath(), json);
    }

    /// <summary>
    /// 마지막에 플레이 했던 슬롯 불러오기
    /// </summary>
    /// <returns></returns>
    public int GetLastPlayedSlot()
    {
        string path = GetSystemPath();
        if (File.Exists(path))
        {
            // SystemData.json 파일이 있으면 열어서 번호를 읽어옴
            string json = File.ReadAllText(path);
            SystemData sysData = JsonConvert.DeserializeObject<SystemData>(json);
            return sysData.lastPlayedSlot;
        }

        return 1; // 파일이 아예 없으면 (첫 접속이면) 1번 반환
    }

    #endregion

    #region 데이터 저장, 불러오기 관련

    /// <summary>
    /// 데이터 저장
    /// </summary>
    /// <param name="slot">저장할 슬롯</param>
    /// <param name="data">저장시킬 데이터</param>
    public async Task Save(int slot, SaveData data)
    {
        // Formatting.Indented를 쓰면 메모장으로 열었을 때 예쁘게 줄바꿈이 됨
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        await System.IO.File.WriteAllTextAsync(GetPath(slot), json);

        Debug.Log("비동기 저장");
    }

    /// <summary>
    /// 데이터 불러오기
    /// </summary>
    /// <param name="slot">불러올 슬롯</param>
    /// <returns></returns>
    public async Task <SaveData> Load(int slot)
    {
        string path = GetPath(slot);
        if (System.IO.File.Exists(path))
        {
            string json = await System.IO.File.ReadAllTextAsync(path);
            Debug.Log("비동기 로드");
            return JsonConvert.DeserializeObject<SaveData>(json);
        }

        return null;
    }

    /// <summary>
    /// 슬롯 데이터 삭제
    /// </summary>
    /// <param name="slot">삭제할 슬롯</param>
    public void Delete(int slot)
    {
        string path = GetPath(slot);
        if (File.Exists(path)) File.Delete(path);
    }

    /// <summary>
    /// 데이터가 존재하는지
    /// </summary>
    /// <param name="slot">확인할 슬롯</param>
    /// <returns></returns>
    public bool HasData(int slot)
    {
        return File.Exists(GetPath(slot));
    }

    #endregion

}
