using UnityEngine;
using Zenject;

namespace My.Command
{
    public class CharacterInstaller : Installer<CharacterInstaller>
    {
        public override void InstallBindings()
        {
            //Debug.Log("CharacterInstaller : InstallBindings");

            Container.Bind<CharacterModel>().AsSingle();
            Container.Bind<CharacterViewModel>().AsSingle().NonLazy();
        }
    }
}
