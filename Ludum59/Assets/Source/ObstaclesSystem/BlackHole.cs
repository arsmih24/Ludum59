using UnityEngine;

namespace ObstaclesSystem
{
    public class BlackHole : AObstacle
    {
        [SerializeField] private float maxGravityForce = 50f;
        [Space]
        [SerializeField] private float maxRadius = 10f;

        private void OnTriggerStay2D(Collider2D other)
        {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb == null) return;

            Vector2 toCenter = (Vector2)transform.position - rb.position;
            float distance = toCenter.magnitude;
            float normalizedDist = Mathf.Clamp01(distance / maxRadius);
            float gravityFactor = 1f - normalizedDist;
            float force = maxGravityForce * (gravityFactor * gravityFactor);

            Vector2 forceDirection = toCenter.normalized;
            rb.AddForce(forceDirection * force, ForceMode2D.Force);
        }
    }
}
