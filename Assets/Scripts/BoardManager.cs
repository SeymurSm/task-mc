using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public event Action OnGameOver = delegate {  }; 
    [SerializeField] private GameObject m_cardPrefab = null;
    [SerializeField] private GameObject m_cardBoard;
    [SerializeField] private uint m_numberOfPairs = 14;
    [SerializeField] private RectTransform m_victoryParticles = null;
    [SerializeField] private AudioClip m_pairSound = null;
    [SerializeField] private AudioClip m_victorySound = null;
    
    private LinkedList<int> possibleIndexes;
    private bool m_canClick = true;
    private bool m_isCardSelected = false;
    private Card m_selectedCard = null;
    private uint m_pairsRemaing = 0;
    
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
            TakeCardsFromBoard(clickedCard);
            //TODO: Play victory sound
            if (GameIsOver())
            {
                OnGameOver();
                PlayerPrefs.SetInt(Constants.NEW_SCORE_FLAG, 1);
                //TODO: Play victory sound
                m_victoryParticles.gameObject.SetActive(true);
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
