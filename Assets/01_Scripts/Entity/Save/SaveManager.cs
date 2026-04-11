using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : Singleton<SaveManager>
{
    protected override void Awake()
    {

    }

    // 슬롯 번호를 넣으면 해당 저장 파일의 경로를 뱉음
    private string GetPath(int slot)
    {
        return Path.Combine(Application.persistentDataPath, $"SaveSlot_{slot}.json");
    }

    /// <summary>
    /// 데이터 저장
    /// </summary>
    /// <param name="slot">저장할 슬롯</param>
    /// <param name="data">저장시킬 데이터</param>
    public void Save(int slot, SaveData data)
    {
        // Formatting.Indented를 쓰면 메모장으로 열었을 때 예쁘게 줄바꿈이 됨
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(GetPath(slot), json);
        Debug.Log($"{slot}번 슬롯 저장");
    }

    /// <summary>
    /// 데이터 불러오기
    /// </summary>
    /// <param name="slot">불러올 슬롯</param>
    /// <returns></returns>
    public SaveData Load(int slot)
    {
        string path = GetPath(slot);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log($"{slot}번 슬롯 로드");
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
        Debug.Log($"{slot}번 슬롯 삭제");
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
}
