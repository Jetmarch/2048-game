using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public int Value { get; private set; }
    public int Points => Value == 0 ? 0 : (int)Mathf.Pow(2, Value);
    public bool IsEmpty => Value == 0;
    public bool HasMerged { get; private set; }

    public const int MaxValue = 11;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI points;

    private CellAnimation currentAnimation;

    public void SetValue(int x, int y, int value, bool updateUI = true)
    {
        this.X = x;
        this.Y = y;
        this.Value = value;

        if (updateUI)
        {
            UpdateCell();
        }
    }

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;

        GameController.instance.AddPoints(Points);

    }

    public void ResetFlag()
    {
        HasMerged = false;
    }

    public void MergeWithCell(Cell otherCell)
    {
        CellAnimationController.instance.SmoothTransition(this, otherCell, true);
        otherCell.IncreaseValue();
        SetValue(X, Y, 0);

    }

    public void MoveToCell(Cell target)
    {
        CellAnimationController.instance.SmoothTransition(this, target, false);

        target.SetValue(target.X, target.Y, Value, false);
        SetValue(X, Y, 0);

    }

    public void UpdateCell()
    {
        points.text = IsEmpty ? string.Empty : Points.ToString();
        points.color = Value <= 2 ? ColorManager.instance.PointsDarkColor : ColorManager.instance.PointsLigthColor;
        image.color = ColorManager.instance.CellColors[Value];
    }

    public void SetAnimation(CellAnimation animation)
    {
        currentAnimation = animation;
    }

    public void CancelAnimation()
    {
        if(currentAnimation != null)
        {
            currentAnimation.Destroy();
        }
    }

}