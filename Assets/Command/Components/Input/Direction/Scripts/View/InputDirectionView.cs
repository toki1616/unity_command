using UnityEngine;
using UnityEngine.UI;
using Zenject;
using R3;

namespace MyCommand
{
    public class InputDirectionView : MonoBehaviour
    {
        private InputViewModel _inputViewModel;

        [Inject]
        public void Construct(InputViewModel inputViewModel)
        {
            _inputViewModel = inputViewModel;

            _inputViewModel.InputDirectionObservable
                .Subscribe(_ => 
                {
                    ImageActive(_);
                })
                .AddTo(this);
        }

        [SerializeField]
        private Image _directionImage;

        [SerializeField]
        private InputDirection _inputDirection;

        private void ImageActive(InputDirection inputDirection)
        {
            _directionImage.color = (_inputDirection == inputDirection)
                ? CommandColors.directionActiveColor
                : CommandColors.directionEnactiveColor;
        }
    }
}
