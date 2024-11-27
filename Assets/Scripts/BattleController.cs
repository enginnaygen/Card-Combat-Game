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

        if(playerMana < 0)
        {
            playerMana = 0;
        }

        UIController.Instance.SetPlayerManaText(playerMana);

    }

}
