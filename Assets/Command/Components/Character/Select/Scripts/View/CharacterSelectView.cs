using UnityEngine;
using UnityEngine.UI;
using R3;

namespace My.Command
{
    public class CharacterSelectView : MonoBehaviour
    {
        private CharacterViewModel _characterViewModel;

        public void Inject(CharacterViewModel characterViewModel)
        {
            _characterViewModel = characterViewModel;
        }

        [SerializeField]
        private Toggle _toggle;

        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private Text _text;

        private CharacterData _characterData;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _toggle.OnValueChangedAsObservable().Subscribe(_ => OnChangeToggle(_)).AddTo(this);
        }

        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            _toggle.group = toggleGroup;
        }

        public void SetCharacterData(CharacterData characterData)
        {
            _characterData = characterData;
            _text.text = characterData.Character.GetName();
        }

        private void OnChangeToggle(bool isActive)
        {
            UpdateColor();

            if (!isActive) 
                return;

            SelectCharacter();
        }

        void UpdateColor()
        {
            if (_toggle.isOn)
                backgroundImage.color = ColorConst.toggleSelectColor;
            else
                backgroundImage.color = ColorConst.toggleNormalColor;
        }

        private void SelectCharacter()
        {
            _characterViewModel.UpdateCharacter(_characterData.Character);
        }
    }
}
