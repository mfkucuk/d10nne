using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockObject : MonoBehaviour
{

    [SerializeField] private Animator unlockAnimation;
    [SerializeField] private ParticleSystem particleP;
    [SerializeField] private ParticleSystem particleO;

    public void Unlock()
    {
        unlockAnimation.Play("Unlock");
    }

    private void BOOM()
    {
        particleP.Play();
        particleO.Play();
    }
    
}

