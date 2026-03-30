using UnityEngine;

public class Zombie : MonoBehaviour, IInteractable
{
    public void Interact() // 플레이어와 상호작용 시킬 메서드
    {
        UIManager.Instance.ShowDialogueUI(gameObject.name, ChangeScene);
    }

    // 이 함수는 async를 달지 않은 평범한 동기 함수
    private void ChangeScene()
    {
        // 씬을 바꾸라고 명령만 던져놓고, 끝날 때까지 안 기다림
        // await를 안 썼으니 이 함수에 async를 달 필요가 없음
        if (GameManager.Instance != null) _ = GameManager.Instance.ChangeScene(SceneNames.MiniGameScene_2);
    }
}
