using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    [Header("Notice")]
    public TextMeshProUGUI NoticeText;
    private Coroutine _noticeTimerCoroutine;

    #region NoticeText

    public void UpdateNoticeText(string text) // �ؽ�Ʈ ������Ʈ
    {
        NoticeText.text = text; // ȭ�� ��ܿ� �ߴ� ���� �ؽ�Ʈ
        StartNoticeTimer(NoticeText.gameObject); // �ؽ�Ʈ Ÿ�̸� �۵�
    }

    // �ؽ�Ʈ Ÿ�̸� �۵�
    private void StartNoticeTimer(GameObject activeObject)
    {
        // 1.5�� �ȿ� ��ȣ�ۿ� ������ ������ ��� - ���� �ڷ�ƾ�� ���� ����
        if (_noticeTimerCoroutine != null)
        {
            StopCoroutine(_noticeTimerCoroutine); // �ڷ�ƾ ����
            _noticeTimerCoroutine = null; // Ÿ�̸� �ʱ�ȭ
        }

        // �ڷ�ƾ �۵�
        _noticeTimerCoroutine = StartCoroutine(routine: CountingCoroutine(activeObject));
    }

    // ���� �ð� �ڿ� UpdateNoticeText�� ����
    IEnumerator CountingCoroutine(GameObject activeObject)
    {
        activeObject.gameObject.SetActive(true); // NoticeText.text�� Ȱ��ȭ
        yield return new WaitForSeconds(1.5f); // 1.5�� ���
        activeObject.gameObject.SetActive(false); // NoticeText.text�� ��Ȱ��ȭ
    }

    #endregion
}