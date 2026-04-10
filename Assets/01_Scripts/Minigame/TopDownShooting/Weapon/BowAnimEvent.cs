using UnityEngine;

public class BowAnimEvent : MonoBehaviour
{
    [SerializeField] private ShootingPlayerController _spController;

    private void Reset()
    {
        _spController = transform.GetComponentInParent<ShootingPlayerController>();
    }

    public void OnShoot()
    {
        if (_spController != null) _spController.OnShoot();
    }
}
