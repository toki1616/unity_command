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
        private Button _button;

        [SerializeField]
        private Text _text;

        private CharacterData _characterData;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _button.OnClickAsObservable().Subscribe(_ => SelectCharacter()).AddTo(this);
        }

        public void SetCharacterData(CharacterData characterData)
        {
            _characterData = characterData;
            _text.text = characterData.Character.GetName();
        }

        public void SelectCharacter()
        {
            _characterViewModel.UpdateCharacter(_characterData.Character);
        }
    }
}
