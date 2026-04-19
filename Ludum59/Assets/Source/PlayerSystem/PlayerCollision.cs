using UnityEngine;
using ObstaclesSystem;
using System.Collections;

namespace PlayerSystem
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private LayerMask signalLayer;

        private Invoker _invoker;

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
                Debug.Log("Entered Black Hole");
            }

            if (collision.gameObject.TryGetComponent(out Star star))
            {
                Debug.Log("Entered Star Radius");
                StartCoroutine(ExpolisionTimerCoroutine(star.ExplosionTimer));
            }

            if (collision.gameObject.layer == signalLayer)
            {
                Debug.Log("Signal");
                _invoker.InvokeReturnPhotoButton().gameObject.SetActive(true);

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
                Debug.Log("Left Black Hole");
            }

            if (collision.gameObject.TryGetComponent(out Star star))
            {
                Debug.Log("Left Star Radius");
                StopAllCoroutines();
            }

            if (collision.gameObject.layer == signalLayer) 
            {
                _invoker.InvokeReturnPhotoButton().onClick.RemoveAllListeners();
                _invoker.InvokeReturnPhotoButton().gameObject.SetActive(false);
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