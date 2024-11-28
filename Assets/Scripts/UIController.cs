using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] TMP_Text playerManaText;
    [SerializeField] TMP_Text playerHealthText;
    [SerializeField] TMP_Text enemyHealthText;
    [SerializeField] GameObject manaWarningText;

    [SerializeField] float manaWarningTime;
    [SerializeField] GameObject drawCardButton;
    [SerializeField] GameObject endTurnButton;

    [SerializeField] UIDamageIndicator playerDamage;
    [SerializeField] UIDamageIndicator enemyDamage;

    float manaWarningCounter;

    public GameObject DrawCardButton { get { return drawCardButton; } set { drawCardButton = value; } }
    public GameObject EndTurnButton { get { return endTurnButton; } set { endTurnButton = value; } }

    public UIDamageIndicator PlayerDamage => playerDamage;
    public UIDamageIndicator EnemyDamage => enemyDamage;

    private void Awake()
    {
        Singelton();
    }

    private void Update()
    {
        if(manaWarningCounter > 0)
        {
            manaWarningCounter -= Time.deltaTime;

            if(manaWarningCounter <= 0)
            {
                manaWarningText.SetActive(false);
            }
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

    public void WarningMana()
    {
        manaWarningText.SetActive(true);

        manaWarningCounter = manaWarningTime;



    }
    public void SetPlayerManaText(int manaAmount)
    {
        playerManaText.text = "Mana: " + manaAmount;
    }

    public void SetPlayerHealthText(int healthAmount)
    {
        playerHealthText.text = "Player Health: " + healthAmount;
    }

    public void SetEnemyHealthText(int healthAmount)
    {
        enemyHealthText.text = "Enemy Health: " + healthAmount;
    }

    public void DrawCard()
    {
        DeckController.Instance.DrawCardForMana();
    }

    public void EndPlayerTurn()
    {
        
        BattleController.Instance.EndPlayerTurn();
    }

}
