using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverBrick : MonoBehaviour
{
    [SerializeField] int hitsToDestroy = 2; 
    int currentHits = 0;
    public void ReduceHits()
    {
        currentHits++;

        if (currentHits >= hitsToDestroy)
        {
            Destroy(gameObject);
            Score.Instance.AddPointsNormalBrick(Score.BlockType.SilverBrick);
        }
    }
}