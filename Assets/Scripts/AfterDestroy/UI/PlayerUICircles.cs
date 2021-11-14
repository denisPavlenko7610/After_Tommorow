using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AfterDestroy.UI
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

        [SerializeField] float countDownTime;

        void Start()
        {
            StartCoroutine(UpdateCircles());
        }

        IEnumerator UpdateCircles()
        {
            while (true)
            {
                PlayerStateCountDown(circleHungerImage, countDownTime);
                PlayerStateCountDown(circleThirstyImage, countDownTime);
                PlayerStateCountDown(circleSleepImage, countDownTime);

                CheckImageRedStatus();
                yield return new WaitForSeconds(0.5f);
            }
        }

        void PlayerStateCountDown(Image image, float time, float slowerParameter = 1)
        {
            image.fillAmount -= time * Time.deltaTime * slowerParameter;

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