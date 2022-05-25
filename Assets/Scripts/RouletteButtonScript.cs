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
        var tempColor = gameObject.GetComponent<RawImage>().color;

        if (IsTrue)
        {
            tempColor.a = 0f;
            gameObject.GetComponent<RawImage>().color = tempColor;
            IsTrue = false;
        }
            
        else
        {
            tempColor.a = 255f;
            gameObject.GetComponent<RawImage>().color = tempColor;
            IsTrue = true;
        }
    }

}


public enum RoulettePieceColor
{
    red = 1,
    black = 0
}