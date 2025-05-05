using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator = null; // �ִϸ����Ϳ� ������ٵ� ���� ����
    Rigidbody2D _rigidbody = null; // �÷��̾��� ������ٵ� (���� ���� ����)
    GameManager gameManager = null;

    float flapForce = 6f; // ���� ���� (�÷�)
    float forwardSpeed = 3f; // ������ ������ �ӵ� (���� �̵�)
    public static bool isDead = false; // �÷��̾ �׾����� Ȯ���ϴ� ����
    float deathCooldown = 0f; // ��� �� ������� ���� ��� �ð�

    bool isFlap = false; // ����(�÷�) ���� Ȯ���ϴ� ����

    // Start is called before the first frame update
    void Start()
    {
        // �ִϸ����Ϳ� ������ٵ� ������Ʈ���� ã��
        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) // ���
        {
            // �׾��� �� ��� �ð�(`deathCooldown`)�� ������ ������� ���� �Է� �ޱ�
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame(); // ���� �����
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime; // ��� �ð� ����
            }
        }
        else // ����
        {
            // ���� (�÷�) �Է� ó��
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true; // ���� ����
            }
        }
    }

    public void FixedUpdate() // ���� ������Ʈ (������ �ð� �������� ȣ���)
    {
        if (isDead) return; // �׾����� ���� ���� ���� ����

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed; // ���� �ӵ��� �����ϰ� ���� (������ ��� �̵�)

        if (isFlap)
        {
            velocity.y += flapForce; // ���� ȿ�� (���� �ӵ� ����)
            isFlap = false; // ���� �Ϸ� �� �ʱ�ȭ
        }

        _rigidbody.velocity = velocity; // ������ٵ� �ӵ� ������Ʈ

        // ���� �� ���� ���� (���Ʒ��� ����)
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90); // y��: -90 ~ 90
        // Ư�� �������Ϸ� �������� ���� ����
        float lerpAngle = Mathf.Lerp(transform.rotation.eulerAngles.z, angle, Time.fixedDeltaTime * 5f);

        transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
    }

    public void OnCollisionEnter2D(Collision2D collision) // �浹 ���� �� ȣ��
    {
        if(isDead) return; // �̹� �׾����� �浹 ó�� �� ��

        // ���� �ִϸ��̼� ����
        animator.SetInteger("IsDie", 1);
        isDead = true; // ���� ���·� ����
        deathCooldown = 1f; // ����� ��� �ð� 1�� ����
        gameManager.GameOver(); // ���� ���� ó��
    } 
}
