using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoardManager : MonoBehaviour
{
    public event Action OnGameOver = delegate {  }; 
    [SerializeField] private GameObject m_cardPrefab = null;
    [SerializeField] private GameObject m_cardBoard;
    [SerializeField] private GameObject m_resultPanel;
    [SerializeField] private TMP_Text m_resultText;
    [SerializeField] private TMP_Text m_turnCount;
    [SerializeField] private TMP_Text m_matchesCount;
    [SerializeField] private uint m_numberOfPairs = 14;

    [SerializeField] private AudioClip m_pairSound = null;
    [SerializeField] private AudioClip m_victorySound = null;
    
    private LinkedList<int> possibleIndexes;
    private bool m_canClick = true;
    private bool m_isCardSelected = false;
    private Card m_selectedCard = null;
    private uint m_pairsRemaing = 0;

    private byte turnCount = 0;
    private byte matchCount = 0;
    
    void Awake()
    {
        InitCards();
        m_pairsRemaing = m_numberOfPairs;
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
        uint numberOfCards = m_numberOfPairs * 2;
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
                    ++turnCount;
                    m_turnCount.text = "Turns\n" + turnCount;
                    clickedCard.Flip();
                    StartCoroutine(CheckForPairAndGameOver(clickedCard));
                }
            }
        }
    }

    private IEnumerator CheckForPairAndGameOver(Card clickedCard)
    {
        m_canClick = false;
        yield return new WaitForSecondsRealtime(1.1f);
        if (SelectedCardsArePair(clickedCard))
        {
            ++matchCount;
            m_matchesCount.text = "Matches\n" + matchCount;
            TakeCardsFromBoard(clickedCard);
            //TODO: Play victory sound
            AudioManager.instance.PlayPair();
            if (GameIsOver())
            {
                OnGameOver();
                PlayerPrefs.SetInt(Constants.NEW_SCORE_FLAG, 1);
                m_resultText.text = "Game Finished";
                m_resultPanel.SetActive(true);
                //TODO: Play victory sound
                AudioManager.instance.PlayWon();
               
            }
        }
        else
        {
            clickedCard.Flip();
        }
        DeselectCard();
        m_canClick = true;
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
