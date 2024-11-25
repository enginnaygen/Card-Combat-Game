using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController Instance;

    [SerializeField] List<CardScriptableObject> deckToUse = new List<CardScriptableObject>();
    List<CardScriptableObject> activeCards = new List<CardScriptableObject>();

    public List<CardScriptableObject> DeckToUse => deckToUse;


    private void Awake()
    {
        Singelton();
    }

    private void Start()
    {
        SetupDeck();
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
}
