using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> MainDeck = new List<GameObject>();
<<<<<<< HEAD
    public List<Card> DiscardPile = new List<Card>();
    public List<Card> ActiveHand = new List<Card>();
=======
    public List<GameObject> DiscardPile = new List<GameObject>();
    public List<GameObject> ActiveHand = new List<GameObject>();
>>>>>>> b1fa82e46fe0c96dbafeedb02fb47691816f2301

    public GameObject DrawCard()
    {
        int cardDraw = Random.Range(0, MainDeck.Count - 1);
        if (MainDeck.Count == 0)
        {
            print("you stupid bum, there ain't no cards");
            return null;
        }
<<<<<<< HEAD
        
        GameObject card = MainDeck[cardDraw];
        MainDeck.RemoveAt(cardDraw);
        //ActiveHand.Add(card);

        return card;
=======
            var card = MainDeck[cardDraw];
            MainDeck.RemoveAt(cardDraw);
            ActiveHand.Add(card);
>>>>>>> b1fa82e46fe0c96dbafeedb02fb47691816f2301
    }

    public void DiscardHand()
    {
        for (int i = 0; i < ActiveHand.Count; i++)
        {
            var card = ActiveHand[i];
            ActiveHand.RemoveAt(i);
            DiscardPile.Add(card);
        }
    }

    public void ResetDeck()
    {
        foreach (var card in DiscardPile)
        {
            MainDeck.Add(card);
            DiscardPile.Remove(card);
        }
        foreach (var card in ActiveHand)
        {
            MainDeck.Add(card);
            ActiveHand.Remove(card);
        }
        ActiveHand.Clear();
        DiscardPile.Clear();
    }
}
