using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackLogic : Singleton<BlackJackLogic>
{
    [SerializeField] TMP_Text currentBet;
    [SerializeField] GameObject dealerHandPanel;
    [SerializeField] GameObject playerHandPanel;
    [SerializeField] Texture faceDownCard;

    public GameObject deck;

    List<GameObject> dealerHand = new List<GameObject>();
    List<GameObject> playerHand = new List<GameObject>();

    int playerHandVal;
    int bet;

    //eGameState gameState;

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
        //gameState = eGameState.PLACE_BETS;
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

    public void PlaceBets()
    {
        //enable chip buttons
        //enable "place bet" button

        //int betAmmount = int.Parse(currentBet.text);
        //bet += betAmmount;

        DealCards();

    }

    public void AddToBet(int value)
    {
        bet += value;
        currentBet.text = "$" + bet.ToString();
    }

    public void DealCards()
    {
        GameObject card1 = Instantiate(deck.GetComponent<Deck>().DrawCard(), dealerHandPanel.transform);
        GameObject card2 = Instantiate(deck.GetComponent<Deck>().DrawCard(), dealerHandPanel.transform);

        card1.GetComponent<RawImage>().texture = faceDownCard;

        dealerHand.Add(card1);
        dealerHand.Add(card2);

        card1 = Instantiate(deck.GetComponent<Deck>().DrawCard(), playerHandPanel.transform);
        card2 = Instantiate(deck.GetComponent<Deck>().DrawCard(), playerHandPanel.transform);
        dealerHand.Add(card1);
        dealerHand.Add(card2);
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
