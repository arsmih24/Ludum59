using UnityEngine;

namespace PlayerSystem
{
    public class PlayerMovement
    {
        private Vector2 _currentVelocity;
        private Vector2 _stickDirection;
        private float _currentSpeed;
        private bool _hasStickSet;
        private bool _isReversing;

        public void Move(Vector2 inputDir, bool canChangeDir, Rigidbody2D rb, float maxSpeed, float accel, float turnSpeed, float decel)
        {
            Vector2 newInput = inputDir.normalized;

            if (canChangeDir && newInput != Vector2.zero)
            {
                if (_hasStickSet)
                {
                    float dot = Vector2.Dot(_stickDirection, newInput);
                    if (dot < -0.8f) 
                    {
                        _isReversing = true;
                    }
                }

                _stickDirection = newInput;
                _hasStickSet = true;
            }

            if (!_hasStickSet)
            {
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0f, decel * Time.fixedDeltaTime);

                if (_currentSpeed < 0.01f)
                {
                    _currentSpeed = 0f;
                    _currentVelocity = Vector2.zero;
                    _isReversing = false;
                }
                else
                {
                    _currentVelocity = _currentVelocity.normalized * _currentSpeed;
                }

                rb.linearVelocity = _currentVelocity;
                return;
            }

            Vector2 currentDir = _currentVelocity.magnitude > 0.01f ? _currentVelocity.normalized : _stickDirection;

            if (_isReversing)
            {
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0f, decel * Time.fixedDeltaTime);

                if (_currentSpeed < 0.1f)
                {
                    _isReversing = false;
                    _currentSpeed = 0f;
                }

                _currentVelocity = currentDir * _currentSpeed;
            }
            else
            {
                Vector2 smoothedDir = Vector2.Lerp(currentDir, _stickDirection, turnSpeed * Time.fixedDeltaTime).normalized;
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, maxSpeed, accel * Time.fixedDeltaTime);
                _currentVelocity = smoothedDir * _currentSpeed;
            }

            rb.linearVelocity = _currentVelocity;
        }

        public Vector2 GetTargetDirection()
        {
            if (!_hasStickSet) return Vector2.zero;
            return _stickDirection;
        }

        public void Stop(Rigidbody2D rb)
        {
            _hasStickSet = false;
            _isReversing = false;
            _stickDirection = Vector2.zero;
        }

        public void Reset()
        {
            _hasStickSet = false;
            _stickDirection = Vector2.zero;
            _currentSpeed = 0f;
            _currentVelocity = Vector2.zero;
            _isReversing = false;
        }
    }
}