using ObstaclesSystem;
using UnityEngine;

public class Star : AObstacle
{
    [SerializeField] private float explosionTimer = 10f;

    public float ExplosionTimer => explosionTimer;
}
