using UnityEngine;

public class GroundTest : MonoBehaviour
{
    Rigidbody2D _rigidbody2;
    public float speed = 2f; // 움직이는 속도
    public float range = 3f; // 움직이는 범위
    public Vector2 direction = Vector2.right; // 초기 방향 (x축)
    private Vector2 startPosition;

    private void Awake()
    {
        _rigidbody2 = GetComponent<Rigidbody2D>();
        startPosition = _rigidbody2.position;
    }

    void FixedUpdate()
    {
        float offset = Mathf.Sin(Time.time * speed) * range;

        // Rigidbody2D 위치 설정
        _rigidbody2.linearVelocity = new Vector2(offset, 0);

    }
}
