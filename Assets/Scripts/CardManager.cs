using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] private CardSO[] m_possibleCards;
    
    private static LinkedList<CardSO> m_cardsToBeUsed;
    private void Awake()
    {
        PopulateCardList();
    }

    private void PopulateCardList()
    {
        m_cardsToBeUsed = new LinkedList<CardSO>();
        foreach (var possibleCard in m_possibleCards)
        {
            for (int i = 0; i < Constants.NUMBER_OF_REPEATED_CARDS; i++)
            {
                m_cardsToBeUsed.AddLast(possibleCard);
            }
        }
    }
    public static CardSO RandomCardData()
    {
        int index = Random.Range(0, m_cardsToBeUsed.Count);
        CardSO card = m_cardsToBeUsed.ElementAt(index);
        m_cardsToBeUsed.Remove(card);
        return card;
    }
}
