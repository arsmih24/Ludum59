using UnityEngine;

namespace PlayerSystem
{
    public class PlayerInvoker
    {
        private PlayerData _playerData;
        private PlayerMovement _playerMovement;
        private PlayerView _playerView;

        public PlayerInvoker(PlayerData playerData, PlayerMovement playerMovement, PlayerView playerView)
        {
            _playerData = playerData;
            _playerMovement = playerMovement;
            _playerView = playerView;
        }

        public void InvokeMove(Vector2 dir)
        {
            _playerMovement.Move(dir, true, _playerData.Rb, _playerData.Speed, _playerData.Acceleration, _playerData.TurnSpeed, _playerData.Decceleration);
        }

        public void InvokeStop()
        {
            _playerMovement.Stop(_playerData.Rb);
        }

        public void InvokeUpdateDirectionView() 
        {
            _playerView.UpdateDirectionView(_playerMovement.GetTargetDirection());
        }

        public void InvokeUpdateCoordinatesView() 
        {
            _playerView.UpdateCoordinatesView(_playerData.Rb);
        }
    }
}