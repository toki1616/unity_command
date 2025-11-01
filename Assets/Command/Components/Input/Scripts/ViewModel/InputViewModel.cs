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

        public void OnVector2Action(string actionName, Vector2 value)
        {
            _inputModel.HandleVector2(actionName, value);
        }

        public void OnButtonAction(string actionName, bool isPressed)
        {
            _inputModel.HandleButton(actionName, isPressed);
        }

        public void OnFloatAction(string actionName, float value)
        {
            _inputModel.HandleFloat(actionName, value);
        }

        public void OnButtonPressed(string actionName)
        {
            _inputModel.HandleButtonPressed(actionName);
        }

        public void OnButtonReleased(string actionName)
        {
            _inputModel.HandleButtonReleased(actionName);
        }
    }
}
