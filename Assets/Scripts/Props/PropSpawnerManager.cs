using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PropSpawnerManager : MonoBehaviour
{
    public List<Transform> spawnPositions;
    public Bullet bulletPrefab;
    public DestructiveProp destructivePropPrefab;

    void Start()
    {
        StartCoroutine(PropSpawnDelay());
    }

    IEnumerator PropSpawnDelay()
    {
        yield return new WaitForSeconds(GameConstants.PROP_SPAWN_DELAY);
        var rndProp = Random.Range(0f, 1f);
        if (rndProp > 0.8f)
        {
            SpawnProp(bulletPrefab);
        }
        else
        {
            SpawnProp(destructivePropPrefab);
        }
        
        StartCoroutine(PropSpawnDelay());
    }
    
    public void SpawnProp(Prop prop)
    {
        Prop currProp = Instantiate(prop, spawnPositions[Random.Range(0, spawnPositions.Count)]);
        currProp.transform.localPosition = Vector3.zero;
    }
}
