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

    public void SetGameModeRows(int rows){
        PlayerPrefs.SetInt(Application.identifier + "rows", rows);
    }    
    public void SetGameModeColumns(int columns){
        PlayerPrefs.SetInt(Application.identifier + "columns", columns);
    }    
}
