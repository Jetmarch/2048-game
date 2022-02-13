using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileUI : MonoBehaviour
{
    public int x, y;
    public int tileScore;
    public float stepSizeDefault = 58; //temporary
    public void MoveToX(int newX, bool isDestroyAfter = false, float stepSize = 58)
    {
        int destX = x - newX;
        x = newX;
        Vector3 newPos = transform.position;

        newPos.x -= destX * stepSize;
        StartCoroutine(Moving(newPos, .3f, isDestroyAfter));
    }

    public void MoveToY(int newY, bool isDestroyAfter = false, float stepSize = 58)
    {
        int destY = y - newY;
        y = newY;
        Vector3 newPos = transform.position;

        newPos.y += destY * stepSize;
        StartCoroutine(Moving(newPos, .3f, isDestroyAfter));
    }

    IEnumerator Moving(Vector3 target, float timeToReachNewPos, bool isDestroyAfter)
    {
        Vector3 startPosition = transform.position;
        float t = 0;
        while (transform.position != target)
        {
            t += Time.deltaTime / timeToReachNewPos;
            transform.position = Vector3.Lerp(startPosition, target, t);
            yield return new WaitForEndOfFrame();
        }
        if(isDestroyAfter)
        {
            Debug.Log($"Destroyed x: {x} y: {y}");
            Destroy(this.gameObject);
        }

    }
}
