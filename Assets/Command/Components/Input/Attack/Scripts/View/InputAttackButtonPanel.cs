using UnityEngine;
using UnityEngine.UI;
using Zenject;
using R3;
using ObservableCollections;

namespace My.Command{
    public class InputAttackButtonPanel : MonoBehaviour
    {
        private InputViewModel _inputViewModel;

        [Inject]
        public void Construct
            (
                InputViewModel inputViewModel
            )
        {
            _inputViewModel = inputViewModel;

            _inputViewModel.PressedAttacksObservable
                .Subscribe(_ => PressedAttack(_))
                .AddTo(this);

            _inputViewModel.ReleasedAttacksObservable
                .Subscribe(_ => ReleasedAttack(_))
                .AddTo(this);
        }

        [SerializeField]
        private Image attackImage;

        [SerializeField]
        private InputAttack _inputAttack;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //初期に色を追加するため
            ReleasedAttack(_inputAttack);
        }

        private void PressedAttack(InputAttack inputAttack)
        {
            if (_inputAttack != inputAttack) return;

            attackImage.color = _inputAttack.GetColor(true);
        }

        private void ReleasedAttack(InputAttack inputAttack)
        {
            if (_inputAttack != inputAttack) return;

            attackImage.color = _inputAttack.GetColor(false);
        }
    }
}
