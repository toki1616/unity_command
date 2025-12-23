using UnityEngine;
using Zenject;

namespace My.Command
{
    public class BaseInputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputInstaller.Install(Container);
            CommandInstaller.Install(Container);
            CharacterInstaller.Install(Container);
        }
    }
}
