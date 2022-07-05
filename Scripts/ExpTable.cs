using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpTable : MonoBehaviour
{
    private int[] expTable = new int[98];
    void Awake()
    {
        for (int i = 1; i < 99; i++)
        {
            if(i == 1)
            {
                expTable[i - 1] = 10;
            }
            else
            {
                float nextExp = (expTable[i - 2] * 1.18f + i * 11f) * 0.9f;
                expTable[i - 1] = (int)Mathf.Ceil(nextExp);
            }    
        }
    }

    public int GetNextExp(int myLevel)
    {
        return expTable[myLevel-1];
    }
}
