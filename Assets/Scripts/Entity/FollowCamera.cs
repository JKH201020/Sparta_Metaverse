using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // ���� ��� (�÷��̾�)
    public float Speed = 5f; // ī�޶� �̵� �ӵ�
    public Vector2 minBounds; // ī�޶� ������ �� �ִ� �ּ� ��ġ
    public Vector2 maxBounds; // ī�޶� ������ �� �ִ� �ִ� ��ġ
    private Vector3 offset; // ī�޶�� �÷��̾� ���� �ʱ� �Ÿ�

    void Start()
    {
        // �ʱ� �Ÿ� ���� (���� z �ุ -10)
        offset = transform.position - target.position;
    }

    // LateUpdate()�� ����ϴ� ������ ��� ĳ���� �̵��� ���� �Ŀ� ī�޶� ���󰡴� ������ ����� ����
    void LateUpdate()
    {
        // ���󰡾� �� ��ġ ��� (z�� ����)
        Vector3 pos = target.position + offset;
        pos.z = transform.position.z;

        // ��ġ ���� ����
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

        // �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * Speed);
    }
}
