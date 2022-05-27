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

        if (IsTrue)
        {
            tempAlpha.a = 0;
            gameObject.GetComponent<Image>().color = tempAlpha;
            IsTrue = false;
            betVal = 0;
        }
            
        else
        {
            tempAlpha.a = 255f;
            temp.sprite = RouletteLogic.Instance.dead;
            gameObject.GetComponent<Image>().sprite = temp.sprite;
            gameObject.GetComponent<Image>().color = tempAlpha;
            IsTrue = true;
            betVal = RouletteLogic.Instance.chipValue;
        }
    }

}


public enum RoulettePieceColor
{
    red = 1,
    black = 0,
    noColor = 2
}