using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] TMP_Text playerManaText;
    [SerializeField] GameObject manaWarningText;

    [SerializeField] float manaWarningTime;
    [SerializeField] GameObject drawCardButton;
    [SerializeField] GameObject endTurnButton;

    float manaWarningCounter;

    public GameObject DrawCardButton { get { return drawCardButton; } set { drawCardButton = value; } }
    public GameObject EndTurnButton { get { return endTurnButton; } set { endTurnButton = value; } }

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

    public void DrawCard()
    {
        DeckController.Instance.DrawCardForMana();
    }

    public void EndPlayerTurn()
    {
        
        BattleController.Instance.EndPlayerTurn();
    }

}
