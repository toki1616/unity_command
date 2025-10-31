using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using R3;

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

        [SerializeField]
        private InputActionAsset _inputActionAsset;

        private InputAction _moveAction;

        private CompositeDisposable _disposables = new CompositeDisposable();

        private void OnEnable()
        {
            if (_inputActionAsset == null) return;

            foreach (var map in _inputActionAsset.actionMaps)
            {
                map.Enable();

                foreach (var action in map.actions)
                {
                    action.performed += OnAnyActionPerformed;
                }
            }

            _moveAction = _inputActionAsset.FindAction(InputName.Move.ToString());
            _moveAction?.Enable();

            // 毎フレーム入力値を確認
            Observable.EveryUpdate()
                .Select(_ => _moveAction.ReadValue<Vector2>())
                .DistinctUntilChanged()
                .Subscribe(v => 
                {
                    UpdateMoveInput(v);
                })
                .AddTo(_disposables);
        }

        private void OnDisable()
        {
            if (_inputActionAsset == null) return;

            foreach (var map in _inputActionAsset.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    action.performed -= OnAnyActionPerformed;
                }

                map.Disable();
            }
        }

        private void OnAnyActionPerformed(InputAction.CallbackContext context)
        {
            _inputViewModel.OnAnyAction(context);
        }

        private void UpdateMoveInput(Vector2 value)
        {
            _inputViewModel.UpdateMove(value);
        }
    }
}
