using UnityEngine;

[CreateAssetMenu(menuName = "CardBehaviour")]
public class CardBehaviour : ScriptableObject
{
    public string cardName;
    public int manaCost;

    // Add other properties and methods for card behavior

    public void ExecuteAction()
    {
        // Add code for executing the card's action
    }
}