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

    public int PlayerMana => playerMana;

    public enum TurnOrder { playerActive, playerCardAttacks, enemyActive, enemyCardAttacks }
    public TurnOrder currentPhase;
    private void Awake()
    {
        Singelton();
    }

    void Start()
    {
        playerMana = startingMana;
        UIController.Instance.SetPlayerManaText(playerMana);

        DeckController.Instance.DrawMultipleCards(startCardAmount);
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

            switch (currentPhase)
            {
                case TurnOrder.playerActive:

                    Debug.Log("player Active");
                    break;
                case TurnOrder.playerCardAttacks:
                    Debug.Log("player Active Attacks");


                    break;
                case TurnOrder.enemyActive:
                    Debug.Log("enemy Active");


                    break;
                case TurnOrder.enemyCardAttacks:

                    Debug.Log("enemy Active Attacks");


                    break;

                default:

                    break;
            }
        }

    }
}
