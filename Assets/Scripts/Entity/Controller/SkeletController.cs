using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletController : MonoBehaviour
{
    public GameObject textCanvas; // 문구 UI 오브젝트 (Inspector에서 할당)
    public DialogueManager dm; // DialogueManager를 Inspector에서 할당

    public string sceneName; // 이동할 씬의 이름 (Inspector 창에서 설정)
    bool isInteract = false; // 상호작용 가능한지
    bool isDialogueActive = false; // 대화가 활성화되었는지

    void Start()
    {
        dm.OnDialogueEnd += AfterTalking; // DialogueManager의 대화 종료 이벤트에 콜백 함수 등록
    }

    void Update()
    {
        // 플레이어가 트리거 영역 안에 있는 동안 설정된 상호작용 키 (F)를 누르면
        if (isInteract && !isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            Interactive();
        }

    }

    void OnTriggerEnter2D(Collider2D other) // 플레이어가 트리거에 들어왔을 때 실행되는 함수
    {
        if (other.CompareTag("Player")) // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
        {
            TextMeshProUGUI tmpText = textCanvas.GetComponent<TextMeshProUGUI>();

            textCanvas.SetActive(true); // 상호작용 텍스트 출력
            isInteract = true; // 상호작용 가능
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
        {
            textCanvas.SetActive(false); // 상호작용 텍스트 미출력
            isInteract = false; // 상호작용 불가

            // 대화 초기화
            dm.SettingUI(false);
            dm.currentTextIndex = 0;
            dm.isWaitingInput = false;
            isDialogueActive = false;
        }
    }

    void Interactive() // 상호작용
    {
        isInteract = true;
        isDialogueActive = true; // 대화가 시작되었음을 표시

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.CompareTag("Player")) // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
        {
            dm.ShowDialogue(); // 대화 시작

            PlayerPositionManager.Instance.SetLastPositionAndScene(
                player.transform.position, SceneManager.GetActiveScene().name); // 현재 위치와 씬 이름 저장
        }
    }

    void AfterTalking()
    {
        isDialogueActive = false; // 대화가 끝났음을 표시
        dm.gameObject.SetActive(false); // 씬 로드 전에 DialogueManager 오브젝트를 비활성화

        if (isInteract && !isDialogueActive && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(sceneName); // 상호작용(미니게임으로 이동)
        }
    }
}
