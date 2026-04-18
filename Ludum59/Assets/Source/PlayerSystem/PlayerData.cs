using UnityEngine;

namespace PlayerSystem
{
    public class PlayerData : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rb { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 1;
        [field: SerializeField] public float Acceleration { get; private set; } = 0.8f;
        [field: SerializeField] public float TurnSpeed { get; private set; } = 0.5f;
        [field: SerializeField] public float Decceleration { get; private set; } = 0.8f;
    }
}