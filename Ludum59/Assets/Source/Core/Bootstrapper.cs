using UnityEngine;
using PlayerSystem;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [Space]
    [SerializeField] private InputListener inputListener;

    private void Awake()
    {
        PlayerMovement playerMovement = new PlayerMovement();
        PlayerInvoker playerInvoker = new PlayerInvoker(playerData, playerMovement);

        inputListener.Construct(playerInvoker);
    }
}
