using ObstaclesSystem;
using UnityEngine;

public class Asteroid : AObstacle
{
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float wanderRadius = 5f;
    [Space]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float turnSpeed = 1.5f;
    [SerializeField] private float noiseScale = 0.3f;

    private Vector2 _center;
    private Vector2 _currentDirection;
    private float _noiseOffset;

    void Start()
    {
        _center = centerPoint != null ? centerPoint.position : transform.position;
        _currentDirection = Random.insideUnitCircle.normalized;
        _noiseOffset = Random.Range(0f, 1000f); 
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        Vector2 toCenter = _center - pos;
        float distFromCenter = toCenter.magnitude;
        float angleNoise = Mathf.PerlinNoise(Time.time * noiseScale, _noiseOffset) * 2f - 1f;

        float turnAngle = angleNoise * turnSpeed * Time.fixedDeltaTime;
        _currentDirection = RotateVector(_currentDirection, turnAngle);

        if (distFromCenter > wanderRadius * 0.8f)
        {
            Vector2 toCenterDir = toCenter.normalized;
            _currentDirection = Vector2.Lerp(_currentDirection, toCenterDir, 0.05f).normalized;
        }

        transform.position += (Vector3)(_currentDirection * speed * Time.fixedDeltaTime);
    }

    Vector2 RotateVector(Vector2 v, float angle)
    {
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }

    void OnDrawGizmosSelected()
    {
        Vector2 center = Application.isPlaying ? _center :
            (centerPoint != null ? centerPoint.position : transform.position);

        Gizmos.color = new Color(1, 1, 0, 0.3f);
        Gizmos.DrawWireSphere(center, wanderRadius);
    }
}
