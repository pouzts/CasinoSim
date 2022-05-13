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

    public CardSuit cardSuit;
    public int value;
}
