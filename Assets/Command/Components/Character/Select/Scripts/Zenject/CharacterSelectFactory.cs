using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace My.Command
{
    public class CharacterSelectFactory : IFactory<GameObject>
    {
        private readonly DiContainer _container;
        private readonly Transform _spawnRoot;
        private readonly GameObject _prefab;
        private readonly ToggleGroup _toggleGroup;

        public CharacterSelectFactory(DiContainer container, Transform spawnRoot, GameObject prefab, ToggleGroup toggleGroup)
        {
            _container = container;
            _spawnRoot = spawnRoot;
            _prefab = prefab;
            _toggleGroup = toggleGroup;
        }

        public GameObject Create()
        {
            var view = _container.InstantiatePrefabForComponent<CharacterSelectView>(_prefab, _spawnRoot);
            view.SetToggleGroup(_toggleGroup);
            
            return view.gameObject;
        }
    }
}
