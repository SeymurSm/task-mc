using UnityEngine;

public class SwapImage : MonoBehaviour
{
    private Card m_card = null;
    private void Awake()
    {
        m_card = GetComponentInParent<Card>();
    }
    public void SwapCardImage()
    {
        m_card.SwapImage();
    }
}
