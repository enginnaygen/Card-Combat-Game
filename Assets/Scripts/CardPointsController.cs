using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CardPointsController : MonoBehaviour
{
    public static CardPointsController Instance;

    public CardPlacement[] playerCardPoints;
    public CardPlacement[] enemyCardPoints;

    [SerializeField] float timeBetweenAttacks = .25f;

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

    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttackCO());
    }

    IEnumerator PlayerAttackCO()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);

        for (int i = 0; i < playerCardPoints.Length; i++)
        {
            if (playerCardPoints[i].ActiveCard != null )
            {
                if(enemyCardPoints[i].ActiveCard != null)
                {
                    enemyCardPoints[i].ActiveCard.DamageCard(playerCardPoints[i].ActiveCard.AttackPower);
                    playerCardPoints[i].ActiveCard.Animator.SetTrigger("Attack");
                }
                else
                {
                    //attack the enemies overall health
                }

                yield return new WaitForSeconds(timeBetweenAttacks);

            }
        }

        CheckAssignedCards();

        BattleController.Instance.AdvancePhase();
    }

    public void CheckAssignedCards() //extra check for safe
    {
        foreach (CardPlacement point in enemyCardPoints)
        {
            if(point.ActiveCard != null && point.ActiveCard.CurrentHealth <=0)
            {
                point.ActiveCard = null;
            }
        }

        foreach (CardPlacement point in playerCardPoints)
        {
            if (point.ActiveCard != null && point.ActiveCard.CurrentHealth <= 0)
            {
                point.ActiveCard = null;
            }
        }
    }
}
