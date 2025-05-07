using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniFollowCamera : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ���
    float offsetX; // ī�޶�� ��� ������ X�� ������

    // Start is called before the first frame update
    void Start()
    {
        // target�� �������� ������, ī�޶� �ƹ��͵� ������ �ʵ��� ��
        if (target == null) return;

        // ī�޶��� ���� ��ġ�� ����� X ��ġ ���̸� ���������� ����
        offsetX = transform.position.x - target.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // target�� null�̶�� ī�޶� �ƹ��͵� ������ ����
        if (target == null) return;


        Vector3 pos = transform.position; // ���� ī�޶� ��ġ�� ����
        pos.x = target.position.x + offsetX; // ī�޶��� X ��ġ�� ����� X ��ġ�� �������� ���� ����
        transform.position = pos; // ���� ���ο� ��ġ�� ī�޶� �̵�
    }
}
