using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackLogic : MonoBehaviour
{
    [SerializeField] TMP_Text CurrentBet;
    [SerializeField] TMP_Text PlayerMoney;
    [SerializeField] TMP_Text CardVal;
    [SerializeField] GameObject DealerHandPanel;
    [SerializeField] GameObject PlayerHandPanel;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] GameObject DrawPanel;
    [SerializeField] GameObject Chips;
    [SerializeField] GameObject Hit;
    [SerializeField] GameObject Stand;
    [SerializeField] GameObject Double;
    [SerializeField] GameObject Deal;

    [SerializeField] Texture faceDownCard;
    [SerializeField] GameData gameData;

    public GameObject deck;

    List<GameObject> dealerHand = new List<GameObject>();
    List<GameObject> playerHand = new List<GameObject>();

    int playerHandVal;
    int dealerHandVal;
    int bet;
    int turnCount = 1;

    bool playerStand = false;
    bool dealerStand = false;

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

    class Win
    {
        public bool True { get; set; } = false;
        public int payout { get; set; }
    }

    private void Start()
    {
        //gameState = eGameState.PLACE_BETS;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentBet.text = "$" + bet.ToString();
        PlayerMoney.text = "$" + gameData.intData["PlayerBank"].ToString();

        //disable double if player turn is not 1
        if (turnCount != 1) Double.SetActive(false);

        if (CardVal.IsActive()) CardVal.text = playerHandVal.ToString();

        
    }

    public void AddToBet(int value)
    {
        gameData.intData["PlayerBank"] -= value;
        bet += value;
    }

    public void DealCards()
    {
        //disable chips
        Chips.SetActive(false);
        //disable deal button
        Deal.SetActive(false);

        //enable hit
        Hit.SetActive(true);
        //enable stand
        Stand.SetActive(true);
        //enable double
        Double.SetActive(true);
        //enable card value
        CardVal.enabled = true;

        //take 2 cards out of the deck
        GameObject card1 = Instantiate(deck.GetComponent<Deck>().DrawCard(), DealerHandPanel.transform);
        GameObject card2 = Instantiate(deck.GetComponent<Deck>().DrawCard(), DealerHandPanel.transform);
        //flip the first card over (now facedown)
        card1.GetComponent<RawImage>().texture = faceDownCard;
        //add the cards to the dealer's hand
        dealerHand.Add(card1);
        dealerHand.Add(card2);
        //sum up the cards' value
        AddCardToHandVal(card1, "Dealer");
        AddCardToHandVal(card2, "Dealer");

        //take another 2 cards out of the deck
        card1 = Instantiate(deck.GetComponent<Deck>().DrawCard(), PlayerHandPanel.transform);
        card2 = Instantiate(deck.GetComponent<Deck>().DrawCard(), PlayerHandPanel.transform);
        //add the cards to the player's hand
        playerHand.Add(card1);
        playerHand.Add(card2);
        //sum up the cards' value
        AddCardToHandVal(card1, "Player");
        AddCardToHandVal(card2, "Player");

        if (playerHandVal == 21) GameWon(2);
    }

    public void DoubleBet()
    {
        Double.SetActive(false);
        gameData.intData["PlayerBank"] -= bet;
        bet *= 2;
    }

    public void ReturnCardsToDeck()
    {
        deck.GetComponent<Deck>().ResetDeck();

        foreach (var card in playerHand)
        {
            Destroy(card);
        }
        foreach (var card in dealerHand)
        {
            Destroy(card);
        }
        playerHand = new List<GameObject>();
        dealerHand = new List<GameObject>();
        playerHandVal = 0;
        dealerHandVal = 0;
    }

    public void PlayerHit()
    {
        GameObject card = Instantiate(deck.GetComponent<Deck>().DrawCard(), PlayerHandPanel.transform);
        playerHand.Add(card);

        AddCardToHandVal(card, "Player");

        if (playerHandVal > 21)
        {
            GameLost();
        }
        else
        {
            Win win = CheckForWin();
            if (win.True && win.payout == 0)
            {
                GameDraw();
            }
            else if (win.True)
            {
                GameWon(win.payout);
            }
        }
        
    }

    void DealerTurn()
    {
        if (dealerHandVal >= 17) dealerStand = true;

        if (!dealerStand)
        {
            GameObject card = Instantiate(deck.GetComponent<Deck>().DrawCard(), DealerHandPanel.transform);
            dealerHand.Add(card);
            AddCardToHandVal(card, "Dealer");
            
        }

        if (dealerHandVal == 21) GameLost();
    }

    public void PlayerStand()
    {
        playerStand = true;
        while (!dealerStand)
        {
            DealerTurn();
        }

        Win win = CheckForWin();
        if (win.True && win.payout == 0)
        {
            GameDraw();
        }
        else if (win.True)
        {
            GameWon(win.payout);
        }
        else
        {
            GameLost();
        }
    }

    public void Okay(int panel)
    {
        if (panel == 0)
        {
            LosePanel.SetActive(false);
        }
        else if(panel == 1)
        {
            WinPanel.SetActive(false);
        }
        else if (panel == 2)
        {
            DrawPanel.SetActive(false);
        }

        Restart();
    }

    public void Restart()
    {
        //disable hit
        Hit.SetActive(false);
        //disable stand
        Stand.SetActive(false);
        //disable double
        Double.SetActive(false);
        //enable chips
        Chips.SetActive(true);
        //enable deal button
        Deal.SetActive(true);

        ReturnCardsToDeck();

        //reset stand bool
        playerStand = false;
        dealerStand = false;

        //reset bet
        bet = 0;

    }

    void AddCardToHandVal(GameObject card, string hand, bool checkAce = true)
    {
        if (hand == "Player")
        {
            if (card.GetComponent<Card>().value > 10)
            {
                playerHandVal += 10;
            }
            else if (card.GetComponent<Card>().value == 1)
            {
                playerHandVal += 11;
            }
            else
            {
                playerHandVal += card.GetComponent<Card>().value;
            }
        }
        else
        {
            if (card.GetComponent<Card>().value > 10)
            {
                dealerHandVal += 10;
            }
            else if (card.GetComponent<Card>().value == 1)
            {
                dealerHandVal += 11;
            }
            else
            {
                dealerHandVal += card.GetComponent<Card>().value;
            }
        }

        if(checkAce) CheckForAceChange(hand);

    }

    void CheckForAceChange(string hand)
    {
        if (hand == "Player")
        {
            playerHandVal = 0;
            foreach (var card in playerHand)
            {
                AddCardToHandVal(card, hand, false);
            }

            foreach (var card in playerHand)
            {
                if (playerHandVal > 21)
                {
                    if (card.GetComponent<Card>().value == 1)
                    {
                        playerHandVal -= 10;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            dealerHandVal = 0;
            foreach (var card in dealerHand)
            {
                AddCardToHandVal(card, hand, false);
            }

            foreach (var card in dealerHand)
            {
                if (dealerHandVal > 21)
                {
                    if (card.GetComponent<Card>().value == 1)
                    {
                        dealerHandVal -= 10;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        
    }

    Win CheckForWin()
    {
        Win win = new Win();

        if (playerHandVal == 21)
        {
            win.True = true;
            win.payout = 2;
        } 
        else if (playerStand && dealerStand)
        {
            if (playerHandVal > dealerHandVal)
            {
                win.True = true;
                win.payout = 1;
            }
            else if (playerHandVal == dealerHandVal)
            {
                win.True = true;
                win.payout = 0;
            }

            if (dealerHandVal > 21)
            {
                win.True = true;
                win.payout = 1;
            }

        }

        if (playerHand.Count >= 5)
        {
            win.True = true;
            win.payout = 3;
        }

        return win;
    }

    void GameWon(int payout)
    {
        //enable Win panel
        WinPanel.SetActive(true);
        gameData.intData["PlayerBank"] += Utilities.Payout(bet, payout);
    }

    void GameLost()
    {
        //enable loose panel
        LosePanel.SetActive(true);
    }

    void GameDraw()
    {
        //enable draw pannel
        DrawPanel.SetActive(true); 
        gameData.intData["PlayerBank"] += bet;
    }
}
