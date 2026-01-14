using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace My.Command
{
    public class CharacterFactoryInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform _spawnRoot;

        [SerializeField]
        private GameObject _characterSelectUIPrefab;

        [SerializeField]
        private ToggleGroup _toggleGroup;

        public override void InstallBindings()
        {
            Container.Bind<CharacterSelectFactory>().AsSingle().WithArguments(_spawnRoot, _characterSelectUIPrefab, _toggleGroup);
        }
    }
}
