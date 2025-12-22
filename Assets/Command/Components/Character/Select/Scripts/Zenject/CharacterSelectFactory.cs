using UnityEngine;
using Zenject;

public class CharacterSelectFactory : IFactory<GameObject>
{
    private readonly Transform _spawnRoot;
    private readonly DiContainer _container;
    private readonly GameObject _prefab;

    public CharacterSelectFactory(Transform spawnRoot, DiContainer container, GameObject prefab)
    {
        _spawnRoot = spawnRoot;
        _container = container;
        _prefab = prefab;
    }

    public GameObject Create()
    {
        return _container.InstantiatePrefab(_prefab, _spawnRoot);
    }
}
