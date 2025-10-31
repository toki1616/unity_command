using UnityEngine;
using Zenject;

namespace MyCommand
{
    public class BaseInputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputInstaller.Install(Container);
            CommandInstaller.Install(Container);
        }
    }
}
