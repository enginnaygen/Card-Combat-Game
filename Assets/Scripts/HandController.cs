using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] List<Card> heldCards = new List<Card>();

    [SerializeField] Transform minPos;
    [SerializeField] Transform maxPos;
    [SerializeField] List<Vector3> cardPositions = new List<Vector3>();
    void Start()
    {
        SetCardPositionsInHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCardPositionsInHand()
    {
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

            heldCards[i].transform.position = cardPositions[i];
        }
    }
}
