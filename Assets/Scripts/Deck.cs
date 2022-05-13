using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> MainDeck = new List<Card>();
    public List<Card> DiscardPile = new List<Card>();
    public List<Card> ActiveHand = new List<Card>();

    public void DrawCard()
    {
        int cardDraw = Random.Range(0, MainDeck.Count - 1);
        if (MainDeck.Count == 0)
        {
            print("you stupid bum, there ain't no cards");
            return;
        }
            Card card = MainDeck[cardDraw];
            MainDeck.RemoveAt(cardDraw);
            ActiveHand.Add(card);
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
