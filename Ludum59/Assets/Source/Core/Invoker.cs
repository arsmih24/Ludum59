using UnityEngine;
using PlayerSystem;
using UiSystem;
using UnityEngine.UI;

public class Invoker
{
    private PlayerData _playerData;
    private PlayerMovement _playerMovement;
    private PlayerView _playerView;
    private SignalsManager _signalsManager;
    private UiManager _uiManager;

    public Invoker(PlayerData playerData, PlayerMovement playerMovement, PlayerView playerView,
                   SignalsManager signalsManager, UiManager uiManager)
    {
        _playerData = playerData;
        _playerMovement = playerMovement;
        _playerView = playerView;
        _signalsManager = signalsManager;
        _uiManager = uiManager;
    }

    public void InvokeMove(Vector2 dir)
    {
        _playerMovement.Move(dir, _playerData.Rb, _playerData.Speed, _playerData.Acceleration, _playerData.TurnSpeed, _playerData.Decceleration);
    }
    public void InvokeStopMovement()
    {
        _playerMovement.Stop();
    }
    public void InvokeResetMovement() 
    {
        _playerMovement.Reset(_playerData.Rb);
    }
    public void InvokeSetChangeDirection(bool canChange)
    {
        _playerMovement.CanChangeDirection = canChange;
    }

    public void InvokeUpdateDirectionView()
    {
        _playerView.UpdateDirectionView(_playerMovement.GetTargetDirection());
    }
    public void InvokeUpdateCoordinatesView()
    {
        _playerView.UpdateCoordinatesView(_playerData.Rb);
    }

    public void InvokeFirstSignalCollect() 
    {
        _signalsManager.CollectFirstSignal();
    }
    public void InvokeSecondSignalCollect()
    {
        _signalsManager.CollectSecondSignal();
    }
    public void InvokeThirdSignalCollect()
    {
        _signalsManager.CollectThirdSignal();
    }
    public void InvokeFourthSignalCollect()
    {
        _signalsManager.CollectFourthSignal();
    }
    public void InvokeFifthSignalCollect()
    {
        _signalsManager.CollectFifthSignal();
    }

    public Button InvokeReturnPhotoButton() 
    {
        return _uiManager.ReturnPhotoButton();
    }

    public void InvokeReloadGame() 
    {
        _uiManager.ReloadGame();
    }
}