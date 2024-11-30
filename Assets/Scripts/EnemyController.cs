using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    [SerializeField] float delay = .5f;
    [SerializeField] List<CardScriptableObject> deckToUse = new List<CardScriptableObject>();
    List<CardScriptableObject> activeCards = new List<CardScriptableObject>();

    [SerializeField] Card cardToSpawn;
    [SerializeField] Transform cardToSpawnPoint;

    public enum AIType { placeFromDeck, handRandomPlace, handDefensive, handOffensive}

    [SerializeField] AIType enemyAIType;

    List<CardScriptableObject> cardsInHand = new List<CardScriptableObject>();

    [SerializeField] int startHandSize;

    private void Awake()
    {
        Singleton();
    }
    void Start()
    {
        SetupDeck();

        if(enemyAIType != AIType.placeFromDeck)
        {
            SetupHand();
        }
    }


    void Singleton()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void SetupDeck()
    {
        activeCards.Clear();

        List<CardScriptableObject> tempDeck = new List<CardScriptableObject>();

        tempDeck.AddRange(deckToUse); //1)deckToUse kartlarin hepsinin oldugu yerdir, gecici degiskene oldugu gibi deckToUse'u veriyoruz

        int iterations = 0; // sadece guvenlik amacli sonsuz bir donguye olur da girerse diye

        while (tempDeck.Count > 0 && iterations < 500)
        {
            int selectedCard = Random.Range(0, tempDeck.Count);


            activeCards.Add(tempDeck[selectedCard]); //2)gecici degiskenden de activeCards listesine elemanlari rastgele sirada verip gecici degiskenden kartlari
                                                     //cikariyoruz
            tempDeck.RemoveAt(selectedCard);

            iterations++;
        }

    }

    public void StartEnemyAction()
    {
        StartCoroutine(EnemyActionCO());
    }

    IEnumerator EnemyActionCO()
    {
        if(activeCards.Count <= 0)
        {
            SetupDeck();
        }

        yield return new WaitForSeconds(delay);


        List<CardPlacement> enemyCardPlacement = new List<CardPlacement>();
        enemyCardPlacement.AddRange(CardPointsController.Instance.enemyCardPoints);

        int randomSelectedPlacement = Random.Range(0, enemyCardPlacement.Count);

        CardPlacement selectedPlacement = enemyCardPlacement[randomSelectedPlacement];

        if(enemyAIType == AIType.placeFromDeck || enemyAIType == AIType.handRandomPlace)
        {
            enemyCardPlacement.Remove(selectedPlacement);

            while (selectedPlacement.ActiveCard != null && enemyCardPlacement.Count > 0)
            {
                randomSelectedPlacement = Random.Range(0, enemyCardPlacement.Count);
                selectedPlacement = enemyCardPlacement[randomSelectedPlacement];
                enemyCardPlacement.RemoveAt(randomSelectedPlacement);

            }
        }


        CardScriptableObject selectedCard = null;

        switch (enemyAIType)
        {
            case AIType.placeFromDeck:

                if (selectedPlacement.ActiveCard == null)
                {
                    Card newCard = Instantiate(cardToSpawn, cardToSpawnPoint.position, cardToSpawnPoint.rotation);
                    newCard.CardSO = activeCards[0];
                    activeCards.RemoveAt(0);
                    newCard.SetupCard();
                    newCard.MoveToPoint(selectedPlacement.transform.position + new Vector3(0f, 0f, .38f), Quaternion.identity);

                    selectedPlacement.ActiveCard = newCard;
                    newCard.CardAssingedPlace = selectedPlacement;
                } 

                break;
            case AIType.handRandomPlace:

                selectedCard = SelectedCardToPlay();

                int iterations = 0;
                while(selectedCard != null && selectedPlacement.ActiveCard == null && iterations < 50)
                {
                    PlayCard(selectedCard, selectedPlacement);

                    selectedCard = SelectedCardToPlay();

                    iterations++;
                    yield return new WaitForSeconds(CardPointsController.Instance.TimeBetWeenAttacks);

                    while (selectedPlacement.ActiveCard != null && enemyCardPlacement.Count > 0)
                    {
                        randomSelectedPlacement = Random.Range(0, enemyCardPlacement.Count);
                        selectedPlacement = enemyCardPlacement[randomSelectedPlacement];
                        enemyCardPlacement.RemoveAt(randomSelectedPlacement);

                    }


                }



                break;
            case AIType.handDefensive:


                break;
            case AIType.handOffensive:


                break;
            default:


                break;
        }

       

        yield return new WaitForSeconds(delay);

        BattleController.Instance.AdvancePhase();

    }


    void SetupHand()
    {
        
        for (int i = 0; i < startHandSize; i++)
        {
            if (activeCards.Count <= 0)
            {
                SetupDeck();
            }

            cardsInHand.Add(activeCards[0]);
            activeCards.RemoveAt(0);
        }
    }

    public void PlayCard(CardScriptableObject cardSO, CardPlacement cardPlacePoint)
    {
        Card newCard = Instantiate(cardToSpawn, cardToSpawnPoint.position, cardToSpawnPoint.rotation);
        newCard.CardSO = cardSO;

        newCard.SetupCard();
        newCard.MoveToPoint(cardPlacePoint.transform.position + new Vector3(0f, 0f, .38f), Quaternion.identity);

        cardPlacePoint.ActiveCard = newCard;
        newCard.CardAssingedPlace = cardPlacePoint;
        cardsInHand.Remove(cardSO);

        BattleController.Instance.SpendEnemyMana(cardSO.manaCost);
    }

    CardScriptableObject SelectedCardToPlay()
    {
        CardScriptableObject cardToPlay = null;

        List<CardScriptableObject> cardsToPlay = new List<CardScriptableObject>();

        foreach(CardScriptableObject card in cardsInHand)
        {
            if(card.manaCost <= BattleController.Instance.EnemyMana)
            {
                cardsToPlay.Add(card);
            }
        }

        if(cardsToPlay.Count > 0)
        {
            cardToPlay = cardsToPlay[Random.Range(0, cardsToPlay.Count)];
        }

        return cardToPlay;
    }
}
