using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)]
public class CardScriptableObject : ScriptableObject
{
    public string cardName;
    [TextArea] public string actionDescription;
    [TextArea] public string cardLore;

    public int currentHealth;
    public int attackPower;
    public int manaCost;

    public Sprite bgSprite;
    public Sprite characterSprite;
}
