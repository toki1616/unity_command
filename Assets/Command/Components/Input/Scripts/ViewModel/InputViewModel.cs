using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace MyCommand
{
    public class InputViewModel
    {
        private InputModel _inputModel;

        [Inject]
        public InputViewModel
            (
                InputModel inputModel
            )
        {
            _inputModel = inputModel;
        }

        public void UpdateMove(Vector2 value)
        {
            _inputModel.UpdateMove(value);
        }

        public void OnAnyAction(InputAction.CallbackContext context)
        {
            _inputModel.OnAnyAction(context);
        }
    }
}
