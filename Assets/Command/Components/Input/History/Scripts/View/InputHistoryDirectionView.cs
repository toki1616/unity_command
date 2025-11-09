using UnityEngine;
using UnityEngine.UI;

namespace My.Command
{
    public class InputHistoryDirectionView : MonoBehaviour
    {
        [SerializeField]
        private Text neutralText;

        [SerializeField]
        private Image arrowImage;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        public void SetInputFrameData(InputFrameData inputFrameData)
        {
            float angle = 0f;
            bool isNeutral = false;

            switch (inputFrameData.Direction)
            {
                case InputDirection.Neutral:
                    isNeutral = true;
                    break;

                case InputDirection.Top:
                    angle = 0f;
                    break;

                case InputDirection.Bottom:
                    angle = 180f;
                    break;

                case InputDirection.Left:
                    angle = 90f;
                    break;

                case InputDirection.Right:
                    angle = -90f;
                    break;

                case InputDirection.UpperLeft:
                    angle = 45f;
                    break;

                case InputDirection.UpperRight:
                    angle = -45f;
                    break;

                case InputDirection.LowerLeft:
                    angle = 135f;
                    break;

                case InputDirection.LowerRight:
                    angle = -135f;
                    break;
            }

            SwitchDirection(isNeutral);

            if (!isNeutral)
            {
                arrowImage.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            }
        }

        private void SwitchDirection(bool isNeutral)
        {
            neutralText.gameObject.SetActive(isNeutral);
            arrowImage.gameObject.SetActive(!isNeutral);
        }
    }
}
