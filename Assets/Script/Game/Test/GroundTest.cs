using UnityEngine;

public class GroundTest : MonoBehaviour
{
    Rigidbody2D _rigidbody2;
    public float speed = 2f; // �����̴� �ӵ�
    public float range = 3f; // �����̴� ����
    public Vector2 direction = Vector2.right; // �ʱ� ���� (x��)
    private Vector2 startPosition;

    private void Awake()
    {
        _rigidbody2 = GetComponent<Rigidbody2D>();
        startPosition = _rigidbody2.position;
    }

    void FixedUpdate()
    {
        float offset = Mathf.Sin(Time.time * speed) * range;

        // Rigidbody2D ��ġ ����
        _rigidbody2.linearVelocity = new Vector2(offset, 0);

    }
}
