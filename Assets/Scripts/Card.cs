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

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 540f;

    Vector3 targetPos;
    Quaternion targetRotation;



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

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion pointToRollTo)
    {
        targetPos = pointToMoveTo;
        targetRotation = pointToRollTo;

    }
}
