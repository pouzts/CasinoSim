using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoulettePiece
{
    public int value;
    public Color color;
    string colorString;
}

public class RoulettePieceButton
{
    public bool IsTrue { get; set; }
    public int Val { get; set; }
    public Color Color { get; set; }
}

public class RouletteLogic : MonoBehaviour
{
    //list of roulette pieces
    public List<GameObject> RouletteList = new List<GameObject>();

    RoulettePiece winningPiece;

    Color selectedColor = Color.blue;

    [SerializeField] TMP_Text PlayerBank;
    [SerializeField] TMP_Text InputAmount;
    public GameData gd;
    int betAmount;

    //start
    //generate list
    private void Start()
    {
        for (int i = 0; i < 36; i++)
        {
            RoulettePiece roulette = new RoulettePiece();
            roulette.value = i + 1;
            //RouletteList.Add(roulette);
        }
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


    public void SpinWheel()
    {
        if (gd.intData["PlayerBank"] < 0)
        {
            print("Can't bet anymore");
            return;
        }
        else if (selectedColor == Color.blue)
        {
            print("no color selected");
            return;
        }
        else
        {
            //get random number 0 - 35
            int roll = Random.Range(0, 35);
            //winingPiece = list[rand]
            var winningPiece = RouletteList[roll];
            //print("piece value:" + winningPiece.value + "piece color:" + winningPiece.color.ToString());

            //player selected color = winningPiece.color
            /*if (selectedColor == winningPiece.color)
            {
                gd.intData["PlayerBank"] += Utilities.Payout(betAmount, 1);
                print("win");
            }
            else
            {
                print("lose");
            }*/

            PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
            betAmount = 0;
            InputAmount.text = "$" + betAmount;
        }

    }

    public void SetSelectedColor(int choice)
    {
        switch (choice)
        {
            case 0:
                selectedColor = Color.red;
                print("red color selected");
                break;
            case 1:
                selectedColor = Color.black;
                print("black color selected");
                break;
        }
    }
    public void AddBetAmount(int input)
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
            gd.intData["PlayerBank"] -= input;
            PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
            betAmount += input;
            InputAmount.text = "$" + betAmount;
        }

    }
}
