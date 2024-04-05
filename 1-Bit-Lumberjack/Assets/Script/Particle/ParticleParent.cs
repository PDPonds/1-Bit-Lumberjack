using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleParent : MonoBehaviour
{
    [SerializeField] Transform mainParticle;

    Animator anim;

    private void Awake()
    {
        anim = mainParticle.GetComponent<Animator>();
    }

    public float GetMainParticleTime()
    {
        return anim.GetCurrentAnimatorStateInfo(0).length;
    }

}
