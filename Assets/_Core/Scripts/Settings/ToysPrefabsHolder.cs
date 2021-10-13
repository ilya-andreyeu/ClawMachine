using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ToysPrefabsHolder
{
    [SerializeField] private List<GameObject> toysPrefabs = new List<GameObject>();

    public GameObject GetToyPrefab()
    {
        var prefab = toysPrefabs[Random.Range(0, toysPrefabs.Count)];
        return prefab;
    }
}