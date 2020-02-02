using System.Collections;
﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    public List<GameObject> particles;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject InstantiateParticle(string name, Transform destTransform, bool shouldParent = false)
    {
        var prefabReference = particles.FirstOrDefault(p => p.name == name);

        if (prefabReference == null)
        {
            return null;
        }

        var instance = Instantiate(prefabReference, destTransform.position, destTransform.rotation);

        if (shouldParent)
        {
            instance.transform.SetParent(destTransform, false);
            instance.transform.localPosition = Vector3.zero;
        }

        return instance;
    }

    public GameObject InstantiateParticle(string name, Vector3 pos)
    {
        var prefabReference = particles.FirstOrDefault(p => p.name == name);

        if (prefabReference == null)
        {
            return null;
        }

        var instance = Instantiate(prefabReference, pos, Quaternion.identity);

        return instance;
    }
}
