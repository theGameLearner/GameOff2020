using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVfx : MonoBehaviour
{
    ParticleSystem[] psList;

    float activeDuration = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

        psList = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in psList)
        {
            if(ps.duration > activeDuration){
                activeDuration = ps.duration;
            }
        }
    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        foreach (ParticleSystem ps in psList)
        {
            ps.Play();
        }
        Invoke(nameof(ReturntoPool),activeDuration);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        foreach (ParticleSystem ps in psList)
        {
            ps.Stop();
        }
    }

    void ReturntoPool(){
        ReturnList.instance.ReturnIfExisting(gameObject);
    }
}
