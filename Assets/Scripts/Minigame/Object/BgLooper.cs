using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5; // ��� ����
    public int obstacleCount = 0; // ��ֹ��� ����
    public Vector3 obstacleLastPosition = Vector3.zero; // ���������� ��ġ�� ��ֹ��� ��ġ

    // Start is called before the first frame update
    void Start()
    {
        // Scene�� �����ϴ� ��� Obstacle ��ü�� �迭�� ��������
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        // ù ��° ��ֹ��� ��ġ�� obstacleLastPosition�� ����
        obstacleLastPosition = obstacles[0].transform.position;
        // ��ֹ��� ������ ����Ͽ� ����
        obstacleCount = obstacles.Length;

        // ��ֹ� ������ŭ �ݺ��Ͽ� �� ��ֹ��� ��ġ�� �����ϰ� ����
        for (int i = 0; i < obstacleCount; i++)
        {
            // SetRandomPlace �Լ��� �� ��ֹ��� ��ġ�� ���� ��ֹ� ��ġ�� ������� ������
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround")) // �浹ü�� ��׶��� �±׸� �ް� ������
        {
            // �浹�� ��׶��� ������Ʈ�� BoxCollider2D ������Ʈ���� ���� ���̸� ������
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            // �浹�� ��׶��� ������Ʈ�� ���� ��ġ�� ����
            Vector3 pos = collision.transform.position;

            // ��׶��� ������Ʈ�� ���� ���̿� numBgCount ���� ���Ͽ� ���ο� x ��ǥ�� ���
            // ��׶��带 �ݺ������� ��ġ�ϱ� ���� ��ġ ����
            pos.x += widthOfBgObject * numBgCount;
            // �浹�� ��׶��� ������Ʈ�� ��ġ�� ���� ���� ��ġ�� ������Ʈ
            collision.transform.position = pos;
            return;
        }

        // �浹�� ��ü�� Obstacle���� Ȯ��
        Obstacle obstacle = collision.GetComponent<Obstacle>();

        if(obstacle)
        {
            // ��ֹ��� �浹 �� ���� ��ġ�� ���ġ
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
