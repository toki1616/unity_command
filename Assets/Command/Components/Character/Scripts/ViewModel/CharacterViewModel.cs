using UnityEngine;
using Zenject;
using R3;

namespace My.Command
{
    public class CharacterViewModel
    {
        private CharacterSelectFactory _characterSelectFactory;
        private CharacterModel _characterModel;

        [Inject]
        public CharacterViewModel
            (
                CharacterModel characterModel,
                CharacterSelectFactory characterSelectFactory
            )
        {
            //Debug.Log("CharacterViewModel : Inject");
            _characterModel = characterModel;
            _characterSelectFactory = characterSelectFactory;

            foreach (var character in _characterModel.Characters)
            {
                CreateCharacterSelectUI(character);
            }
        }

        private void CreateCharacterSelectUI(CharacterData data) 
        { 
            var obj = _characterSelectFactory.Create();
            var view = obj.GetComponent<CharacterSelectView>();
            view.Inject(this);
            view.SetCharacterData(data); 
        }

        public void UpdateCharacter(CharacterType character)
        {
            _characterModel.UpdateCharacter(character);
        }

        public Observable<CharacterData> CharacterObservable =>
            _characterModel.CharacterRP
            .AsObservable()
            .DistinctUntilChanged();
    }
}
