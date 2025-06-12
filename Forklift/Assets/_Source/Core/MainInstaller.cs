using CargoSystem;
using ForkliftSystem;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private ForkliftSO _forkliftSo;
        [SerializeField] private ForkliftView _forkliftView;
        [SerializeField] private CargoSO _cargoSo;

        public override void InstallBindings()
        {
            InstallCargoSystem();
            InstallForkliftSystem();
        }

        private void InstallForkliftSystem()
        {
            Container
                .Bind<ForkliftSO>()
                .FromInstance(_forkliftSo)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<ForkliftView>()
                .FromInstance(_forkliftView)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<ForkliftController>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallCargoSystem()
        {
            Container
                .Bind<CargoSO>()
                .FromInstance(_cargoSo)
                .AsSingle()
                .NonLazy();
        }
    }
}