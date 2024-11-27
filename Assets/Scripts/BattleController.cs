using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController Instance;

    [SerializeField] int maxMana = 12;
    [SerializeField] int startingMana = 4;
    [SerializeField] int playerMana;
    [SerializeField] int startCardAmount = 5;

    int currentPlayerMaxMana;

    public int PlayerMana => playerMana;

    public enum TurnOrder { playerActive, playerCardAttacks, enemyActive, enemyCardAttacks }
    TurnOrder currentPhase;

    public TurnOrder CurrentPhase => currentPhase;

    private void Awake()
    {
        Singelton();
    }

    void Start()
    {
       
        DeckController.Instance.DrawMultipleCards(startCardAmount);
        currentPlayerMaxMana = startingMana;
        FillPlayerMana();
    }

    private void FillPlayerMana()
    {
        playerMana = currentPlayerMaxMana;
        UIController.Instance.SetPlayerManaText(playerMana);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvancePhase();
        }

    }
    private void Singelton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SpendPlayerMana(int playerManaCost)
    {
        playerMana -= playerManaCost;

        if (playerMana < 0)
        {
            playerMana = 0;
        }

        UIController.Instance.SetPlayerManaText(playerMana);

    }

    public void AdvancePhase()
    {
        currentPhase++;

        if ((int)currentPhase >= System.Enum.GetValues(typeof(TurnOrder)).Length)
        {
            currentPhase = 0;
        }

            switch (currentPhase)
            {
                case TurnOrder.playerActive:

                UIController.Instance.EndTurnButton.SetActive(true);
                UIController.Instance.DrawCardButton.SetActive(true);

                if(currentPlayerMaxMana < maxMana)
                {
                    currentPlayerMaxMana++;
                }

                FillPlayerMana();

                    break;

                case TurnOrder.playerCardAttacks:

                AdvancePhase();

                    break;

                case TurnOrder.enemyActive:

                AdvancePhase();

                    break;

                case TurnOrder.enemyCardAttacks:

                AdvancePhase();


                break;

                default:

                    break;
            }
        }

    public void EndPlayerTurn()
    {
        UIController.Instance.EndTurnButton.SetActive(false);
        UIController.Instance.DrawCardButton.SetActive(false);
        AdvancePhase();
    }

    }

