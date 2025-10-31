using UnityEngine;
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
    }
}
