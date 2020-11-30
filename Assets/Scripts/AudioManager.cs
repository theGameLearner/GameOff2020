﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGL.Singletons;

public class AudioManager : GenericSingletonMonobehaviour<AudioManager>
{

    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource bgAudioSource;

    [SerializeField] Clip BgMusic;
    [SerializeField] Clip killTurretClip;
    [SerializeField] Clip wallHitClip;
    [SerializeField] Clip breakAblewallClip;
    [SerializeField] Clip bulletHitClip;
    [SerializeField] Clip playerDiedClip;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if(BgMusic.clip!=null){
            bgAudioSource.clip = BgMusic.clip;
            bgAudioSource.volume = BgMusic.volumeMultiplier;
            bgAudioSource.Play();
        }

        sfxAudioSource.volume = 1;
    }

    public void PlaykillTurretClip(){
        if(killTurretClip.clip!=null){
            sfxAudioSource.PlayOneShot(killTurretClip.clip,killTurretClip.volumeMultiplier);
        }
    }
    public void PlaywallHitClip(){
        if(wallHitClip.clip!=null){
            sfxAudioSource.PlayOneShot(wallHitClip.clip,wallHitClip.volumeMultiplier);
        }
    }
    public void PlaybreakAblewallClip(){
        if(breakAblewallClip.clip!=null){
            sfxAudioSource.PlayOneShot(breakAblewallClip.clip,breakAblewallClip.volumeMultiplier);
        }
    }
    public void PlaybulletHitClip(){
        if(bulletHitClip.clip!=null){
            sfxAudioSource.PlayOneShot(bulletHitClip.clip,bulletHitClip.volumeMultiplier);
        }
    }
    public void PlayplayerDiedClip(){
        if(playerDiedClip.clip!=null){
            sfxAudioSource.PlayOneShot(playerDiedClip.clip,playerDiedClip.volumeMultiplier);
        }
    }
}


[System.Serializable]
public struct Clip{
    public AudioClip clip;

    [Range(0,1)]
    public float volumeMultiplier;
}