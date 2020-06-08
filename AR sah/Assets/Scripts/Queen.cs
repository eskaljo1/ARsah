using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TurnOffParticles());
    }

    IEnumerator TurnOffParticles()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }
}
