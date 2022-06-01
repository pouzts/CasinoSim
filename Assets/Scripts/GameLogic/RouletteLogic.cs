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

public class RouletteLogic : Singleton<RouletteLogic>
{ 
    //list of roulette pieces
    public List<GameObject> RouletteList = new List<GameObject>();
    List<GameObject> selectedSpots = new List<GameObject>();

    RoulettePiece winningPiece;

    RoulettePieceColor selectedColor = RoulettePieceColor.noColor;

    public Sprite dead;
    public int chipValue;

    [SerializeField] TMP_Text PlayerBank;
    [SerializeField] TMP_Text InputAmount;
    public GameData gd;
    int betAmount;

    //start
    //generate list
    private void Start()
    {

        

        /*for (int i = 0; i < 36; i++)
        {
            RoulettePiece roulette = new RoulettePiece();
            roulette.value = i + 1;
            RouletteList.Add(roulette);
        }*/
        /*RouletteList[0].color = Color.red;
        RouletteList[1].color = Color.black;
        RouletteList[2].color = Color.red;
        RouletteList[3].color = Color.black;
        RouletteList[4].color = Color.red;
        RouletteList[5].color = Color.black;
        RouletteList[6].color = Color.red;
        RouletteList[7].color = Color.black;
        RouletteList[8].color = Color.red;
        RouletteList[9].color = Color.black;
        RouletteList[10].color = Color.black;
        RouletteList[11].color = Color.red;
        RouletteList[12].color = Color.black;
        RouletteList[13].color = Color.red;
        RouletteList[14].color = Color.black;
        RouletteList[15].color = Color.red;
        RouletteList[16].color = Color.black;
        RouletteList[17].color = Color.red;
        RouletteList[18].color = Color.red;
        RouletteList[19].color = Color.black;
        RouletteList[20].color = Color.red;
        RouletteList[21].color = Color.black;
        RouletteList[22].color = Color.red;
        RouletteList[23].color = Color.black;
        RouletteList[24].color = Color.red;
        RouletteList[25].color = Color.black;
        RouletteList[26].color = Color.red;
        RouletteList[27].color = Color.black;
        RouletteList[28].color = Color.black;
        RouletteList[29].color = Color.red;
        RouletteList[30].color = Color.black;
        RouletteList[31].color = Color.red;
        RouletteList[32].color = Color.black;
        RouletteList[33].color = Color.red;
        RouletteList[34].color = Color.black;
        RouletteList[35].color = Color.red;*/

        PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
        betAmount = 0;
    }

    private void Update()
    {
        
    }

    public void CheckRoulettePiece()
    {
        foreach (var item in RouletteList)
        {
            if (item.GetComponent<RouletteButtonScript>().IsTrue == true)
            {
                print(item.GetComponent<RouletteButtonScript>().Val + " was true");
                selectedSpots.Add(item);
            }
        }
    }

    public void SpinWheel()
    {
        if (gd.intData["PlayerBank"] < 0)
        {
            print("Can't bet anymore");
            return;
        }
        else
        {
            //get random number 0 - 35
            int roll = Random.Range(0, 35);
            //winingPiece = list[rand]
            var winningPiece = RouletteList[roll];
            print("piece value:" + winningPiece.GetComponent<RouletteButtonScript>().Val + "piece color:" + winningPiece.GetComponent<RouletteButtonScript>().Color.ToString());            
            CheckRoulettePiece();

            foreach (var spot in RouletteList)
            {
                if (spot.GetComponent<RouletteButtonScript>().IsTrue == true)
                {
                    gd.intData["PlayerBank"] -= spot.GetComponent<RouletteButtonScript>().betVal;
                }
            }

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
                gd.intData["PlayerBank"] += Utilities.Payout(betAmount, 1);
                print("win");
            }
            else
            {
                print("lose");
            }

            PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
            //betAmount = 0;
            InputAmount.text = "$" + betAmount;


        }

    }

    public void SetSelectedColor(int choice)
    {
        switch (choice)
        {
            case 0:
                selectedColor = RoulettePieceColor.red;
                print("red color selected");
                break;
            case 1:
                selectedColor = RoulettePieceColor.black;
                print("black color selected");
                break;
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
