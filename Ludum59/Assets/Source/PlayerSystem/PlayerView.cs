using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerSystem
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Image directionImage;
        [Space]
        [SerializeField] private Sprite arrowUp;
        [SerializeField] private Sprite arrowDown;
        [SerializeField] private Sprite arrowLeft;
        [SerializeField] private Sprite arrowRight;
        [SerializeField] private Sprite arrowUpLeft;
        [SerializeField] private Sprite arrowDownLeft;
        [SerializeField] private Sprite arrowUpRight;
        [SerializeField] private Sprite arrowDownRight;
        [SerializeField] private Sprite arrowStand;
        [Space]
        [SerializeField] private TextMeshProUGUI coordinatesTextX;
        [SerializeField] private TextMeshProUGUI coordinatesTextY;

        private const float MOVEMENT_THRESHOLD = 0.01f;

        public void UpdateDirectionView(Vector2 targetDirection)
        {
            if (targetDirection.magnitude < MOVEMENT_THRESHOLD)
            {
                directionImage.sprite = arrowStand;
                return;
            }

            Vector2 dir = targetDirection.normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;

            Sprite selectedSprite;

            if (angle >= 337.5f || angle < 22.5f)
                selectedSprite = arrowRight;
            else if (angle >= 22.5f && angle < 67.5f)
                selectedSprite = arrowUpRight;
            else if (angle >= 67.5f && angle < 112.5f)
                selectedSprite = arrowUp;
            else if (angle >= 112.5f && angle < 157.5f)
                selectedSprite = arrowUpLeft;
            else if (angle >= 157.5f && angle < 202.5f)
                selectedSprite = arrowLeft;
            else if (angle >= 202.5f && angle < 247.5f)
                selectedSprite = arrowDownLeft;
            else if (angle >= 247.5f && angle < 292.5f)
                selectedSprite = arrowDown;
            else
                selectedSprite = arrowDownRight;

            directionImage.sprite = selectedSprite;
        }

        public void UpdateCoordinatesView(Rigidbody2D playerRb) 
        {
            coordinatesTextX.text = $"{Convert.ToInt64(playerRb.position.x)}";
            coordinatesTextY.text = $"{Convert.ToInt64(playerRb.position.y)}";
        }
    }
}