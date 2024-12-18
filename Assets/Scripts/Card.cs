﻿using System.Collections;
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

    [SerializeField] LayerMask whatsDestkop, whatIsPlacement;

    [SerializeField] bool isPlayer;

    [SerializeField] Animator animator;

    public bool inHand;
    public int handPosition;

    Vector3 targetPos;
    Quaternion targetRotation;
    HandController handController;
    Collider collider;

    bool isSelected;
    bool justPressed;

    [SerializeField] CardPlacement cardAssingedPlace;
    public CardScriptableObject CardSO { get { return cardSO; } set { cardSO = value; } }
    public Animator Animator { get { return animator; } set { animator = value; } }
    public int AttackPower => attackPower;
    public int CurrentHealth => currentHealth;
    public int ManaCost => manaCost;
    public CardPlacement CardAssingedPlace { get { return cardAssingedPlace; } set { cardAssingedPlace = value; } }



    //Vector3 startPos;
    //float progress;

    private void Awake()
    {
        handController = FindObjectOfType<HandController>();
        collider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        if(targetPos == Vector3.zero)
        {
            targetPos = transform.position;
            targetRotation = transform.rotation;
        }
        //startPos = transform.position;
        SetupCard();

    }

    public void SetupCard()
    {
        currentHealth = cardSO.currentHealth;
        attackPower = cardSO.attackPower;
        manaCost = cardSO.manaCost;

        nameText.text = cardSO.cardName;
        actionDescriptionText.text = cardSO.actionDescription;
        loreText.text = cardSO.cardLore;

        UpdateCardDisplay();

        characterArt.sprite = cardSO.characterSprite;
        backgroundArt.sprite = cardSO.bgSprite;
    }

    void Update()
    {
        
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime); //transform.position'u surekli ileri gittiğinden surekli sona
                                                                                                      // dogru yaklasiyor seklinde anladim
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        if(isSelected && !BattleController.Instance.BattleEnded && Time.timeScale != 0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100f, whatsDestkop))
            {
                MoveToPoint(hit.point + Vector3.up, Quaternion.identity);

            }

            if (Input.GetMouseButtonDown(1) && !BattleController.Instance.BattleEnded)
            {
                ReturnToHand();
            }

            if (Input.GetMouseButtonDown(0) && !justPressed && !BattleController.Instance.BattleEnded)
            {

                if (Physics.Raycast(ray, out hit, 100f, whatIsPlacement) && BattleController.Instance.CurrentPhase == BattleController.TurnOrder.playerActive)
                {
                    CardPlacement selectedPoint = hit.collider.GetComponent<CardPlacement>();

                    if(selectedPoint.ActiveCard == null && selectedPoint.IsPlayerPoint)
                    {
                        if (BattleController.Instance.PlayerMana >= manaCost)
                        {
                            selectedPoint.ActiveCard = this;
                            cardAssingedPlace = selectedPoint;

                            MoveToPoint(selectedPoint.transform.position + new Vector3(0f, 0f, .38f), Quaternion.identity);

                            inHand = false;
                            isSelected = false;

                            handController.RemoveCardFromHand(this);

                            BattleController.Instance.SpendPlayerMana(manaCost);

                            AudioManager.Instance.PlayeSFX(4);

                        }
                        else
                        {
                            UIController.Instance.WarningMana();
                            ReturnToHand();
                        }
                    }
                    else
                    {
                        ReturnToHand();
                    }
                }
                else
                {
                    ReturnToHand();
                }
            }

            justPressed = false;

        }

       

        /*transform.position = Vector3.Lerp(transform.position, targetPos, progress);

        if (progress > 1) return;
        progress += Time.deltaTime;*/
    }

    private void ReturnToHand()
    {
        isSelected = false;
        collider.enabled = true;
        MoveToPoint(handController.CardPositions[handPosition], targetRotation);
    }

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion pointToRollTo)
    {
        targetPos = pointToMoveTo;
        targetRotation = pointToRollTo;

    }

    public void DamageCard(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <=0)
        {
            currentHealth = 0;
            cardAssingedPlace.ActiveCard = null;
            MoveToPoint(BattleController.Instance.DiscardPoint.position, BattleController.Instance.DiscardPoint.rotation);
            animator.SetTrigger("Jump");
            Destroy(gameObject, 3f);
            AudioManager.Instance.PlayeSFX(2);

        }
        else
        {
            AudioManager.Instance.PlayeSFX(1);

        }

        animator.SetTrigger("Hurt");
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        healthText.text = currentHealth.ToString();
        attackPowerText.text = attackPower.ToString();
        costText.text = manaCost.ToString();
    }

    private void OnMouseOver()
    {
        if(inHand && isPlayer && !BattleController.Instance.BattleEnded)
        {
            MoveToPoint(handController.CardPositions[handPosition] + new Vector3(0f, 1f, .5f), Quaternion.identity);
            /*Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            transform.position = Vector3.Lerp(startPos, startPos + Vector3.up, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(startRot, Quaternion.identity, rotateSpeed * Time.deltaTime);*/
        }
        
    }

    private void OnMouseExit()
    {
        if (inHand && isPlayer && !BattleController.Instance.BattleEnded)
        {
            MoveToPoint(handController.CardPositions[handPosition], targetRotation);
        }
    }

    private void OnMouseDown()
    {
        if(inHand && BattleController.Instance.CurrentPhase == BattleController.TurnOrder.playerActive && isPlayer && !BattleController.Instance.BattleEnded && Time.timeScale != 0f)
        {
            isSelected = true;
            collider.enabled = false;

            justPressed = true;
        }
    }
}
