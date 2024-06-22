using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollector : MonoBehaviour
{
    [SerializeField] private float currentPoints;

    public void increasePoints(float pointIncrease)
    {
        currentPoints += pointIncrease;
    }

    public float getPoints()
    {
        return currentPoints;
    }

    public void resetPoints()
    {
        currentPoints = 0;
    }
}
