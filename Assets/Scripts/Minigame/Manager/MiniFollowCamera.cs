using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniFollowCamera : MonoBehaviour
{
    private Transform target; // ī�޶� ���� ���
    float offsetX; // ī�޶�� ��� ������ X�� ������

    // Update is called once per frame
    void Update()
    {
        // target�� null�̶�� ī�޶� �ƹ��͵� ������ ����
        if (target == null) return;

        Vector3 pos = transform.position; // ���� ī�޶� ��ġ�� ����

        pos.x = target.position.x + offsetX; // ī�޶��� X ��ġ�� ����� X ��ġ�� �������� ���� ����
        transform.position = pos; // ���� ���ο� ��ġ�� ī�޶� �̵�
    }

    // ���� �������� ���������� �ҷ��� ��
    public void SetTarget(Transform _target) // ī�޶� Ÿ�� ����
    {
        target = _target;

        // ī�޶��� ���� ��ġ�� ����� X ��ġ ���̸� ���������� ����
        offsetX = transform.position.x - target.position.x;
    }
}
