using UnityEngine;
using UnityEngine.InputSystem;

namespace ForkliftSystem
{
    public class ForkliftController
    {
        private readonly ForkliftSO _forkliftSo;
        private readonly ForkliftView _view;

        private InputAction _moveAction;
        private InputAction _turnAction;
        private InputAction _liftUpAction;
        private InputAction _liftDownAction;
        private InputAction _engineAction;

        private bool _engineOn;
        private float _currentFuel;
        private float _currentLiftHeight;

        public ForkliftController(ForkliftSO forkliftSo, ForkliftView view)
        {
            _forkliftSo = forkliftSo;
            _view = view;

            _engineOn = false;
            _currentFuel = forkliftSo.MaxFuel;
            _currentLiftHeight = forkliftSo.MinLiftHeight;

            Bind();
        }

        private void Bind()
        {
            PlayerInput input = _view.PlayerInput;

            _moveAction = input.actions["Move"];
            _turnAction = input.actions["Turn"];
            _liftUpAction = input.actions["LiftUp"];
            _liftDownAction = input.actions["LiftDown"];
            _engineAction = input.actions["ToggleEngine"];

            _engineAction.started += OnEngineToggle;

            _view.OnUpdate += Update;
            _view.OnFixedUpdate += FixedUpdate;
        }

        public void Dispose()
        {
            _engineAction.started -= OnEngineToggle;

            _view.OnUpdate -= Update;
            _view.OnFixedUpdate -= FixedUpdate;
        }

        private void Update()
        {
            if (!_engineOn) return;

            HandleTurning();
            HandleLifting();
        }

        private void FixedUpdate()
        {
            if (!_engineOn) return;

            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 moveInput = _moveAction.ReadValue<Vector2>();
            if (moveInput != Vector2.zero)
            {
                float speedMultiplier = _currentFuel < _forkliftSo.LowFuelThreshold
                    ? _forkliftSo.LowFuelSpeedMultiplier
                    : 1f;

                _view.Rb.AddForce(_view.transform.forward * moveInput.y * _forkliftSo.MoveSpeed * speedMultiplier);
                ConsumeFuel(true);
            }
        }

        private void HandleTurning()
        {
            Vector2 turnInput = _turnAction.ReadValue<Vector2>();
            if (turnInput.x != 0)
            {
                _view.transform.Rotate(0, turnInput.x * _forkliftSo.RotationSpeed * Time.deltaTime, 0);
                ConsumeFuel(true);
            }
        }

        private void HandleLifting()
        {
            float liftInput = 0f;

            if (_liftUpAction.ReadValue<float>() > 0 && _currentLiftHeight < _forkliftSo.MaxLiftHeight)
            {
                liftInput = 1f;
            }
            else if (_liftDownAction.ReadValue<float>() > 0 && _currentLiftHeight > _forkliftSo.MinLiftHeight)
            {
                liftInput = -1f;
            }

            if (liftInput != 0f)
            {
                _currentLiftHeight += liftInput * _forkliftSo.ForkLiftSpeed * Time.deltaTime;
                _currentLiftHeight = Mathf.Clamp(
                    _currentLiftHeight,
                    _forkliftSo.MinLiftHeight,
                    _forkliftSo.MaxLiftHeight);

                _view.UpdateForkPosition(_currentLiftHeight);
                ConsumeFuel(true);
            }
        }

        private void OnEngineToggle(InputAction.CallbackContext context)
        {
            _engineOn = !_engineOn;
            Debug.Log($"Engine {(_engineOn ? "started" : "stopped")}");
        }

        private void ConsumeFuel(bool isActionPerformed)
        {
            if (!_engineOn || !isActionPerformed) return;

            _currentFuel -= _forkliftSo.FuelConsumptionRate * Time.deltaTime;
            _currentFuel = Mathf.Clamp(_currentFuel, 0, _forkliftSo.MaxFuel);
            _view.UpdateFuelUI(_currentFuel, _forkliftSo.MaxFuel);
        }
    }
}