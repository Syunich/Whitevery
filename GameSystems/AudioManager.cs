using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SyunichTool;

public class AudioManager : SingletonMonovehavior<AudioManager>
{
    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SESource;
    [SerializeField] private AudioClip[] BGMs;
    [SerializeField] private  AudioClip[] SEs;


    protected override bool IsDestroyOnLoad
    {
        get => false;
    }

    public float BGMvolume
    {
        get => BGMSource.volume;
        set => BGMSource.volume = value;
    }

    public float SEvolume
    {
        get => SESource.volume;
        set => SESource.volume = value;
    }
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayBGM(int index)
    {
        BGMSource.clip = BGMs[index];
        BGMSource.Play();
    }
    public void PlaySE(int index)
    {
        SESource.PlayOneShot(SEs[index]);
    }
    
}