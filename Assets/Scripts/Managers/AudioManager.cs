using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioPlayer;
    [SerializeField] AudioSource m_backgroundPlayer;
 
    [SerializeField] AudioClip pair, won, lol, cardShuffle, cardFlip;

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
        audioPlayer = GetComponent<AudioSource>();
    }

    public void ControlBackgroundMusic(Toggle musicToggle){
        m_backgroundPlayer.enabled = musicToggle.isOn;
        if(musicToggle.isOn)
            PlayerPrefs.SetInt(Application.identifier + Constants.MUSIC_FLAG, 1);
        else
            PlayerPrefs.SetInt(Application.identifier + Constants.MUSIC_FLAG, 0);
    }

    public void PlayPair(){
        audioPlayer.PlayOneShot(pair, 1);
    }


    public void PlayWon(){
        audioPlayer.PlayOneShot(won, 1);
    }

    public void PlayLol(){
        audioPlayer.PlayOneShot(lol, 1);
    }

    public void PlayCardShuffle(){
        audioPlayer.PlayOneShot(cardShuffle, 1);
    }

    public void PlayCardFlip(){
        audioPlayer.PlayOneShot(cardFlip, 1);
    }
}