using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ���

    // Update is called once per frame
    void Update()
    {
        if (target == null) return; // target�� null�̶�� ī�޶� �ƹ��͵� ������ ����

        Vector3 pos = transform.position; // ���� ī�޶� ��ġ�� ����
        pos.x = target.position.x; // ī�޶��� X ��ġ�� ����� X ��ġ
        pos.y = target.position.y; // ī�޶��� Y ��ġ�� ����� Y ��ġ
        transform.position = pos; // ���� ���ο� ��ġ�� ī�޶� �̵�
    }
}
