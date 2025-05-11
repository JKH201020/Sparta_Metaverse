using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText; // 인스펙터에서 할당 가능
    [SerializeField] string[] _zombieDialogue; // 인스펙터에 여러 문장을 추가

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO : 충돌체가 내 플레이어 영역에 들어왔을 때
        RandomDialogue(); // 랜덤 문구가 출력
        MainGameManager.Instance.UpdateNoticeText("상호작용 범위 입니다.");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // TODO : 충돌체가 내 플레이어 영역에 벗어냈을 때
        _dialogueText.gameObject.SetActive(false); // 텍스트 화면 미출력
        MainGameManager.Instance.UpdateNoticeText("상호작용 범위를 벗어났습니다.");
    }

    private void RandomDialogue()
    {
        int randomIndex = Random.Range(0, _zombieDialogue.Length); // 0부터 배열 길이 만큼 랜덤으로 숫자 지정

        _dialogueText.text = _zombieDialogue[randomIndex];  // 문자열 배열에서 랜덤으로 문장 출력
        _dialogueText.gameObject.SetActive(true); // 텍스트 화면에 출력
    }
}
