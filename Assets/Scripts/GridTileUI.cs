using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileUI : MonoBehaviour
{
    public int x, y;
    public int tileScore;

    public void MoveTo(int newX, int newY, float stepSize)
    {
        int destX = x - newX;
        int destY = y - newY;

        Vector3 newPos = transform.position;

        if(destX > 0)
        {
            newPos.x -= destX * stepSize;
            StartCoroutine(Moving(newPos, 1f));
        }
        else if(destX < 0)
        {
            newPos.x += destX * stepSize;
            StartCoroutine(Moving(newPos, 1f));
        }
        else if(destX == 0)
        {
            Debug.Log("Тайл стоит на месте");
        }

    }

    IEnumerator Moving(Vector3 target, float timeToReachNewPos)
    {
        Vector3 startPosition = transform.position;
        float t = Time.deltaTime / timeToReachNewPos;
        while (true)
        {
            transform.position = Vector3.Lerp(startPosition, target, t);
            yield return new WaitForSeconds(.1f);
            Debug.Log("Are you here?");
        }
    }
}
