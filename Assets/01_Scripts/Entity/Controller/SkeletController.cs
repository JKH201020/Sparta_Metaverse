using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletController : MonoBehaviour
{
    #region 기존 코드

    //public GameObject textCanvas; // 문구 UI 오브젝트 (Inspector에서 할당)
    //public DialogueManager dm; // DialogueManager를 Inspector에서 할당

    //bool isInteract = false; // 상호작용 가능한지
    //bool isDialogueActive = false; // 대화가 활성화되었는지

    //void Start()
    //{
    //    // dm.OnDialogueEnd += AfterTalking; // DialogueManager의 대화 종료 이벤트에 콜백 함수 등록
    //}

    //void Update()
    //{
    //    // 플레이어가 트리거 영역 안에 있는 동안 설정된 상호작용 키 (F)를 누르면
    //    if (isInteract && !isDialogueActive && Input.GetKeyDown(KeyCode.F))
    //    {
    //        Interactive();
    //    }

    //}

    //void OnTriggerEnter2D(Collider2D other) // 플레이어가 트리거에 들어왔을 때 실행되는 함수
    //{
    //    if (other.CompareTag("Player")) // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
    //    {
    //        TextMeshProUGUI tmpText = textCanvas.GetComponent<TextMeshProUGUI>();

    //        textCanvas.SetActive(true); // 상호작용 텍스트 출력
    //        isInteract = true; // 상호작용 가능
    //    }
    //}

    //void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player")) // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
    //    {
    //        textCanvas.SetActive(false); // 상호작용 텍스트 미출력
    //        isInteract = false; // 상호작용 불가

    //        // 대화 초기화
    //        //dm.SettingUI(false);
    //        //dm.currentTextIndex = 0;
    //        //dm.isWaitingInput = false;
    //        isDialogueActive = false;
    //    }
    //}

    //void Interactive() // 상호작용
    //{
    //    isInteract = true;
    //    isDialogueActive = true; // 대화가 시작되었음을 표시

    //    GameObject player = GameObject.FindGameObjectWithTag("Player");

    //    if (player.CompareTag("Player")) // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
    //    {
    //        //dm.ShowDialogue(); // 대화 시작

    //        //PlayerPositionManager.Instance.SetLastPositionAndScene(
    //        //    player.transform.position, SceneManager.GetActiveScene().name); // 현재 위치와 씬 이름 저장
    //    }
    //}

    //void AfterTalking()
    //{
    //    isDialogueActive = false; // 대화가 끝났음을 표시
    //    dm.gameObject.SetActive(false); // 씬 로드 전에 DialogueManager 오브젝트를 비활성화

    //    if (isInteract && !isDialogueActive && Input.GetKeyDown(KeyCode.F))
    //    {
    //        // MainGameManager에서 LoadScene을 불러옴 (미니게임으로 이동)
    //        MainGameManager.Instance.LoadScene("MiniGameScene");
    //    }
    //}

    #endregion

    #region 강의 기반 코드

    public GameObject textCanvas; // 문구 UI 오브젝트 (Inspector에서 할당)
    public DialogueManager dialogueManager; // DialogueManager를 Inspector에서 할당

    bool isInteract = false; // 상호작용 가능한지
    bool isDialogueActive = false; // 대화가 활성화되었는지

    void Start()
    {
        // DialogueManager의 대화 종료 이벤트에 AfterTalking 함수 등록
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueEnd += AfterTalking;
        }
        else
        {
            Debug.LogError("SkeletController: DialogueManager가 할당되지 않았습니다.");
        }
    }

    void Update()
    {
        // 플레이어가 트리거 영역 안에 있는 동안 설정된 상호작용 키 (F)를 누르면
        if (isInteract && Input.GetKeyDown(KeyCode.F))
        {
            if (!isDialogueActive) // 대화가 활성화되지 않았다면 (처음 대화 시작)
            {
                Interactive(); // 대화 시작
            }
            else // 대화가 활성화 중이라면 (다음 대화 진행 또는 대화 강제 스킵)
            {
                dialogueManager.Nextdialogue();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) // 플레이어가 트리거에 들어왔을 때 실행되는 함수
    {
        if (other.CompareTag("Player")) // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
        {
            //TextMeshProUGUI tmpText = textCanvas.GetComponent<TextMeshProUGUI>();

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

            if (isDialogueActive)
            {
                isDialogueActive = false; // 대화 종료

                if (dialogueManager != null) // 대화 코루틴이 실행 중이라면 강제 중지
                {
                    StopAllCoroutines(); // 현재 스크립트의 코루틴 중지
                    dialogueManager.SettingUI(false); // 대화창 UI 비활성화
                    dialogueManager.StopAllCoroutines(); // DialogueManager의 코루틴도 모두 중지
                }
            }
        }
    }

    void Interactive() // 상호작용
    {
        //isInteract = true;
        isDialogueActive = true; // 대화가 시작되었음을 표시
        dialogueManager.gameObject.SetActive(true);
        dialogueManager.StartDialogue();

    }

    void AfterTalking()
    {
        isDialogueActive = false; // 대화가 끝났음을 표시
        dialogueManager.gameObject.SetActive(false); // 씬 로드 전에 DialogueManager 오브젝트를 비활성화
        HandleSceneTransition();
    }

    void HandleSceneTransition()
    {
        // MainGameManager에서 LoadScene을 불러옴 (미니게임으로 이동)
        if (MainGameManager.Instance != null)
        {
            MainGameManager.Instance.LoadScene("MiniGameScene");
            Debug.Log("SkeletController: MiniGameScene으로 이동합니다.");
        }
        else
        {
            Debug.LogError("SkeletController: MainGameManager.Instance가 null입니다. 씬 전환 실패.");
        }
    }

    #endregion
}
