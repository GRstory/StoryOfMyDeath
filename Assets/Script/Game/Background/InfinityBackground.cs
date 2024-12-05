using UnityEngine;
using UnityEngine.UIElements;

public class InfinityBackground : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _transformList;


    float leftPosX = 0f;
    float rightPosX = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;

    void Start()
    {
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        leftPosX = -(xScreenHalfSize * 2);
        rightPosX = xScreenHalfSize * 2 * _transformList.Length;
    }

    void Update()
    {
        for (int i = 0; i < _transformList.Length; i++)
        {
            _transformList[i].position += new Vector3(-_speed, 0, 0) * Time.deltaTime;

            if (_transformList[i].position.x < leftPosX)
            {
                Vector3 nextPos = _transformList[i].position;
                nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                _transformList[i].position = nextPos;
            }
        }
    }
}
