using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAnimationController : MonoBehaviour
{
    public static CellAnimationController instance { get; private set; }

    [SerializeField]
    private CellAnimation animationPref;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }

    public void SmoothTransition(Cell from, Cell to, bool isMerging)
    {
        Instantiate(animationPref, transform, false).Move(from, to, isMerging);
    }

    public void SmoothAppear(Cell cell)
    {
        Instantiate(animationPref, transform, false).Appear(cell);
    }
   
}
