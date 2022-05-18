using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackJackLogic : Singleton<BlackJackLogic>
{
    [SerializeField] TMP_Text currentBet;
    [SerializeField] GameObject playerHandPanel;
    Deck deck;

    List<Card> dealerHand = new List<Card>();
    List<Card> playerHand = new List<Card>();

    int playerHandVal;
    int bet;

    eGameState gameState;

    public enum eGameState
    {
        PLACE_BETS,
        DEAL_CARDS,
        PLAYER_TURN,
        DEALER_TURN,
        GAME_WON,
        GAME_LOST
    }

    private void Start()
    {
        gameState = eGameState.PLACE_BETS;
    }

    // Update is called once per frame
    void Update()
    {
        currentBet.text = bet.ToString();

        /*switch (gameState)
        {
            case eGameState.PLACE_BETS:
                PlaceBets();
                break;
            case eGameState.DEAL_CARDS:
                DealCards();
                break;
            case eGameState.PLAYER_TURN:
                PlayerTurn();
                break;
            case eGameState.DEALER_TURN:
                DealerTurn();
                    break;
            case eGameState.GAME_WON:
                GameWon();
                break;
            case eGameState.GAME_LOST:
                GameLost();
                break;

        }*/
    }

    void PlaceBets(int betAmmount)
    {
        //enable chip buttons
        //enable "place bet" button
        bet += betAmmount;
    }

    void DealCards()
    {
        //dealerHand.Add(deck.DrawCard());
    }

    void PlayerTurn()
    {

    }

    void DealerTurn()
    {

    }

    void GameWon()
    {

    }

    void GameLost()
    {

    }
}
