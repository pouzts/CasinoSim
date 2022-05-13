using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardSuit   
{
    Hearts = 1,
    Clubs = 2,
    Diamonds = 3,
    Spades = 4
}
public class Card : MonoBehaviour
{

    private CardSuit cardSuit;
    private int value;

    public CardSuit Suit { get { return cardSuit; } }
    public int cardValue { get { return value; } }

    public GameObject card;

    public Card(CardSuit suit, int rank, Vector2 position, Quaternion rotation)
    {
        string assetName = string.Format("Card_{0}_{1}", suit, rank);
        GameObject asset = GameObject.Find(assetName);
        if (asset == null)
        {
            Debug.LogError("Asset '" + assetName + "' could not be found.");
        }
        else
        {
            card = Instantiate(asset, position, rotation);
            cardSuit = suit;
            value = rank;
        }
    }
}
