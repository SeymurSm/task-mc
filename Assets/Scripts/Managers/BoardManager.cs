using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public event Action OnGameOver = delegate {  }; 
    [SerializeField] private GameObject m_cardPrefab = null;
    [SerializeField] private GameObject m_cardBoard;
    [SerializeField] private GameObject m_resultPanel;
    [SerializeField] private TMP_Text m_resultText;
    [SerializeField] private TMP_Text m_scoreText;
    [SerializeField] private TMP_Text m_turnCountText;
    [SerializeField] private TMP_Text m_matchesCountText;
    private LinkedList<int> possibleIndexes;
    private bool m_canClick = true;
    private bool m_isCardSelected = false;
    private Card m_selectedCard = null;
    private uint m_pairsRemaing = 0;

    private byte m_turnCount = 0;
    private byte m_matchCount = 0;

    int rows, columns;
    
    void Awake()
    {
        //Reading saved size paramaters and generating board
        rows = PlayerPrefs.GetInt(Application.identifier + Constants.ROWS_FLAG);
        columns = PlayerPrefs.GetInt(Application.identifier + Constants.COLUMNS_FLAG);

        InitCards();
        m_pairsRemaing = (uint)(rows*columns)/2;
        CardClickNotifier.OnCardClick += CardWasClicked;
        StartCoroutine(DisableGridLayout());
    }

    IEnumerator DisableGridLayout()
    {
        yield return new WaitForSecondsRealtime(.5f);
        m_cardBoard.GetComponent<GridLayoutGroup>().enabled = false;
    }
    private void InitCards()
    {
        uint numberOfCards = (uint)(rows * columns);
        m_cardBoard.GetComponent<GridLayoutGroup>().constraintCount = columns;
        
        for (uint i = 0; i < numberOfCards; i++)
            Instantiate(m_cardPrefab, m_cardBoard.transform);
    }

    private void CardWasClicked(Card clickedCard)
    {
        if(m_canClick)
        {
            if (!m_isCardSelected)
            {
                SelectCard(clickedCard);
            }
            else
            {
                if (!ClickedCardWasAlreadySelected(clickedCard))
                {
                    ++m_turnCount;
                    m_turnCountText.text = "Turns\n" + m_turnCount;
                    clickedCard.Flip();
                    StartCoroutine(CheckForPairAndGameOver(clickedCard));
                }
            }
        }
    }

    private IEnumerator CheckForPairAndGameOver(Card clickedCard)
    {
        m_canClick = false;
        yield return new WaitForSecondsRealtime(0.5f);
        if (SelectedCardsArePair(clickedCard))
        {
            ++m_matchCount;
            m_matchesCountText.text = "Matches\n" + m_matchCount;
            TakeCardsFromBoard(clickedCard);
            //TODO: Play victory sound
            AudioManager.instance.PlayPair();
            AudioManager.instance.PlayCorrectMatch();
            if (GameIsOver())
            {
                OnGameOver();
                m_resultText.text = "You Won!";
                CalculateScore();
                m_resultPanel.SetActive(true);
                //TODO: Play victory sound
                AudioManager.instance.PlayWon();
               
            }
        }
        else
        {
            clickedCard.Flip();
            AudioManager.instance.PlayWrongMatch();
        }
        DeselectCard();
        m_canClick = true;
    }

    private void CalculateScore(){
        int score = (m_matchCount  + m_turnCount)*100;
        m_scoreText.text = "Score: "+(score);

        if(score>PlayerPrefs.GetInt(Application.identifier + Constants.HIGHSCORE_FLAG, 0)){
           PlayerPrefs.SetInt(Application.identifier + Constants.HIGHSCORE_FLAG, score);
        }
    }

    public void LoseGame(){
        m_resultText.text = "You Lose!";
        m_resultPanel.SetActive(true);
    }
    
    private void TakeCardsFromBoard(Card cardToTake)
    {
        cardToTake.PairWasMade();
        m_selectedCard.PairWasMade();
    }

    private bool GameIsOver()
    {
        return --m_pairsRemaing == 0;
    }
    private bool SelectedCardsArePair(in Card secondCardSelected)
    {
        return secondCardSelected.GetCardType() == m_selectedCard.GetCardType();
    }
    private bool ClickedCardWasAlreadySelected(in Card secondCardSelected)
    {
        return secondCardSelected.GetCardID() == m_selectedCard.GetCardID();
    }
    private void SelectCard(Card clickedCard)
    {
        m_selectedCard = clickedCard;
        m_selectedCard.Flip();
        m_isCardSelected = true;
    }
    private void DeselectCard()
    {
        m_selectedCard.Flip();
        m_selectedCard = null;
        m_isCardSelected = false;
    }
    
    private void OnDisable()
    {
        CardClickNotifier.OnCardClick -= CardWasClicked;
    }
}
