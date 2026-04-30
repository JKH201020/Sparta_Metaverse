using System.Collections;
using TMPro;
using UnityEngine;

public class DamageTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _damageText;

    private void Reset()
    {
        _damageText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 대미지와 대미지UI 연결
    /// </summary>
    /// <param name="damage">대미지</param>
    public void SetDamage(float damage)
    {
        _damageText.text = damage.ToString();

        StartCoroutine(AnimateDamageText());
    }

    private IEnumerator AnimateDamageText()
    {
        float elapsed = 0f;
        float duration = 0.5f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * 50f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;

            // 위치 이동 및 투명도 조절
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            _damageText.alpha = Mathf.Lerp(1, 0, t);

            yield return null;
        }

        DamageTextPool.Instance.ReturnToPool(this);
    }
}
