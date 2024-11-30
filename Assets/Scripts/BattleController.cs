using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController Instance;

    [SerializeField] int maxMana = 12;
    [SerializeField] int playerStartingMana = 4;
    [SerializeField] int enemyStartingMana = 4;
    [SerializeField] int playerMana;
    [SerializeField] int enemyMana;
    [SerializeField] int startCardAmount = 5;
    [SerializeField] int cardsToDrawByTurn = 1;
    [SerializeField] int playerHealth;
    [SerializeField] int enemyHealth;
    [SerializeField] Transform discardPoint;

    int currentPlayerMaxMana;
    int currentEnemyMaxMana;

    public int PlayerMana => playerMana;
    public int PlayerHealth => playerHealth;
    public int EnemyMana => enemyMana;
    public int CardsToDrawBuTurn => cardsToDrawByTurn;
    public Transform DiscardPoint => discardPoint;

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
        currentPlayerMaxMana = playerStartingMana;
        currentEnemyMaxMana = enemyStartingMana;
        FillPlayerMana();
        FillEnemyMana();
        UIController.Instance.SetPlayerHealthText(playerHealth);
        UIController.Instance.SetEnemyHealthText(enemyHealth);

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

    private void FillPlayerMana()
    {
        playerMana = currentPlayerMaxMana;
        UIController.Instance.SetPlayerManaText(playerMana);
    }

    void FillEnemyMana()
    {
        enemyMana = currentEnemyMaxMana;
        UIController.Instance.SetEnemyManaText(enemyMana);
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

    public void SpendEnemyMana(int enemyManaCost)
    {
        enemyMana -= enemyManaCost;

        if(enemyMana <0)
        {
            enemyMana = 0;
        }

        UIController.Instance.SetEnemyManaText(enemyMana);
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

                DeckController.Instance.DrawMultipleCards(cardsToDrawByTurn);

                    break;

                case TurnOrder.playerCardAttacks:

                CardPointsController.Instance.PlayerAttack();
                //AdvancePhase();

                    break;

                case TurnOrder.enemyActive:

                EnemyController.Instance.StartEnemyAction();

                if(currentEnemyMaxMana < maxMana)
                {
                    currentEnemyMaxMana++;
                }
                FillEnemyMana();

                    break;

                case TurnOrder.enemyCardAttacks:

                CardPointsController.Instance.EnemyAttack();


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

    public void DamagePlayer(int damageAmount)
    {
        if(playerHealth > 0)
        {
            playerHealth -= damageAmount;

            if(playerHealth<=0)
            {
                playerHealth = 0;

                //end battle

            }

            UIController.Instance.SetPlayerHealthText(playerHealth);

            UIDamageIndicator damageClone = Instantiate(UIController.Instance.PlayerDamage, UIController.Instance.PlayerDamage.transform.parent);
            damageClone.DamageIndicator.text = damageAmount.ToString();
            damageClone.gameObject.SetActive(true);

        }
    }

    public void DamageEnemy(int damageAmount)
    {
        if (enemyHealth > 0)
        {
            enemyHealth -= damageAmount;

            if (enemyHealth <= 0)
            {
                enemyHealth = 0;

                //end battle

            }

            UIController.Instance.SetEnemyHealthText(enemyHealth);

            UIDamageIndicator damageClone = Instantiate(UIController.Instance.EnemyDamage, UIController.Instance.EnemyDamage.transform.parent);
            damageClone.DamageIndicator.text = damageAmount.ToString();
            damageClone.gameObject.SetActive(true);
        }
    }

}

