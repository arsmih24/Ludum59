using UnityEngine;
using ObstaclesSystem;
using System.Collections;

namespace PlayerSystem
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private LayerMask signalLayer;

        private Invoker _invoker;
        private Coroutine _explosionTimerCoroutine;

        private const string FIRST_SIGNAL_TAG = "FirstSignal";
        private const string SECOND_SIGNAL_TAG = "SecondSignal";
        private const string THIRD_SIGNAL_TAG = "ThirdSignal";
        private const string FOURTH_SIGNAL_TAG = "FourthSignal";
        private const string FIFTH_SIGNAL_TAG = "FifthSignal";

        public void Construct(Invoker invoker) 
        {
            _invoker = invoker;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out AObstacle obstacle))
            {
                Explode();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BlackHole blackHole))
            {
                _invoker.InvokeStartBlackHoleLightBlinkCoroutine();
            }

            if (collision.gameObject.TryGetComponent(out Star star))
            {
                _invoker.InvokeStartStarLightBlinkCoroutine();
                _explosionTimerCoroutine = StartCoroutine(ExpolisionTimerCoroutine(star.ExplosionTimer));
            }

            if ((signalLayer.value & (1 << collision.gameObject.layer)) != 0)
            {
                _invoker.InvokePhotoButtonActivate();

                if (collision.gameObject.CompareTag(FIRST_SIGNAL_TAG))
                    _invoker.InvokeReturnPhotoButton().onClick.AddListener(_invoker.InvokeFirstSignalCollect);

                else if (collision.gameObject.CompareTag(SECOND_SIGNAL_TAG))
                    _invoker.InvokeReturnPhotoButton().onClick.AddListener(_invoker.InvokeSecondSignalCollect);

                else if (collision.gameObject.CompareTag(THIRD_SIGNAL_TAG))
                    _invoker.InvokeReturnPhotoButton().onClick.AddListener(_invoker.InvokeThirdSignalCollect);

                else if (collision.gameObject.CompareTag(FOURTH_SIGNAL_TAG))
                    _invoker.InvokeReturnPhotoButton().onClick.AddListener(_invoker.InvokeFourthSignalCollect);

                else if (collision.gameObject.CompareTag(FIFTH_SIGNAL_TAG))
                    _invoker.InvokeReturnPhotoButton().onClick.AddListener(_invoker.InvokeFifthSignalCollect);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BlackHole blackHole))
            {
                _invoker.InvokeStopBlackHoleLightBlinkCoroutine();
            }

            if (collision.gameObject.TryGetComponent(out Star star))
            {
                _invoker.InvokeStopStarLightBlinkCoroutine();
                StopCoroutine(_explosionTimerCoroutine);
                _explosionTimerCoroutine = null;
            }

            if ((signalLayer.value & (1 << collision.gameObject.layer)) != 0)
            {
                _invoker.InvokeReturnPhotoButton().onClick.RemoveListener(_invoker.InvokeFifthSignalCollect);
                _invoker.InvokePhotoButtonDeactivate();
            }
        }

        private IEnumerator ExpolisionTimerCoroutine(float timer) 
        {
            yield return new WaitForSeconds(timer);

            Explode();
        }

        private void Explode() 
        {
            _invoker.InvokeSetChangeDirection(false);
            _invoker.InvokeResetMovement();
            _invoker.InvokeReloadGame();
        }
    }
}