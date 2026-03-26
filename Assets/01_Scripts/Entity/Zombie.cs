using TMPro;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText; // 인스펙터에서 할당 가능
    [SerializeField] string[] _zombieDialogue; // 인스펙터에 여러 문장을 추가

    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomDialogue(); // 랜덤 문구가 출력
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _dialogueText.gameObject.SetActive(false); // 텍스트 화면 미출력
    }

    private void RandomDialogue()
    {
        int randomIndex = Random.Range(0, _zombieDialogue.Length); // 0부터 배열 길이 만큼 랜덤으로 숫자 지정

        _dialogueText.text = _zombieDialogue[randomIndex];  // 문자열 배열에서 랜덤으로 문장 출력
        _dialogueText.gameObject.SetActive(true); // 텍스트 화면에 출력
    }
}
