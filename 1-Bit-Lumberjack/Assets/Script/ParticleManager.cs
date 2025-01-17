using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    public Particle[] particles;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SpawnParticle(string name, Vector3 pos)
    {
        Particle p = Array.Find(particles, p => p.name == name);
        if (p == null) return;
        Vector3 worldPos = new Vector3(pos.x, pos.y, 0f);
        GameObject pObj = Instantiate(p.prefab, worldPos, Quaternion.identity);
        if (pObj.TryGetComponent<ParticleParent>(out ParticleParent pParent))
        {
            Destroy(pObj, pParent.GetMainParticleTime());
        }
        else
        {
            Animator a = pObj.GetComponent<Animator>();
            Destroy(pObj, a.GetCurrentAnimatorStateInfo(0).length);
        }

    }

}

[Serializable]
public class Particle
{
    public string name;
    public GameObject prefab;
}
