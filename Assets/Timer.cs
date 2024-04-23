using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public static event Action<int> OnTimeCalculated = delegate {  };
    
    [SerializeField] private BoardManager m_boardManager = null;
    
    [SerializeField] private TMP_Text m_text = null;
    private float m_time = 0f;
    private string m_seconds = "";
    private string m_minutes = "";
    private const string c_timeFormat = Constants.MINUTES_FORMAT;
    private const string c_timeDelimiter = Constants.DELIMITER;
    private const int c_seconds = Constants.SECONDS;
    
    void Awake()
    {
        CalculateTimeHardness();
        m_boardManager.OnGameOver += StopTimer;
    }

    void Update()
    {
        m_time -= Time.deltaTime;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if(m_time>=0){
            m_minutes = Mathf.Floor(m_time / c_seconds).ToString(c_timeFormat);
            m_seconds = (m_time % c_seconds).ToString(c_timeFormat);
            m_minutes = String.Concat(m_minutes, c_timeDelimiter, m_seconds);
            m_text.text = m_minutes.ToString();
        }else{
            //StopTimer();
            m_boardManager.LoseGame();
        }
    }

    private void CalculateTimeHardness(){
        int rows = PlayerPrefs.GetInt(Application.identifier + "rows");
        int columns = PlayerPrefs.GetInt(Application.identifier + "columns");
        int hardness = PlayerPrefs.GetInt(Application.identifier + "hardness");
        m_time = rows*columns * (6-hardness);
    }

    private void StopTimer()
    {
        UpdateTimerText();
        OnTimeCalculated(Mathf.RoundToInt(m_time));
        this.enabled = false;
    }

    private void OnDisable()
    {
        m_boardManager.OnGameOver -= StopTimer;
    }
}
