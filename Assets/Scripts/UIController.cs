using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] TMP_Text playerManaText;

    private void Awake()
    {
        Singelton();
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
    public void SetPlayerManaText(int manaAmount)
    {
        playerManaText.text = "Mana: " + manaAmount;
    }
}
