using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;

    private AudioSource audioSource;

    public AudioClip clip_BackgroundMusic;
    public AudioClip clip_Walk;

    private void Awake()
    {
        if (inst)
        {
            Destroy(gameObject);
            return;
        }
        inst = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayBackgroundSound();
    }

    public static SoundManager Inst
    {
        get
        {
            if (null == inst)
            {
                return null;
            }
            return inst;
        }
    }

    private void PlayBackgroundSound()
    {
        audioSource.PlayOneShot(clip_BackgroundMusic);
    }

    // 발걸음 소리 재생 함수
    public void PlayWalkSound1()
    {
        audioSource.PlayOneShot(clip_Walk);
    }
}
