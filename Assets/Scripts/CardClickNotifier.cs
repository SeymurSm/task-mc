using System;
using UnityEngine;

public class CardClickNotifier : MonoBehaviour
{
    public static event Action<Card> OnCardClick = delegate {  };
    
    private Card m_card = null;
    
    private void Awake()
    {
        m_card = GetComponent<Card>();
    }

    public void CardClicked()
    {
        OnCardClick(m_card);
    }
}