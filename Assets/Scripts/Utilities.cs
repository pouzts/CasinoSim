using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static int Payout(int bet, int mult)
    {
        return bet + (mult * bet);
    }
   
    
}
