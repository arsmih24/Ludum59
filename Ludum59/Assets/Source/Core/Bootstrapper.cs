using UnityEngine;
using PlayerSystem;
using UiSystem;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerCollision playerCollision;
    [Space]
    [SerializeField] private SignalsManager signalsManager;
    [SerializeField] private InputListener inputListener;
    [Space]
    [SerializeField] private UiManager uiManager;
    [SerializeField] private RadarPanel radarPanel;
    [SerializeField] private MemoryMiniGame miniGamePanel;
    [SerializeField] private PausePanel pausePanel;

    private void Awake()
    {
        PlayerMovement playerMovement = new PlayerMovement();
        Invoker playerInvoker = new Invoker(playerData, playerMovement, playerView, signalsManager, uiManager);

        inputListener.Construct(playerInvoker);
        playerCollision.Construct(playerInvoker);
        signalsManager.Construct(playerInvoker);
        radarPanel.Construct(uiManager);
        miniGamePanel.Construct(uiManager);
        pausePanel.Construct(uiManager);
    }
}
