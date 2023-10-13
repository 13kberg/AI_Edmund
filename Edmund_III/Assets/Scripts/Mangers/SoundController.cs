using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public bool m_musicEnabled = true;
    public bool m_fxEnabled = true;
    
    [Range(0, 1)] public float m_musicVolume = 1.0f;
    [Range(0, 1)] public float m_fxVolume = 1.0f;

    //ALL GAME SOUNDS
    public AudioClip m_gameOver;
    public AudioClip m_landed;
    public AudioClip m_levelUp;
    public AudioClip m_lineCleared;
    public AudioClip m_moved;
    public AudioClip m_rotated;

    //TO RANDOMISE THE BG MUSIC
    public AudioClip[] m_musicClips;
    public AudioSource m_bgMusicSouce; 
    AudioClip m_randomMusicClip;    
    
    //ICON TOGGLE
    public IconToggle m_musicIconToggle;
    public IconToggle m_fxIconToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        m_randomMusicClip = GetRandomClip(m_musicClips);
        PlayBackgroundMusic (m_randomMusicClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ToggleMusic()
    {
        m_musicEnabled = !m_musicEnabled;
        UpdateMusic();
        
        if (m_musicIconToggle)
        {
            m_musicIconToggle.ToggleIcon(m_musicEnabled);
        }
    }

    public void ToggleFX()
    {
        m_fxEnabled = !m_fxEnabled;

        if (m_fxIconToggle)
        {
            m_fxIconToggle.ToggleIcon(m_fxEnabled);
        }

    }
    
    void UpdateMusic ()
    {
        if (m_bgMusicSouce.isPlaying != m_musicEnabled) 
        {
            if (m_musicEnabled) 
            {
                m_randomMusicClip = GetRandomClip(m_musicClips);
                PlayBackgroundMusic (m_randomMusicClip);
            }
            else {
                m_bgMusicSouce.Stop ();
            }
        }
    }
    
    public AudioClip GetRandomClip(AudioClip[] clips)
    {
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        return randomClip;
    }
    
    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (!m_musicEnabled || !musicClip || !m_bgMusicSouce)
        {
            return;
        }
        
        m_bgMusicSouce.Stop();

        m_bgMusicSouce.clip = musicClip;
        
        m_bgMusicSouce.volume = m_musicVolume;
        
        m_bgMusicSouce.loop = true;
        
        m_bgMusicSouce.Play();        
    } 
}
