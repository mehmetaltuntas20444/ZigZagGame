using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalScript : MonoBehaviour
{

    //this script is just for handling the particles when we pick up points
    private ParticleSystem particleSys;
    void Start()
    {
        particleSys = GetComponent<ParticleSystem>();
        
    }

    void Update()
    {
        if (!particleSys.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
