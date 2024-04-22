using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private AudioClip m_flip = null;
    [SerializeField] private CardSO m_cardData = null;
    [SerializeField] private Sprite m_cardBackSprite = null;
    
    private bool m_isFlippedUp = false;
    private int m_ID;
    private Animator m_animator;
    private Image m_image;
    void Awake()
    {
        m_ID = GetInstanceID();
        m_animator = GetComponentInChildren<Animator>();
        m_image = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        m_cardData = CardManager.RandomCardData();
    }

    public void Flip()
    {
        if (m_isFlippedUp)
            FlipDown();
        else
            FlipUp();
        m_isFlippedUp = !m_isFlippedUp;
        //PlaySound.instance.Play(m_flip);
    }
    private void FlipUp()
    {
        //SwapImage();
        m_animator.SetTrigger(Constants.FLIP);
    }
    private void FlipDown()
    {
        //SwapImage();
        m_animator.SetTrigger(Constants.FLIP);
    }
    public void SwapImage()
    {
        m_image.overrideSprite = (!m_isFlippedUp) ? m_cardBackSprite : m_cardData.GetSprite();
    }
    public void PairWasMade()
    {
        gameObject.SetActive(false);
    }
    public int GetCardType()
    {
        return m_cardData.GetCardType();
    }

    public int GetCardID()
    {
        return m_ID;
    }
}
