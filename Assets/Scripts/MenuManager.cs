using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioSource m_musicPlayer;
    public void PlayGame(){
        SceneController.instance.PlayGame();
    }

    public void MusicControl(Toggle musicToggle){
        m_musicPlayer.enabled = musicToggle.isOn;
    }   
}
