using UnityEngine;

[CreateAssetMenu(fileName = "Card00", menuName = "ScriptableObjects/Card")]
public class CardSO : ScriptableObject
{
    [SerializeField] private Sprite m_sprite = null;
   
    public int GetCardType() { return GetInstanceID(); }
    public Sprite GetSprite() { return m_sprite; }
}
