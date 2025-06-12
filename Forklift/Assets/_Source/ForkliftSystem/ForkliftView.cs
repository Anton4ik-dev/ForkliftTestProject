using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;
using DG.Tweening;

namespace ForkliftSystem
{
    public class ForkliftView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rb { get; private set; }
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        [SerializeField] private Transform _forkLiftPoint;
        [SerializeField] private Image _fuelImage;
        [SerializeField] private TextMeshProUGUI _fuelText;

        public Action OnUpdate;
        public Action OnFixedUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        public void UpdateFuelUI(float currentFuel, float maxFuel)
        {
            _fuelImage.fillAmount = currentFuel / maxFuel;
            _fuelText.text = $"{currentFuel / maxFuel * 100:F0}%";
        }

        public void UpdateForkPosition(float height)
        {
            _forkLiftPoint.DOLocalMoveY(height, 0.01f);
        }
    }
}