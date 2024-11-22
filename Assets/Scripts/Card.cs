using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] CardScriptableObject cardSO;

    [SerializeField] int currentHealth;
    [SerializeField] int attackPower;
    [SerializeField] int manaCost;

    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text attackPowerText;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text actionDescriptionText;
    [SerializeField] TMP_Text loreText;

    [SerializeField] Image characterArt;
    [SerializeField] Image backgroundArt;


    void Start()
    {
        SetupCard();
    }

    private void SetupCard()
    {
        currentHealth = cardSO.currentHealth;
        attackPower = cardSO.attackPower;
        manaCost = cardSO.manaCost;

        nameText.text = cardSO.cardName;
        actionDescriptionText.text = cardSO.actionDescription;
        loreText.text = cardSO.cardLore;

        healthText.text = currentHealth.ToString();
        attackPowerText.text = attackPower.ToString();
        costText.text = manaCost.ToString();

        characterArt.sprite = cardSO.characterSprite;
        backgroundArt.sprite = cardSO.bgSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
