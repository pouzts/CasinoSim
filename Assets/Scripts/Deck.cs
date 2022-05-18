using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> MainDeck = new List<GameObject>();
    public List<Card> DiscardPile = new List<Card>();
    public List<Card> ActiveHand = new List<Card>();

    public GameObject DrawCard()
    {
        int cardDraw = Random.Range(0, MainDeck.Count - 1);
        if (MainDeck.Count == 0)
        {
            print("you stupid bum, there ain't no cards");
            return null;
        }
        
        GameObject card = MainDeck[cardDraw];
        MainDeck.RemoveAt(cardDraw);
        //ActiveHand.Add(card);

        return card;
    }

    public void DiscardHand()
    {
        for (int i = 0; i < ActiveHand.Count; i++)
        {
            Card card = ActiveHand[i];
            ActiveHand.RemoveAt(i);
            DiscardPile.Add(card);
        }
    }
}
