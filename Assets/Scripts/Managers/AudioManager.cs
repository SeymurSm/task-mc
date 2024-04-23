using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource _audioPlayer;
    [SerializeField] AudioSource m_backgroundPlayer;
 
    [SerializeField] AudioClip m_pair, m_won, m_lol, m_cardShuffle, m_cardFlip, m_correctMatch, m_wrongMatch;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance != null && instance != this) 
        { 
            Destroy(this.gameObject); 
            //instance = this;
        } 
        else 
        { 
            instance = this; 
        } 
        _audioPlayer = GetComponent<AudioSource>();
    }

    public void ControlBackgroundMusic(Toggle musicToggle){
        m_backgroundPlayer.enabled = musicToggle.isOn;
        if(musicToggle.isOn)
            PlayerPrefs.SetInt(Application.identifier + Constants.MUSIC_FLAG, 1);
        else
            PlayerPrefs.SetInt(Application.identifier + Constants.MUSIC_FLAG, 0);
    }

    public void PlayPair(){
        _audioPlayer.PlayOneShot(m_pair, 1);
    }


    public void PlayWon(){
        _audioPlayer.PlayOneShot(m_won, 1);
    }

    public void PlayLol(){
        _audioPlayer.PlayOneShot(m_lol, 1);
    }

    public void PlayCardShuffle(){
        _audioPlayer.PlayOneShot(m_cardShuffle, 1);
    }

    public void PlayCardFlip(){
        _audioPlayer.PlayOneShot(m_cardFlip, 1);
    }
    public void PlayWrongMatch(){
        _audioPlayer.PlayOneShot(m_wrongMatch, 1);
    }

    public void PlayCorrectMatch(){
        _audioPlayer.PlayOneShot(m_correctMatch, 1);
    }
}