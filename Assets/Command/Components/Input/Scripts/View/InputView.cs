using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using R3;

namespace My.Command
{
    public class InputView : MonoBehaviour
    {
        private InputViewModel _inputViewModel;

        [Inject]
        public void Construct(InputViewModel inputViewModel)
        {
            _inputViewModel = inputViewModel;
        }

        [SerializeField]
        private InputActionAsset _inputActionAsset;

        private CompositeDisposable _disposables = new CompositeDisposable();

        private void OnEnable()
        {
            if (_inputActionAsset == null) return;

            foreach (var map in _inputActionAsset.actionMaps)
            {
                map.Enable();

                foreach (var action in map.actions)
                {
                    MonitorActionEveryUpdate(action);
                }
            }
        }

        private void OnDisable()
        {
            _disposables.Dispose();

            if (_inputActionAsset == null) return;

            foreach (var map in _inputActionAsset.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    if (action.type == InputActionType.Button)
                    {
                        action.performed -= OnPress;
                        action.canceled -= OnRelease;
                    }

                    action.Disable();
                }
            }
        }

        private void MonitorActionEveryUpdate(InputAction action)
        {
            //Debug.Log($"InputView : action.name : {action.name}, action.expectedControlType : {action.expectedControlType}, action.type : {action.type}");
            action.Enable();

            switch (action.type)
            {
                case InputActionType.Value:
                    switch (action.expectedControlType)
                    {
                        case "Vector2":
                            Observable.EveryUpdate()
                                .Select(_ => action.ReadValue<Vector2>())
                                .DistinctUntilChanged()
                                .Subscribe(value =>
                                {
                                    _inputViewModel.OnVector2Action(action.name, value);
                                })
                                .AddTo(_disposables);
                            break;

                        case "Float":
                            Observable.EveryUpdate()
                                .Select(_ => action.ReadValue<float>())
                                .DistinctUntilChanged()
                                .Subscribe(value =>
                                {
                                    _inputViewModel.OnFloatAction(action.name, value);
                                })
                                .AddTo(_disposables);
                            break;

                        default:
                            //Debug.LogWarning($"InputView : Unsupported Value control type : action.name : {action.name}, action.expectedControlType : {action.expectedControlType}");
                            break;
                    }
                    break;

                case InputActionType.Button:
                    action.performed += OnPress;
                    action.canceled += OnRelease;
                    //Debug.Log($"InputView : Button callbacks registered : action.name : {action.name}");
                    break;

                case InputActionType.PassThrough:
                    //Debug.Log($"InputView : PassThrough action ignored : action.name : {action.name}");
                    break;

                default:
                    //Debug.LogWarning($"InputView : Unknown action type : action.name : {action.name}, action.type : {action.type}");
                    break;
            }
        }

        // 押された瞬間のコールバック
        public void OnPress(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            _inputViewModel.OnButtonPressed(context.action.name);
        }

        // 離された瞬間のコールバック
        public void OnRelease(InputAction.CallbackContext context)
        {
            if (context.performed) return;
            _inputViewModel.OnButtonReleased(context.action.name);
        }
    }
}
