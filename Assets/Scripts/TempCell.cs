using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������������ ��� ���������� ���������� ��������� ����
public class TempCell 
{
    public int X { get; set; }
    public int Y { get; set; }

    public int Value { get; set; }

    public bool IsBonusTile { get; set; }

    public TempCell (int x, int y, int value, bool isBonusTile)
    {
        X = x;
        Y = y;
        Value = value;
        IsBonusTile = isBonusTile;
    }
}
