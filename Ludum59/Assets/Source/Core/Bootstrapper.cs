using UnityEngine;
using PlayerSystem;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerView playerView;
    [Space]
    [SerializeField] private InputListener inputListener;

    private void Awake()
    {
        PlayerMovement playerMovement = new PlayerMovement();
        PlayerInvoker playerInvoker = new PlayerInvoker(playerData, playerMovement, playerView);

        inputListener.Construct(playerInvoker);
    }
}
