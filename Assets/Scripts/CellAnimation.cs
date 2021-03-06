using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellAnimation : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI points;
    [SerializeField]
    private GameObject bonusLight;

    private float moveTime = .1f;
    private float appearTime = .1f;

    private Sequence sequence;
    public void Move(Cell from, Cell to, bool isMerging)
    {
        from.CancelAnimation();
        to.SetAnimation(this);

        image.color = ColorManager.instance.CellColors[Mathf.Clamp(from.Value, 0, Cell.MaxValue)];
        points.text = from.Points.ToString();
        points.color = from.Value <= 1 || from.Value == 8 ?
            ColorManager.instance.PointsDarkColor :
            ColorManager.instance.PointsLigthColor;
        if (from.IsBonusTile)
        {
            bonusLight.SetActive(true);
        }
        else
        {
            bonusLight.SetActive(false);
        }

        transform.position = from.transform.position;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad));

        if(isMerging)
        {
            sequence.AppendCallback(() =>
           {
               image.color = ColorManager.instance.CellColors[Mathf.Clamp(to.Value, 0, Cell.MaxValue)];
               points.text = to.Points.ToString();
               points.color = to.Value <= 1 || to.Value == 8 ?
                    ColorManager.instance.PointsDarkColor :
                    ColorManager.instance.PointsLigthColor;

               if (to.IsBonusTile)
               {
                   bonusLight.SetActive(true);
               }
               else
               {
                   bonusLight.SetActive(false);
               }
           });

            sequence.Append(transform.DOScale(1.2f, appearTime));
            sequence.Append(transform.DOScale(1f, appearTime));
        }

        sequence.AppendCallback(() =>
        {
            to.UpdateCell();
            Destroy();
        });
    }

    public void Appear(Cell cell)
    {
        cell.CancelAnimation();
        cell.SetAnimation(this);

        image.color = ColorManager.instance.CellColors[Mathf.Clamp(cell.Value, 0, Cell.MaxValue)];
        points.text = cell.Points.ToString();
        points.color = cell.Value <= 1 || cell.Value == 8 ?
             ColorManager.instance.PointsDarkColor :
             ColorManager.instance.PointsLigthColor;
        if (cell.IsBonusTile)
        {
            bonusLight.SetActive(true);
        }
        else
        {
            bonusLight.SetActive(false);
        }

        transform.position = cell.transform.position;
        transform.localScale = Vector2.zero;

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(1.2f, appearTime * 2));
        sequence.Append(transform.DOScale(1f, appearTime * 2));
        sequence.AppendCallback(() => 
        {
            cell.UpdateCell();
            Destroy();
        });
    }

    public void Destroy()
    {
        sequence.Kill();
        Destroy(gameObject);
    }
}
