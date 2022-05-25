using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteButtonScript : MonoBehaviour
{
    public bool IsTrue = false;
    public int Val;
    public RoulettePieceColor Color;
}


public enum RoulettePieceColor
{
    red = 1,
    black = 0
}
