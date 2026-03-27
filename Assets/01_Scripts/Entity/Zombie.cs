using UnityEngine;

public class Zombie : MonoBehaviour, IInteractable
{
    public void Interact() // 플레이어와 상호작용 시킬 메서드
    {
        Debug.Log("상호작용");
        UIManager.Instance.ShowDialogueUI(name);
    }
}
