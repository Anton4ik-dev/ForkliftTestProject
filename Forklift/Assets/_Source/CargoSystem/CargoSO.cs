using UnityEngine;

namespace CargoSystem
{
    [CreateAssetMenu(fileName = "CargoSO", menuName = "Scriptable Objects/CargoSO")]
    public class CargoSO : ScriptableObject
    {
        [Header("Spawn Settings")]
        [field: SerializeField] public float SpawnDuration { get; private set; }
        [field: SerializeField] public float SpawnHeight { get; private set; }
        [field: SerializeField] public Vector3 SpawnRotation { get; private set; }

        [Header("Unload Settings")]
        [field: SerializeField] public float UnloadDuration { get; private set; }
        [field: SerializeField] public float UnloadHeight { get; private set; }
        [field: SerializeField] public Vector3 UnloadRotation { get; private set; }
    }
}