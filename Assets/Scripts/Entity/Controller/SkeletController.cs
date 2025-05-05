using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletController : MonoBehaviour
{
    // 캐릭터 방향으로 바라보게 하기

    public GameObject textCanvas; // 문구 UI 오브젝트 (Inspector에서 할당)
    DialogueManager dm;

    public string sceneName; // 이동할 씬의 이름 (Inspector 창에서 설정)
    bool Interact = false; // 상호작용 가능한지

    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        Interactive();
    }

    void OnTriggerEnter2D(Collider2D other) // 플레이어가 트리거에 들어왔을 때 실행되는 함수
    {
        if (other.CompareTag("Player")) // 트리거에 들어온 오브젝트가 "Player" 태그를 가지고 있는지 확인
        {
            // 원하는 이벤트 실행 (예: 미니게임 시작, 대화 창 띄우기 등)
            if (textCanvas != null)
            {
                TextMeshProUGUI tmpText = textCanvas.GetComponent<TextMeshProUGUI>();

                textCanvas.SetActive(true); // 상호작용 텍스트 출력

                Interact = true; // 상호작용 가능
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (textCanvas != null)
        {
            textCanvas.SetActive(false); // 상호작용 텍스트 미출력

            Interact = false; // 상호작용 불가
            dm.SettingUI(Interact);
        }
    }

    void Interactive()
    {
        Interact = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // 플레이어가 트리거 영역 안에 있는 동안 설정된 상호작용 키 (F)를 누르면
        if (Interact && Input.GetKeyDown(KeyCode.F))
        {
            PlayerPositionManager.Instance.SetLastPositionAndScene(
                player.transform.position, SceneManager.GetActiveScene().name); // 현재 위치와 씬 이름 저장

            dm.ShowDialogue();
            //SceneManager.LoadScene(sceneName); // 상호작용(미니게임으로 이동)
        }
    }
}
