using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEffect : MonoBehaviour
{
    public ParticleSystem particlesTrail,eyeParticle;

    public void PlayTrailParticles()
    {
        particlesTrail.Play();
    }

    public void PlayEyeParticles()
    {
        eyeParticle.Play();
    }
}
