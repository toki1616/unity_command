using UnityEngine;
using Zenject;

public class CharacterFactoryInstaller : MonoInstaller
{
    [SerializeField] private Transform _spawnRoot;
    [SerializeField] private GameObject _characterSelectUIPrefab;

    public override void InstallBindings()
    {
        Container.Bind<CharacterSelectFactory>().AsSingle().WithArguments(_spawnRoot, _characterSelectUIPrefab);
    }
}
