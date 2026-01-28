using UnityEngine;
using Zenject;

namespace My.Command
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            //Debug.Log("InputInstaller : InstallBindings");

            Container.BindInterfacesAndSelfTo<InputModel>().AsSingle();

            Container.Bind<InputViewModel>().AsSingle();
        }
    }
}
