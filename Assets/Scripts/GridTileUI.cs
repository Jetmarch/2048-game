using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileUI : MonoBehaviour
{
    public int x, y;
    public int tileScore;

    public void MoveToX(int newX, float stepSize)
    {
        int destX = x - newX;

        Vector3 newPos = transform.position;

        newPos.x -= destX * stepSize;
        StartCoroutine(Moving(newPos, .6f));
    }

    public void MoveToY(int newY, float stepSize)
    {
        int destY = y - newY;

        Vector3 newPos = transform.position;

        newPos.y += destY * stepSize;
        StartCoroutine(Moving(newPos, .6f));
    }

    IEnumerator Moving(Vector3 target, float timeToReachNewPos)
    {
        Vector3 startPosition = transform.position;
        float t = 0;
        while (transform.position != target)
        {
            t += Time.deltaTime / timeToReachNewPos;
            transform.position = Vector3.Lerp(startPosition, target, t);
            yield return new WaitForEndOfFrame();
            Debug.Log("Are you here?");
        }

    }
}
