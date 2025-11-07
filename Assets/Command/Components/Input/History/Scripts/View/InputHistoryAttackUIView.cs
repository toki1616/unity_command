using UnityEngine;
using UnityEngine.UI;

namespace My.Command
{
    public class InputHistoryAttackUIView : MonoBehaviour
    {
        [SerializeField]
        private Image attackImage;

        [SerializeField]
        private InputAttack _inputAttack;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //初期に色を追加するため
            InitializeColor(_inputAttack);
        }

        private void InitializeColor(InputAttack inputAttack)
        {
            if (_inputAttack != inputAttack) return;

            attackImage.color = _inputAttack.GetColor(true);
        }
    }
}
