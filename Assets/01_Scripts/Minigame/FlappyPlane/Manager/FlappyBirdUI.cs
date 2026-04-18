using UnityEngine;

public class FlappyBirdUI : MonoBehaviour
{
    public static FlappyBirdUI Instance { get; private set; }

    [Header("내부 UI")]
    [SerializeField] private HomeUI _homeUI;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private EndUI _endUI;

    private void Reset()
    {
        _homeUI = GetComponentInChildren<HomeUI>(true);
        _gameUI = GetComponentInChildren<GameUI>(true);
        _endUI = GetComponentInChildren<EndUI>(true);
    }

    private void Awake()
    {
        Instance = this;
    }
}
