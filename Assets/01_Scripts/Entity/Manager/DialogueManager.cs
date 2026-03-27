using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    private const string Dialogue_CSVString = "Dialogue_CSV";

    private Dictionary<string, List<string>> _npcDialogues = new Dictionary<string, List<string>>();
    private Dictionary<string, string> _npcDisplayNames = new Dictionary<string, string>();

    private bool _isLoad = false;

    private const string UnknownString = "알 수 없음";

    protected override void Awake()
    {

    }

    private void Start()
    {
        LoadDialogue();
    }

    /// <summary>
    /// 대화 CSV 불러오기
    /// </summary>
    public void LoadDialogue()
    {
        if (_isLoad) return; // 중복 실행 방지

        TextAsset csvfile = Resources.Load<TextAsset>(Dialogue_CSVString);
        string[] lines = csvfile.text.Split('\n'); // 줄바꿈으로 행 분리

        // i = 0번 줄은 헤더이기에 1부터 시작
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] row = lines[i].Split(','); // 쉼표로 열 분리

            DialogueData data = new DialogueData
            {
                id = row[0].Trim(),
                name = row[1].Trim(),
                context = row[2].Replace("\"", "").Trim(), // 큰따옴표 제거
            };

            if (!_npcDialogues.ContainsKey(data.id))
            {
                _npcDialogues[data.id] = new List<string>();
                _npcDisplayNames[data.id] = data.name;
            }

            _npcDialogues[data.id].Add(data.context);
        }

        _isLoad = true;
    }

    /// <summary>
    /// 해당 Npc 대사 불러오기
    /// </summary>
    /// <param name="npcID">Npc 오브젝트 이름</param>
    /// <returns>Npc 한국어 이름</returns>
    public List<string> GetDialogueList(string npcID, out string koreanName)
    {
        koreanName = UnknownString;

        if (_npcDialogues.ContainsKey(npcID))
        {
            if (_npcDisplayNames.ContainsKey(npcID))
            {
                koreanName = _npcDisplayNames[npcID];
            }

            Debug.Log("정상적으로 불러옴");
            return _npcDialogues[npcID];
        }

        Debug.Log("null");
        return null;
    }
}