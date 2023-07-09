using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "CardConfig")]
public class CardConfig : ScriptableObject
{
    public List<CardBehaviour> cards;

    public List<CardBehaviour> GetCards()
    {
        return new List<CardBehaviour>(cards);
    }
}