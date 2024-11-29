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

    private void Awake()
    {
        Singleton();
    }
    void Start()
    {
        SetupDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
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

        while(selectedPlacement.ActiveCard != null && enemyCardPlacement.Count > 0)
        {
            randomSelectedPlacement = Random.Range(0, enemyCardPlacement.Count);
            selectedPlacement = enemyCardPlacement[randomSelectedPlacement];
            enemyCardPlacement.RemoveAt(randomSelectedPlacement);

        }

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
}
