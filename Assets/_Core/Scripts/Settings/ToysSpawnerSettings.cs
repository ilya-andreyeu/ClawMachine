using UnityEngine;

[CreateAssetMenu(fileName = "ToysSpawnerSettings", menuName = "Custom/Settings/ToysSpawnerSettings")]
public class ToysSpawnerSettings : ScriptableObject
{
    [SerializeField] private Vector2 spawnRange;
    [SerializeField] private int spawnCount;
    [SerializeField] private ToysPrefabsHolder prefabs;

    public Vector2 SpawnRange => spawnRange;

    public int SpawnCount => spawnCount;

    public GameObject GetToyPrefab()
    {
        return prefabs.GetToyPrefab();
    }
}