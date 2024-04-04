using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    public Particle[] particles;

    private void OnEnable()
    {
        PlayerManager.Instance.OnPlayerTap += SpawnTapParticle;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void SpawnParticle(string name, Vector3 pos)
    {
        Particle p = Array.Find(particles, p => p.name == name);
        if (p == null) return;
        Vector3 worldPos = new Vector3(pos.x, pos.y, 0f);
        GameObject pObj = Instantiate(p.prefab, worldPos, Quaternion.identity);
        Animator a = pObj.GetComponent<Animator>();
        Destroy(pObj, a.GetCurrentAnimatorStateInfo(0).length);
    }

    void SpawnTapParticle()
    {
        SpawnParticle("TapParticle", PlayerManager.Instance.GetWorldPosFormTouchPoint3D());
    }
}

[Serializable]
public class Particle
{
    public string name;
    public GameObject prefab;
}
