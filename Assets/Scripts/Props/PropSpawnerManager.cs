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
    public List<Bullet> inStageBullets;
    public List<DestructiveProp> inStageProps;
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
        while (!GameInfo.Instance.IsRunning)
        {
            yield return null;
        }
        
        yield return new WaitForSeconds(GameConstants.PROP_SPAWN_DELAY);
        if (inStageProps.Count < GameConstants.MAX_PROP_SPAWNS &&
            inStageBullets.Count < GameConstants.MAX_BULLET_SPAWNS)
        {
            var rndProp = Random.Range(0f, 1f);
            if (rndProp > 0.8f)
            {
                yield return new WaitForSecondsRealtime(4f);
                inStageBullets.Add(SpawnProp(bulletPrefab) as Bullet);
            }
            else
            {
                inStageProps.Add(
                    SpawnProp(destructivePropPrefabs[
                        Random.Range(0, destructivePropPrefabs.Count)]) as DestructiveProp);
            }
        }else if (inStageProps.Count >= GameConstants.MAX_PROP_SPAWNS &&
                  inStageBullets.Count < GameConstants.MAX_BULLET_SPAWNS)
        {
            yield return new WaitForSecondsRealtime(4f);
            inStageBullets.Add(SpawnProp(bulletPrefab) as Bullet);
        }
        else if(inStageProps.Count < GameConstants.MAX_PROP_SPAWNS &&
                inStageBullets.Count >= GameConstants.MAX_BULLET_SPAWNS)
        {
            inStageProps.Add(SpawnProp(destructivePropPrefabs[Random.Range(0, destructivePropPrefabs.Count)]) as DestructiveProp);
        }

        StartCoroutine(PropSpawnDelay());
    }
    
    public Prop SpawnProp(Prop prop)
    {
        Prop currProp = Instantiate(prop, spawnPositions[Random.Range(0, spawnPositions.Count)]);
        currProp.transform.localPosition = Vector3.zero;
        currProp.transform.localEulerAngles = Vector3.zero;
        currProp.transform.SetParent(null);

        GameObject parachute = Instantiate(parachutePrefab, currProp.transform);
        parachute.name = "Parachute";
        parachute.transform.localPosition = Vector3.zero;

        return currProp;
    }

    public void RemoveProp(Prop prop)
    {
        if(prop is Bullet)
            inStageBullets.Remove(prop as Bullet);
        else if(prop is DestructiveProp)
            inStageProps.Remove(prop as DestructiveProp);
    }
}
