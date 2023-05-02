using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnHouse : MonoBehaviour
{
    [SerializeField] private Transform houseSpawns;
    [SerializeField] private Transform houses;
    private Transform[] spawnList;
    private House[] houseList;

    private void Awake()
    {
        spawnList = houseSpawns.GetComponentsInChildren<Transform>();
        houseList = houses.GetComponentsInChildren<House>();
        
        SetRandomPosHouse();
    }

    private void SetRandomPosHouse()
    {
        int aux = 0;
        foreach (House h in houseList) 
        {
            int randomPos;
            bool condition = false;
            do
            {
                randomPos = Random.Range(1, spawnList.Length);

                for (int i = 0; i < aux; i++)
                {
                    condition = houseList[i].position == randomPos;
                    if(condition)
                    {
                        break;
                    }
                }

            } while (condition);

            h.hasPosition = true;
            h.position = randomPos;
            h.transform.position = spawnList[h.position].transform.position;

            aux++;
        }
    }
}
