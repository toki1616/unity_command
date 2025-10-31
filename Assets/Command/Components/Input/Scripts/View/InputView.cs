using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace MyCommand
{
    public class InputView : MonoBehaviour
    {
        private InputViewModel _inputViewModel;

        [Inject]
        public void Construct
            (
                InputViewModel inputViewModel
            )
        {
            _inputViewModel = inputViewModel;
        }

        // 2軸入力を受け取るAction
        [SerializeField]
        private InputActionProperty _moveAction;

        // 移動の速さ
        [SerializeField]
        private float _speed = 1;

        private void Update()
        {
            // 2軸入力読み込み
            var inputValue = _moveAction.action.ReadValue<Vector2>();
            if (inputValue != Vector2.zero)
            {
                _inputViewModel.UpdateMove(inputValue);
            }
        }

        private void OnDestroy()
        {
            _moveAction.action.Dispose();
        }

        private void OnEnable()
        {
            _moveAction.action.Enable();
        }

        private void OnDisable()
        {
            _moveAction.action.Disable();
        }
    }
}
