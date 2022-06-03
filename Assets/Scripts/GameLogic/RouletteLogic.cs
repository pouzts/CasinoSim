using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoulettePiece
{
    public int value;
    public Color color;
    string colorString;
}

public class RouletteLogic : MonoBehaviour
{ 
    //list of roulette pieces
    public List<GameObject> RouletteList = new List<GameObject>();
    List<GameObject> selectedSpots = new List<GameObject>();

    RoulettePiece winningPiece;

    public static RoulettePieceColor selectedColor = RoulettePieceColor.noColor;

    public Sprite currentSprite;
    public static Sprite dead;
    public static int chipValue { get; set; }
    public GameObject selectedColorPiece;

    [SerializeField] public TMP_Text PlayerBank;
    [SerializeField] public TMP_Text InputAmount;
    [SerializeField] GameObject Lose;
    public GameData gd;
    int betAmount;

    //start
    //generate list
    private void Start()
    {
        chipValue = 1;
        dead = currentSprite;
        Lose.SetActive(false);

        PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
        InputAmount.text = "$" + 0;
    }

    private void Update()
    {
        InputAmount.text = "$" + betAmount;
        PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
    }

    public static void SetSelectedColor(RoulettePieceColor color)
    {
        selectedColor = color;
    }

    public int GetPlayerBank()
    {
        return gd.intData["PlayerBank"];
    }

    public void UpdatePlayerBank(GameObject gameObject)
    {
        //print("this object was selected");
        switch (gameObject.GetComponent<RouletteButtonScript>().IsTrue)
        {
            case true:
                gd.intData["PlayerBank"] -= gameObject.GetComponent<RouletteButtonScript>().betVal;
                betAmount += gameObject.GetComponent<RouletteButtonScript>().betVal;
                break;
            case false:
                gd.intData["PlayerBank"] += gameObject.GetComponent<RouletteButtonScript>().betVal;
                betAmount -= gameObject.GetComponent<RouletteButtonScript>().betVal;
                gameObject.GetComponent<RouletteButtonScript>().betVal = 0;
                break;
        }
        PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
        InputAmount.text = "$" + betAmount;
    }

    public static Sprite GetCurrentChip()
    {
        return dead;
    }

    public static int GetChipValue()
    {
        return chipValue;
    }

    public void CheckRoulettePiece()
    {
        for (int i = 0; i < 36; i++)
        {
            if (RouletteList[i].GetComponent<RouletteButtonScript>().IsTrue == true)
            {
                //print(RouletteList[i].GetComponent<RouletteButtonScript>().Val + " was true");
                selectedSpots.Add(RouletteList[i]);
            }
        }
    }

    public void SetSelectedColor(int choice)
    {
        switch (choice)
        {
            case 0:
                selectedColor = RoulettePieceColor.red;
                selectedColorPiece = RouletteList[36];
                //print("red color selected");
                break;
            case 1:
                selectedColor = RoulettePieceColor.black;
                selectedColorPiece = RouletteList[37];
                //print("black color selected");
                break;
        }        
    }

    public void SpinWheel()
    {      
        if (gd.intData["PlayerBank"] < 0)
        {
            Lose.SetActive(true);
            print("Can't bet anymore");
            return;
        }
        else
        {
            //get random number 0 - 35
            int roll = Random.Range(0, 35);
            //winingPiece = list[rand]
            var winningPiece = RouletteList[1];
            print("piece value:" + winningPiece.GetComponent<RouletteButtonScript>().Val + "piece color:" + winningPiece.GetComponent<RouletteButtonScript>().Color.ToString());            
            CheckRoulettePiece();

            /*foreach (var spot in RouletteList)
            {
                if (spot.GetComponent<RouletteButtonScript>().IsTrue == true)
                {
                    gd.intData["PlayerBank"] -= spot.GetComponent<RouletteButtonScript>().betVal;
                }
            }*/

            for (int i = 0; i < selectedSpots.Count; i++)
            {
                if (selectedSpots[i].GetComponent<RouletteButtonScript>().Val == winningPiece.GetComponent<RouletteButtonScript>().Val && selectedSpots[i].GetComponent<RouletteButtonScript>().Color == winningPiece.GetComponent<RouletteButtonScript>().Color)
                {
                    if (selectedSpots.Count == 1)
                    {
                        gd.intData["PlayerBank"] += Utilities.Payout(selectedSpots[i].GetComponent<RouletteButtonScript>().betVal, 35);
                    }
                    else if (selectedSpots.Count == 2)
                    {
                        gd.intData["PlayerBank"] += Utilities.Payout(selectedSpots[i].GetComponent<RouletteButtonScript>().betVal, 17);
                    }
                    else if (selectedSpots.Count == 3)
                    {
                        gd.intData["PlayerBank"] += Utilities.Payout(selectedSpots[i].GetComponent<RouletteButtonScript>().betVal, 11);
                    }
                    else if (selectedSpots.Count == 4)
                    {
                        gd.intData["PlayerBank"] += Utilities.Payout(selectedSpots[i].GetComponent<RouletteButtonScript>().betVal, 8);
                    }
                    else if (selectedSpots.Count == 5)
                    {
                        gd.intData["PlayerBank"] += Utilities.Payout(selectedSpots[i].GetComponent<RouletteButtonScript>().betVal, 6);
                    }
                    else if (selectedSpots.Count == 6)
                    {
                        gd.intData["PlayerBank"] += Utilities.Payout(selectedSpots[i].GetComponent<RouletteButtonScript>().betVal, 5);
                    }
                    else
                    {
                        gd.intData["PlayerBank"] += Utilities.Payout(selectedSpots[i].GetComponent<RouletteButtonScript>().betVal, 1);
                    }

                }
            }

            print(selectedSpots.Count);

            //player selected color = winningPiece.color
            if (selectedColor == winningPiece.GetComponent<RouletteButtonScript>().Color)
            {
                gd.intData["PlayerBank"] += Utilities.Payout(selectedColorPiece.GetComponent<RouletteButtonScript>().betVal, 1);
                print("win");
            }
            else
            {
                print("lose");
            }

            PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
            betAmount = 0;
            InputAmount.text = "$" + betAmount;
            foreach(var lol in RouletteList)
            {
                lol.GetComponent<RouletteButtonScript>().ResetButton();
            }

        }

    }
    public void SetBetAmount(int input)
    {
        if (gd.intData["PlayerBank"] < 0)
        {
            print("Can't bet anymore");
            return;
        }
        else
        {
            if (input > gd.intData["PlayerBank"])
            {
                print("Can't bet anymore");
                return;
            }
            chipValue = input;
/*            //gd.intData["PlayerBank"] -= input;
            PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
            //betAmount += input;
            InputAmount.text = "$" + betAmount;*/
        }

    }

    public void SetSprite(Sprite sprite)
    {
        dead = sprite;
    }
}
