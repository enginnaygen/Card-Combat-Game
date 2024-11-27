using System.Collections;
using System.Collections.Generic;
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
                    //attack the enemy
                }
                else
                {
                    //attack the enemies overall health
                }

                yield return new WaitForSeconds(timeBetweenAttacks);

            }
        }

        BattleController.Instance.AdvancePhase();
    }
}
