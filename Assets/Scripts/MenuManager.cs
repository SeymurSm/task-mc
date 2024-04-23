using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioSource m_musicPlayer;
    [SerializeField] TMP_Text m_highscoreText;
    [SerializeField] Toggle[] m_hardnessToggles;
    [SerializeField] Toggle[] m_modeToggles;

    void Start(){
        int hardness = PlayerPrefs.GetInt(Application.identifier + "hardness", 0);
        m_hardnessToggles[hardness].SetIsOnWithoutNotify(true);

        int mode = PlayerPrefs.GetInt(Application.identifier + "mode", 0);
        m_modeToggles[mode].SetIsOnWithoutNotify(true);

        int highS = PlayerPrefs.GetInt(Application.identifier + "highscore", 0);
        m_highscoreText.text = "Highscore:"+highS;
    }
    public void PlayGame(){
        SceneController.instance.PlayGame();
    }

    public void MusicControl(Toggle musicToggle){
        m_musicPlayer.enabled = musicToggle.isOn;
    }  

    public void SetGameHardness(int hardness){
        PlayerPrefs.SetInt(Application.identifier + "hardness", hardness);
    }
    public void SetGameMode(int mode){
        PlayerPrefs.SetInt(Application.identifier + "mode", mode);
    }
    public void SetGameModeRows(int rows){
        PlayerPrefs.SetInt(Application.identifier + "rows", rows);
    }    
    public void SetGameModeColumns(int columns){
        PlayerPrefs.SetInt(Application.identifier + "columns", columns);
    }    

    public void SetGameModeRandom(){
        int rows = Random.Range(2, 6);
        int columns = Random.Range(2, 6);
        if((rows*columns)%2 == 1){
            if((rows*(columns))>=30)
                columns--;
            else if((rows*(columns+1))<=30){
                columns++;
            }
        }
        PlayerPrefs.SetInt(Application.identifier + "columns", columns);
        PlayerPrefs.SetInt(Application.identifier + "rows", rows);
    }   
}
