// CardsController.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cards;
using TMPro;

public class CardsController : MonoBehaviour
{
    public GameObject cardViewPrefab;
    public Transform handTransform;
    public TMP_Text deckCountText;
    public TMP_Text playedCardsCountText;

    public CardConfig cardConfig;

    private List<CardBehaviour> deck;
    private List<CardData> hand;
    private List<CardBehaviour> playedCards;

    private Dictionary<CardView, CardData> cardViewToDataMap;

    private void Start()
    {
        deck = new List<CardBehaviour>(cardConfig.GetCards());
        ShuffleDeck();
        hand = new List<CardData>();
        playedCards = new List<CardBehaviour>();
        cardViewToDataMap = new Dictionary<CardView, CardData>();

        DrawInitialHand();
        UpdateDeckCount();
        UpdatePlayedCardsCount();
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            CardBehaviour temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    private void DrawInitialHand()
    {
        int initialHandSize = 5;

        for (int i = 0; i < initialHandSize; i++)
        {
            DrawCard();
        }
    }

    private void DrawCard()
    {
        if (deck.Count == 0)
        {
            // Reshuffle played cards into the deck if it's empty
            deck.AddRange(playedCards);
            playedCards.Clear();
            ShuffleDeck();
            UpdateDeckCount();
        }

        CardBehaviour cardBehaviour = deck[0];
        deck.RemoveAt(0);

        GameObject cardViewObject = Instantiate(cardViewPrefab, handTransform);
        CardView cardView = cardViewObject.GetComponent<CardView>();

        CardData cardData = new CardData();
        cardData.cardBehaviour = cardBehaviour;
        cardData.cardView = cardView;

        hand.Add(cardData);
        cardViewToDataMap.Add(cardView, cardData);

        // Set up the card's visual representation using cardView

        UpdateHandDisplay();
    }

    private void UpdateHandDisplay()
    {
        // Arrange cards in the hand with a slight angle
        float angleIncrement = -8f;
        float totalAngle = (hand.Count - 1) * angleIncrement;
        float startX = -totalAngle / 2f;
        float distance = 100f;

        float yPos = 5f; 

        for (int i = 0; i < hand.Count; i++)
        {
            CardData cardData = hand[i];
            float xPos = startX + i * distance;
            Vector3 position = handTransform.position + new Vector3(xPos, yPos, 0f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, startX + i * angleIncrement);
            cardData.cardView.transform.position = position;
            cardData.cardView.transform.rotation = rotation;
        }
    }


    public void PlayCard(CardData cardData)
    {
        // Execute the card's action
        cardData.cardBehaviour.ExecuteAction();

        // Remove the card from the hand and add it to played cards
        hand.Remove(cardData);
        playedCards.Add(cardData.cardBehaviour);

        // Update UI and display
        UpdateHandDisplay();
        UpdatePlayedCardsCount();
    }

    public CardData GetCardDataByCardView(CardView cardView)
    {
        if (cardViewToDataMap.ContainsKey(cardView))
        {
            return cardViewToDataMap[cardView];
        }
        return null;
    }

    private void UpdateDeckCount()
    {
        deckCountText.text = deck.Count.ToString();
    }

    private void UpdatePlayedCardsCount()
    {
        playedCardsCountText.text = playedCards.Count.ToString();
    }
}
