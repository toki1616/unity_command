using UnityEngine;
using UnityEngine.UI;
using Zenject;
using R3;

namespace My.Command
{
    public class CommandView : MonoBehaviour
    {
        private CommandViewModel _commandViewModel;

        [Inject]
        public void Construct
           (
               CommandViewModel commandViewModel
           )
        {
            _commandViewModel = commandViewModel;

            _commandViewModel.SpecialAttackObservable
                .Subscribe(_ => SuccessSpecialAttack(_))
                .AddTo(this);

            _commandViewModel.NormalAttackObservable
                .Subscribe(_ => SuccessNormallAttack(_))
                .AddTo(this);
        }

        [SerializeField]
        private Text _text;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SuccessSpecialAttack(SpecialAttack specialAttack)
        {
            UpdateAttackText(specialAttack.ToString());
        }

        private void SuccessNormallAttack(NormalAttack normalAttack)
        {
            UpdateAttackText(normalAttack.ToString());
        }

        private void UpdateAttackText(string attackName)
        {
            _text.text = $"{attackName}";
        }
    }
}
