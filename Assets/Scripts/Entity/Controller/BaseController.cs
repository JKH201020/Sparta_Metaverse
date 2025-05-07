using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // �̵��� ���� ���� ������Ʈ
    protected SpriteRenderer spriteRenderer; // SpriteRenderer ������Ʈ�� �����ϱ� ���� ����
    protected AnimationHandler animationHandler;

    [SerializeField] private SpriteRenderer characterRenderer; // �¿� ������ ���� ������

    protected Vector2 movementDirection = Vector2.zero; // ���� �̵� ����
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero; // ���� �ٶ󺸴� ����
    public Vector2 LookDirection { get { return lookDirection; } }

    // ���� ������Ʈ�� ������Ʈ ���� ������ �����ϰų� �ʱ� ���¸� �����ϴ� ���� �ʱ�ȭ �۾��� �����ϴ� �� ����
    protected virtual void Awake() // �� ��ũ��Ʈ�� ���ӿ�����Ʈ�� �ε�� �� �ڵ����� �� �� �� ȣ��
    {
        // ���� ��ũ��Ʈ�� ����� ���� ������Ʈ�� ������ �ִ� Rigidbody 2D ������Ʈ�� ã�� ��ȯ�ϴ� ����
        _rigidbody = GetComponent<Rigidbody2D>();
        // ���� ���� ������Ʈ���� SpriteRenderer ������Ʈ�� �ڽĿ��� ã�� spriteRenderer ������ �Ҵ�
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        // ���� ��ũ��Ʈ�� ����� ���� ������Ʈ�� �ڽ��� ������ �ִ� AnimationHandler ������Ʈ�� ã�� ��ȯ�ϴ� ����
        animationHandler = GetComponentInChildren<AnimationHandler>();
    }

    protected virtual void Start()
    {
        Time.timeScale = 1.0f; // Scene ��ȯ �� �����ִ� ��Ȳ ��������
    }

    protected virtual void Update()
    {
        HandleAction();
        Flip();
        Jump();
    }

    protected virtual void FixedUpdate() // ���� ������Ʈ (������ �ð� �������� ȣ���)
    {
        Movement(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }

    void Movement(Vector2 direction) // �̵�
    {
        direction = direction * 5; // �̵� �ӵ�

        _rigidbody.velocity = direction; // ���� ���� �̵�
        animationHandler.Move(direction); // �̵� �ִϸ��̼� ó��
    }

    void Flip() // ĳ���� �̹��� �¿����
    {
        // Rigidbody2D ������Ʈ�� �����Ѵٸ� �ӵ� ������� �¿� ������ ����
        if (_rigidbody.velocity.x > 0.1f)
        {
            // Rigidbody2D�� x�� �ӵ��� ����̰� ��� ���� ũ�ٸ� (���������� �̵� ��)
            spriteRenderer.flipX = false;
        }
        else if (_rigidbody.velocity.x < -0.1f)
        {
            // Rigidbody2D�� x�� �ӵ��� �����̰� ��� ���� �۴ٸ� (�������� �̵� ��)
            spriteRenderer.flipX = true; // �¿����
        }
    }

    void Jump() // ����
    {
        animationHandler.Jump();
    }
}