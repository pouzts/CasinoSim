using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteButtonScript : MonoBehaviour
{
    public bool IsTrue = false;
    public int Val;
    public RoulettePieceColor Color;
    public int betVal;


    public void OnClick()
    {
        var temp = GetComponent<Image>();
        var tempAlpha = GetComponent<Image>().color;
        if (RouletteLogic.Instance.gd.intData["PlayerBank"] < 0)
        {
            print("Can't bet anymore");
            return;
        }
        else
        {
            if (IsTrue)
            {
                tempAlpha.a = 0;
                gameObject.GetComponent<Image>().color = tempAlpha;
                IsTrue = false;
                RouletteLogic.Instance.gd.intData["PlayerBank"] += betVal;
                betVal = 0;
                //RouletteLogic.Instance.PlayerBank.text = "$" + RouletteLogic.Instance.gd.intData["PlayerBank"].ToString();
            }
            
            else
            {
                tempAlpha.a = 255f;
                temp.sprite = RouletteLogic.Instance.dead;
                gameObject.GetComponent<Image>().sprite = temp.sprite;
                gameObject.GetComponent<Image>().color = tempAlpha;
                IsTrue = true;
                betVal = RouletteLogic.Instance.chipValue;
                RouletteLogic.Instance.gd.intData["PlayerBank"] -= betVal;
                //RouletteLogic.Instance.PlayerBank.text = "$" + RouletteLogic.Instance.gd.intData["PlayerBank"].ToString();
            }
        }

    }

    public void ResetButton()
    {
        var tempAlpha = GetComponent<Image>().color;
        IsTrue = false;
        betVal = 0;
        tempAlpha.a = 0;
        gameObject.GetComponent<Image>().color = tempAlpha;
        //RouletteLogic.Instance.PlayerBank.text = "$" + RouletteLogic.Instance.gd.intData["PlayerBank"].ToString();
    }

    public void SetSelectedColor(int choice)
    {
        switch (choice)
        {
            case 0:
                RouletteLogic.Instance.selectedColor = RoulettePieceColor.red;
                print("red color selected");
                break;
            case 1:
                RouletteLogic.Instance.selectedColor = RoulettePieceColor.black;
                print("black color selected");
                break;
        }
    }

}


public enum RoulettePieceColor
{
    red = 1,
    black = 0,
    noColor = 2
}