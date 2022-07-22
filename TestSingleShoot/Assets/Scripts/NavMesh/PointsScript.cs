using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsScript : MonoBehaviour
{
    public GameObject[] nextPointsForNormalDirection;

    private GameObject nextPoint;
    public GameObject ChoiseRandomNextPoint()
    {
        nextPoint = nextPointsForNormalDirection[Random.Range(0, nextPointsForNormalDirection.Length)];
        return nextPoint;
    }
}
