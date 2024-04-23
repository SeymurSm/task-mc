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

    int rows, columns;
    
    private void Awake()
    {
        PopulateCardList();
    }

    private void PopulateCardList()
    {
        rows = PlayerPrefs.GetInt(Application.identifier + "rows");
        columns = PlayerPrefs.GetInt(Application.identifier + "columns");

        m_cardsToBeUsed = new LinkedList<CardSO>();
        for (int i = 0; i<rows*columns/2; i++)
        {
            for (int j = 0; j < Constants.NUMBER_OF_REPEATED_CARDS; j++)
            {
                m_cardsToBeUsed.AddLast(m_possibleCards[i]);
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
