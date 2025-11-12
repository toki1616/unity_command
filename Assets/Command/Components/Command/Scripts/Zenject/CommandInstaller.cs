using UnityEngine;
using Zenject;

namespace My.Command
{
    public class CommandInstaller : Installer<CommandInstaller>
    {
        public override void InstallBindings()
        {
            //Debug.Log("CommandInstaller : InstallBindings");

            Container.Bind<CommandModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<CommandViewModel>().AsSingle();
        }
    }
}
