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
    public ulong Points => Value == 0 ? 0 : (ulong)Mathf.Pow(2, Value);
    public bool IsEmpty => Value == 0;
    public bool HasMerged { get; private set; }
    public bool IsBonusTile { get; private set; }

    public const int MaxValue = 11;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private GameObject bonusLight;
    [SerializeField] private SOEvent bonusCreated;

    [SerializeField] private SOFloat chanceOfBonusInPercent;
    [SerializeField] private SOBool isBonusesActive;

    private CellAnimation currentAnimation;

    public void SetValue(int x, int y, int value, bool updateUI = true, bool isBonusTile = false)
    {
        this.X = x;
        this.Y = y;
        this.Value = value;
        this.IsBonusTile = isBonusTile;

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

    public void IncreaseBonusValue()
    {
        Value += 2;
        HasMerged = true;

        GameController.instance.AddPoints(Points);
    }
    public void IncreaseDoubleBonusValue()
    {
        Value += 3;
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

        if(IsBonusTile && otherCell.IsBonusTile)
        {
            otherCell.IncreaseDoubleBonusValue();
        }
        else if (IsBonusTile || otherCell.IsBonusTile)
        {
            otherCell.IncreaseBonusValue();
        }
        else
        {
            otherCell.IncreaseValue();
        }



        bool rndBonus = false;

        if (isBonusesActive.value)
        {
            rndBonus = Random.Range(0, 100) <= chanceOfBonusInPercent.value ? true : false;
            if (rndBonus)
            {
                bonusCreated.Raise();
            }
        }

        otherCell.IsBonusTile = rndBonus;

        SetValue(X, Y, 0);
    }

    public void MoveToCell(Cell target)
    {
        CellAnimationController.instance.SmoothTransition(this, target, false);

        target.SetValue(target.X, target.Y, Value, false, IsBonusTile);
        SetValue(X, Y, 0);
    }

    public void UpdateCell()
    {
        points.text = IsEmpty ? string.Empty : Points.ToString();
        points.color = Value <= 1 || Value == 8 ? ColorManager.instance.PointsDarkColor : ColorManager.instance.PointsLigthColor;
        image.color = ColorManager.instance.CellColors[Mathf.Clamp(Value, 0, MaxValue)];
        if(IsBonusTile)
        {
            bonusLight.SetActive(true);
        }
        else
        {
            bonusLight.SetActive(false);
        }
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
