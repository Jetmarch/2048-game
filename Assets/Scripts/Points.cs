using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Points
{
    public ulong Amount { get; set; }
    public Points(ulong points = 0)
    {
        Amount = points;
    }


    public override string ToString()
    {
        if(Amount > 999 && Amount <= 999999)
        {
            return Amount.ToString("0,.#K", CultureInfo.InvariantCulture);
        }

        else if (Amount > 999999 && Amount <= 999999999)
        {
            return Amount.ToString("0,,.##M", CultureInfo.InvariantCulture);
        }

        else if (Amount > 999999999)
        {
            return Amount.ToString("0,,,.###B", CultureInfo.InvariantCulture);
        }

        return Amount.ToString();
    }
}
