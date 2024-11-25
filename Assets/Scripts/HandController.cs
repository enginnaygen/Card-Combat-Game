using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] List<Card> heldCards = new List<Card>();

    [SerializeField] Transform minPos;
    [SerializeField] Transform maxPos;
    [SerializeField] List<Vector3> cardPositions = new List<Vector3>();

    public List<Vector3> CardPositions => cardPositions;
    void Start()
    {
        SetCardPositionsInHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCardPositionsInHand() //kartlar su sekilde yerlesiyor, sondaki kart en sona bastaki kart en basa yerlesiyor ardaki noktalar hesaplaniyor
    {                                    //sirasiyla en bastan en sona dogru kartla yerlesiyor
        cardPositions.Clear();

        Vector3 distanceBetweenPoints = Vector3.zero;

        if(heldCards.Count > 1)
        {
            distanceBetweenPoints = (maxPos.position - minPos.position) / (heldCards.Count - 1);
        }

        for (int i = 0; i < heldCards.Count; i++)
        {
            Vector3 position = minPos.position + (distanceBetweenPoints * i);
            cardPositions.Add(position);

            //heldCards[i].transform.position = cardPositions[i];
            //heldCards[i].transform.rotation = minPos.rotation; //we set z rotation value of minPos -5

            heldCards[i].MoveToPoint(cardPositions[i], minPos.rotation);

            heldCards[i].inHand = true;
            heldCards[i].handPosition = i;
        }
    }

    public void RemoveCardFromHand(Card cardToRemove)
    {
        if (heldCards[cardToRemove.handPosition] == cardToRemove)
        {
            //heldCards.Remove(cardToRemove);
            heldCards.RemoveAt(cardToRemove.handPosition);
        }
        else
        {
            Debug.LogError("Card at position" + cardToRemove.handPosition + "is not the card being removed");
        }
        SetCardPositionsInHand();


    }
}
