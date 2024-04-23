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
    [SerializeField] Toggle m_musicToggle;

    void Start(){
        int hardness = PlayerPrefs.GetInt(Application.identifier + Constants.HARDNESS_FLAG, 0);
        m_hardnessToggles[hardness].SetIsOnWithoutNotify(true);

        int mode = PlayerPrefs.GetInt(Application.identifier + Constants.MODE_FLAG, 0);
        m_modeToggles[mode].SetIsOnWithoutNotify(true);

        int highS = PlayerPrefs.GetInt(Application.identifier + Constants.HIGHSCORE_FLAG, 0);
        m_highscoreText.text = "Highscore:"+highS;

        int music = PlayerPrefs.GetInt(Application.identifier + Constants.MUSIC_FLAG, 1);
        if(music == 0){
            m_musicToggle.isOn = false;
        }
    }
    public void PlayGame(){
        SceneController.instance.PlayGame();
    }

    public void MusicControl(Toggle musicToggle){
        AudioManager.instance.ControlBackgroundMusic(musicToggle);
    }  

    public void SetGameHardness(int hardness){
        PlayerPrefs.SetInt(Application.identifier + Constants.HARDNESS_FLAG, hardness);
    }
    public void SetGameMode(int mode){
        PlayerPrefs.SetInt(Application.identifier + Constants.MODE_FLAG, mode);
    }
    public void SetGameModeRows(int rows){
        PlayerPrefs.SetInt(Application.identifier + Constants.ROWS_FLAG, rows);
    }    
    public void SetGameModeColumns(int columns){
        PlayerPrefs.SetInt(Application.identifier + Constants.COLUMNS_FLAG, columns);
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
        PlayerPrefs.SetInt(Application.identifier + Constants.COLUMNS_FLAG, columns);
        PlayerPrefs.SetInt(Application.identifier + Constants.ROWS_FLAG, rows);
    }   
}
