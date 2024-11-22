using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int attackPower;
    [SerializeField] int manaCost;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text attackPowerText;
    [SerializeField] TMP_Text costText;


    void Start()
    {
        healthText.text = health.ToString();
        attackPowerText.text = attackPower.ToString();
        costText.text = manaCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
