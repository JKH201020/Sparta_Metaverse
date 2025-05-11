using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    MiniGameManager gameManager; // GameManager �ν��Ͻ��� ������ ����

    public float highPosY = 1f; // ��ֹ��� ��ġ�� �� �ִ� Y�� ���Ѽ�
    public float lowPosY = -1f; // ��ֹ��� ��ġ�� �� �ִ� Y�� ���Ѽ�

    public float holeSizeMin = 1.2f; // ������ �ּ� ũ��
    public float holeSizeMax = 2.5f; // ������ �ִ� ũ��

    public Transform topObject; // ��ֹ� ��� ������Ʈ (�� ��ü�� ���� ��ġ)
    public Transform bottomObject; // ��ֹ� �ϴ� ������Ʈ (�� ��ü�� �Ʒ��� ��ġ)

    public float widthPadding = 4f; // �� ��ֹ� ���� X�� ���� (�ʺ� �е�)

    public void Start()
    {
        gameManager = MiniGameManager.Instance; // ���� ���� �� GameManager�� �ν��Ͻ��� �����ͼ� ���
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Plane plane = other.GetComponent<Plane>(); // �浹�� ��ü�� �÷��̾����� Ȯ��

        if (plane != null && !Plane.isDead) // �÷��̾ �´ٸ�
        {
            gameManager.UpdateScore(1); // ���� �Ŵ����� ���� ���� 1 ����
        }
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount) // ��ֹ��� ���� ��ġ�� ��ġ�ϴ� �Լ�
    {
        // ���� ũ�� ���� ���� (min ~ max ���� ������)
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        // ���� ũ�⸦ ������ ������ ��ܰ� �ϴ� ��ü�� Y ��ġ ����
        float halfHoleSize = holeSize / 2f;

        topObject.localPosition = new Vector3(0, halfHoleSize); // ��� ��ü�� ��ġ
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // �ϴ� ��ü�� ��ġ

        // ������ ��ġ���� X������ ������ ���� ���ο� ��ġ ���
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        // ���ο� ��ġ�� Y���� ���� ������ ���� (lowPosY�� highPosY ����)
        placePosition.y = Random.Range(lowPosY, highPosY);

        // ��ֹ��� ��ġ�� ���ο� ��ġ�� ����
        transform.position = placePosition;

        // ���� ������ ��ġ�� ��ȯ
        return placePosition;
    }
}
