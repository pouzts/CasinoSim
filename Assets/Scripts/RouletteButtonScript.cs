using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulettePieceButton
{
    public bool IsTrue { get; set; }
    public int Val { get; set; }
    public RoulettePieceColor Color { get; set; }
}


public enum RoulettePieceColor
{
    red = 1,
    black = 0
}
