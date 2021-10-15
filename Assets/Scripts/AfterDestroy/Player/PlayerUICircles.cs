using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.AfterDestroy.Player
{
    public class PlayerUICircles : MonoBehaviour
    {
        [Header("Circles")]
        [SerializeField] Image circleHungerImage;
        [SerializeField] Image circleThirstyImage;
        [SerializeField] Image circleSleepImage;

        [Header("Images")]
        [SerializeField] Image healthImage;
        [SerializeField] Image hungerImage;
        [SerializeField] Image thirstyImage;
        [SerializeField] Image sleepImage;

        [SerializeField] float timeToCountDown;

        void Update()
        {
            PlayerStateCountDown(circleHungerImage, timeToCountDown);
            PlayerStateCountDown(circleThirstyImage, timeToCountDown);
            PlayerStateCountDown(circleSleepImage, timeToCountDown / 2);

            CheckImageRedStatus();
        }

        void PlayerStateCountDown(Image image, float time)
        {
            image.fillAmount -= time * Time.deltaTime;

            if (IsFillAmountNearByZero(image))
            {
                healthImage.fillAmount -= time * Time.deltaTime;
            }            
        }

        void CheckImageRedStatus()
        {
            if (IsFillAmountNearByZero(circleHungerImage))
            {
                SetRedImage(hungerImage, "HungerRed");
            }

            if (IsFillAmountNearByZero(circleThirstyImage))
            {
                SetRedImage(thirstyImage, "WaterRed");
            }

            if (IsFillAmountNearByZero(circleSleepImage))
            {
                SetRedImage(sleepImage, "SleepRed");
            }
        }

        bool IsFillAmountNearByZero(Image image)
        {
            return image.fillAmount <= 0.01f ? true : false;
        }

        void SetRedImage(Image image, string imageName)
        {
            image.sprite = Resources.Load<Sprite>($"{imageName}");
        }
    }
}