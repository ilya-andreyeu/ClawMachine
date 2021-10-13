using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ToysSpawner : MonoBehaviour
{
    [SerializeField] private ToysSpawnerSettings settings;
    
    [SerializeField] private GameObject boxLocker;
    
    private List<GameObject> spawnedToys = new List<GameObject>();

    private void Awake()
    {
        CheckNull();
        Initialize();
    }

    private void CheckNull()
    {
        if(settings == null) Debug.LogError("Settings is null");
        if(boxLocker == null) Debug.LogError("BoxLocker is null");
    }

    public void Initialize()
    {
        ClearToys();
        SpawnToys();
    }

    private void ClearToys()
    {
        foreach (var toy in spawnedToys)
        {
            GameObject.Destroy(toy);
        }
        spawnedToys.Clear();
    }

    private void SpawnToys()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < settings.SpawnCount; i++)
        {
            var prefab = settings.GetToyPrefab();
            var spawnXOffset = Random.Range(-settings.SpawnRange.x, settings.SpawnRange.x);
            var spawnZOffset = Random.Range(-settings.SpawnRange.y, settings.SpawnRange.y);
            Vector3 spawnPosition = transform.position + new Vector3(spawnXOffset, 0f, spawnZOffset);
            
            var toy = Instantiate(prefab, spawnPosition, Random.rotation);
            spawnedToys.Add(toy);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        boxLocker.SetActive(false);
    }
}