using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    private enum FadeState
    {
        None,
        FadingOut,
        FadingIn
    }

    public AudioSource bgmSource;
    public AudioSource efxSource;
    public AudioSource loopingEfxSource;

    public AudioClip defaultClip;

    public static SoundManager instance = null;

    public bool bgmIsPlaying = true;
    public bool efxIsPlaying = true;
    public bool loopingEfxIsPlaying = true;

    private float bgmVolume;
    private float efxVolume;

    public float bgmMaxVolume;
    public float efxMaxVolume;

    private AudioClip nextClip;
    [SerializeField]
    private FadeState state;
    [SerializeField]
    private float fadeThreshold = 0.01f;
    [SerializeField]
    private float fadeInSpeed = 0.05f;
    [SerializeField]
    private float fadeOutSpeed = 0.2f;

	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(transform.parent);
        
        bgmSource.loop = true;
        loopingEfxSource.loop = true;

        bgmVolume = bgmSource.volume;
        efxVolume = efxSource.volume;
        bgmMaxVolume = bgmVolume;
        efxMaxVolume = efxVolume;
        playBGM(defaultClip);
    }
	
	public void playBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
        bgmIsPlaying = true;
    }

    public void playEfx(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
        efxIsPlaying = true;
    }

    public void playEfx(AudioClip clip, bool loop)
    {
        loopingEfxSource.clip = clip;
        loopingEfxSource.loop = loop;
        loopingEfxSource.Play();
        loopingEfxIsPlaying = true;
    }

    public void setBGMVolume(float vol)
    {
        bgmMaxVolume = vol;
        bgmSource.volume = vol;
        bgmVolume = bgmSource.volume;
    }

    public void setEfxVolume(float vol)
    {
        efxMaxVolume = vol;
        efxSource.volume = vol;
        loopingEfxSource.volume = vol;
        efxVolume = efxSource.volume;
    }

    public void pauseBGM()
    {
        if (bgmIsPlaying)
        {
            bgmSource.Pause();
            bgmIsPlaying = false;
        }
        else
        {
            bgmSource.UnPause();
            bgmIsPlaying = true;
        }
    }

    public void pauseEfx()
    {
        if (efxIsPlaying)
        {
            efxSource.Pause();
            efxIsPlaying = false;
        }
        else
        {
            efxSource.UnPause();
            efxIsPlaying = true;
        }

        if (loopingEfxIsPlaying)
        {
            loopingEfxSource.Pause();
            loopingEfxIsPlaying = false;
        }
        else
        {
            loopingEfxSource.UnPause();
            loopingEfxIsPlaying = true;
        }
    }

    public void stopBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
    }

    public void stopEfx()
    {
        if (efxSource.isPlaying)
            efxSource.Stop();
        if (loopingEfxSource.isPlaying)
            loopingEfxSource.Stop();
    }

    public void Fade(AudioClip clip, float inSpeed = 0.05f, float outSpeed = 0.2f)
    {
        if (clip == null || clip == bgmSource.clip)
            return;

        nextClip = clip;
        fadeInSpeed = inSpeed;
        fadeOutSpeed = outSpeed;

        if (bgmSource.enabled)
        {
            if (bgmSource.isPlaying)
                state = FadeState.FadingOut;
            else
                FadeToNextClip();

        }
        else
            FadeToNextClip();
    }

    private void FadeToNextClip()
    {
        bgmSource.clip = nextClip;
        state = FadeState.FadingIn;

        if (bgmSource.enabled)
            bgmSource.Play();
    }

    void Update()
    {
        if (!bgmSource.enabled)
            return;

        if (state == FadeState.FadingOut)
        {
            if (bgmVolume > fadeThreshold)
            {
                bgmVolume -= fadeOutSpeed * Time.deltaTime;
                bgmSource.volume = bgmVolume;
            }
            else
            {
                FadeToNextClip();
            }
        }
        else if (state == FadeState.FadingIn)
        {
            if (bgmVolume <= bgmMaxVolume)
            {
                bgmVolume += fadeInSpeed * Time.deltaTime;
                bgmSource.volume = bgmVolume;
            }
            else
            {
                state = FadeState.None;
            }
        }
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
