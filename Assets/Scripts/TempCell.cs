using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Используется для сохранения предыдущих состояний поля
public class TempCell 
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Value { get; set; }

    public TempCell (int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;
    }
}
