using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using R3;

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

        public Observable<InputDirection> InputDirectionObservable => 
            _inputModel.InputDirectionRP
            .AsObservable()
            .DistinctUntilChanged();

        public void OnVector2Action(string actionName, Vector2 value)
        {
            _inputModel.HandleVector2(actionName, value);
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
