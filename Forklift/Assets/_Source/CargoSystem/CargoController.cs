using UnityEngine;
using DG.Tweening;
using Zenject;

namespace CargoSystem
{
    public class CargoController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Transform _unloadPlatform;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        private CargoSO _cargoSo;

        [Inject]
        private void Construct(CargoSO cargoSO)
        {
            _cargoSo = cargoSO;
            _originalPosition = transform.position;
            _originalRotation = transform.rotation;
            PlaySpawnAnimation();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == _unloadPlatform)
            {
                DOVirtual.DelayedCall(2f, PlayUnloadAnimation);
            }
        }

        private void PlaySpawnAnimation()
        {
            _rb.constraints = RigidbodyConstraints.FreezePositionY;
            transform.position = _originalPosition + Vector3.up * _cargoSo.SpawnHeight;
            transform.rotation = _originalRotation;

            transform.DOMove(_originalPosition, _cargoSo.SpawnDuration);
            transform.DORotate(_cargoSo.SpawnRotation, _cargoSo.SpawnDuration, RotateMode.LocalAxisAdd)
                .OnComplete(() => _rb.constraints = RigidbodyConstraints.None);
        }

        private void PlayUnloadAnimation()
        {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.DOMove(transform.position + Vector3.up * _cargoSo.UnloadHeight, _cargoSo.UnloadDuration);
            transform.DORotate(_cargoSo.UnloadRotation, _cargoSo.UnloadDuration, RotateMode.LocalAxisAdd);
        }
    }
}