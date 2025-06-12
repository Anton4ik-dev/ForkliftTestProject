using UnityEngine;

namespace ForkliftSystem
{
    [CreateAssetMenu(fileName = "ForkliftSO", menuName = "Scriptable Objects/ForkliftSO")]
    public class ForkliftSO : ScriptableObject
    {
        [Header("Movement Settings")]
        [field: SerializeField] public float MoveSpeed {get; private set;}
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float LowFuelSpeedMultiplier { get; private set; }
        [field: SerializeField] public float LowFuelThreshold { get; private set; }

        [Header("Fork Settings")]
        [field: SerializeField] public float ForkLiftSpeed { get; private set; }
        [field: SerializeField] public float MaxLiftHeight { get; private set; }
        [field: SerializeField] public float MinLiftHeight { get; private set; }

        [Header("Fuel Settings")]
        [field: SerializeField] public float MaxFuel { get; private set; }
        [field: SerializeField] public float FuelConsumptionRate { get; private set; }
    }
}