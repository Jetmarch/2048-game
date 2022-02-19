
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;

    public Color[] CellColors;

    [Space(5)]
    public Color PointsDarkColor;
    public Color PointsLigthColor;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

}
