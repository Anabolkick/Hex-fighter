// CardView.cs

using System.Collections.Generic;
using Cards;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 initialPosition;
    private Vector3 offset;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if the card is dropped over a valid target for playing
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        bool isValidDrop = true;
        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject.CompareTag("HandArea"))
            {
                isValidDrop = false;
                break;
            }
        }

        if (isValidDrop)
        {
            // Card is dropped over a valid target, play the card
            transform.position = eventData.pointerCurrentRaycast.worldPosition;
            // Get the parent CardsController component
            CardsController cardsController = GetComponentInParent<CardsController>();
            // Get the corresponding CardData
            CardData cardData = cardsController.GetCardDataByCardView(this);
            // Play the card
            cardsController.PlayCard(cardData);
            
            Destroy(this.gameObject); //TODO obj pool
        }
        else
        {
            // Card is dropped outside a valid target, return the card to the initial position
            transform.position = initialPosition;
        }
    }

}