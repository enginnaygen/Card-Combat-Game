using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController Instance;

    [SerializeField] List<CardScriptableObject> deckToUse = new List<CardScriptableObject>();
    List<CardScriptableObject> activeCards = new List<CardScriptableObject>();

    public List<CardScriptableObject> DeckToUse => deckToUse;

    [SerializeField] Card cardToSpawn;

    [SerializeField] int drawCardCost = 2;

    [SerializeField] float waitBetweenFirstDrawCards = .25f;


    private void Awake()
    {
        Singelton();
    }

    private void Start()
    {
        SetupDeck();
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            DrawCardToHand();
        }*/
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

    void SetupDeck()
    {
        activeCards.Clear();

        List<CardScriptableObject> tempDeck = new List<CardScriptableObject>();

        tempDeck.AddRange(deckToUse); //1)deckToUse kartlarin hepsinin oldugu yerdir, gecici degiskene oldugu gibi deckToUse'u veriyoruz

        int iterations = 0; // sadece guvenlik amacli sonsuz bir donguye olur da girerse diye

        while(tempDeck.Count > 0 && iterations < 500)
        {
            int selectedCard = Random.Range(0, tempDeck.Count);

                                                      
            activeCards.Add(tempDeck[selectedCard]); //2)gecici degiskenden de activeCards listesine elemanlari rastgele sirada verip gecici degiskenden kartlari
                                                     //cikariyoruz
            tempDeck.RemoveAt(selectedCard);

            iterations++;
        }

    }

    public void DrawCardToHand()
    {
        if(activeCards.Count <= 0)
        {
            SetupDeck();
        }

        Card newCard = Instantiate(cardToSpawn, transform.position, transform.rotation);

        newCard.CardSO = activeCards[0];
        newCard.SetupCard();

        activeCards.RemoveAt(0);

        HandController.Instance.AddCardToHand(newCard);

        
    }

    public void DrawCardForMana()
    {
        if(BattleController.Instance.PlayerMana >= drawCardCost)
        {
            DrawCardToHand();
            BattleController.Instance.SpendPlayerMana(drawCardCost);
        }
        else
        {
            UIController.Instance.WarningMana();
            UIController.Instance.DrawCardButton.SetActive(false);
        }
    }

    public void DrawMultipleCards(int cardAmount)
    {
        StartCoroutine(DrawMultipleCardsCoroutine(cardAmount));
    }

    IEnumerator DrawMultipleCardsCoroutine(int cardAmount)
    {
        for (int i = 0; i < cardAmount; i++)
        {
            DrawCardToHand();
            yield return new WaitForSeconds(waitBetweenFirstDrawCards);
        }
    }
}
