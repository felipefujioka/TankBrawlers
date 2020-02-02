using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PropSpawnerManager : MonoBehaviour
{
    public static PropSpawnerManager Instance;
    public List<Transform> spawnPositions;
    public Bullet bulletPrefab;
    public List<DestructiveProp> destructivePropPrefabs;
    public List<Prop> inStageProps;
    public GameObject parachutePrefab;

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        StartCoroutine(PropSpawnDelay());
    }

    IEnumerator PropSpawnDelay()
    {
        yield return new WaitForSeconds(GameConstants.PROP_SPAWN_DELAY);
        if (inStageProps.Count < GameConstants.MAX_PROP_SPAWNS)
        {
            var rndProp = Random.Range(0f, 1f);
            if (rndProp > 0.8f)
            {
                SpawnProp(bulletPrefab);
            }
            else
            {
                SpawnProp(destructivePropPrefabs[Random.Range(0,destructivePropPrefabs.Count)]);
            }
        }
        
        StartCoroutine(PropSpawnDelay());
    }
    
    public void SpawnProp(Prop prop)
    {
        Prop currProp = Instantiate(prop, spawnPositions[Random.Range(0, spawnPositions.Count)]);
        currProp.transform.localPosition = Vector3.zero;
        currProp.transform.SetParent(null);

        GameObject parachute = Instantiate(parachutePrefab, currProp.transform);
        parachute.name = "Parachute";
        parachute.transform.localPosition = Vector3.zero;
        
        inStageProps.Add(currProp);
    }

    public void RemoveProp(Prop prop)
    {
        inStageProps.Remove(prop);
    }
}
