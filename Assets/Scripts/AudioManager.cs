using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioPlayer;
 
    [SerializeField] AudioClip pair, won, lol, cardShuffle, cardFlip;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    void Start()
    {
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
        //Instance = this;
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