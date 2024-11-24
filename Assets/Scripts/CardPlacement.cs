using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacement : MonoBehaviour
{
    Card activeCard;
    [SerializeField] bool isPlayerPoint = false;

    public Card ActiveCard { get { return activeCard; } set { activeCard = value; } }
    public bool IsPlayerPoint => isPlayerPoint;
}
