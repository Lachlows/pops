using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnState : MonoBehaviour
{
    //public bool[] stateSpawnPoints;
    public List<bool> stateSpawnPoints = new List<bool>(); 
    int numberSpawnPoint;

    public int numberTeam1;
    public int numberTeam2;

    public int playerNumber;
    // Start is called before the first frame update
    void Start()
    {
        numberSpawnPoint = gameObject.transform.childCount;
        for (int i = 0; i < numberSpawnPoint; i++)
        {
            stateSpawnPoints.Add(false);
        }
    }

    public int GetRandomPos()
    {

        for (int i = 0; i < numberSpawnPoint; i++)
        {
            if (!stateSpawnPoints[i])
            {
                stateSpawnPoints[i] = true;
                return i;
            }
        }
        return -1;

        /*
        int posToGet = 0;
        numberTeam1 = 0;
        numberTeam2 = 0;

        for (int i = 0; i < numberSpawnPoint; i++)
        {
            if (stateSpawnPoints[i])
            {
                if (i % 2 == 0 || i==0)
                {
                    numberTeam1++;
                }
                else
                {
                    numberTeam2++;
                }
            }
        }

        if (numberTeam1 > numberTeam2) 
        {
            do
            {
                posToGet = Mathf.RoundToInt(Random.Range(0, numberSpawnPoint));
            } while ((posToGet % 2 != 0 || posToGet != 0) && stateSpawnPoints[posToGet]);
        } 
        else if (numberTeam1 < numberTeam2)
        {
            do
            {
                posToGet = Mathf.RoundToInt(Random.Range(0, numberSpawnPoint));
            } while ((posToGet % 2 == 0 || posToGet == 0) && stateSpawnPoints[posToGet]);
        }
        else
        {
            do
            {
                posToGet = Mathf.RoundToInt(Random.Range(0, numberSpawnPoint));
            } while (stateSpawnPoints[posToGet]);
        }
        stateSpawnPoints[posToGet] = true;


        numberTeam1 = 0;
        numberTeam2 = 0;
        for (int i = 0; i < numberSpawnPoint; i++)
        {
            if (stateSpawnPoints[i])
            {
                if (i % 2 == 0 || i == 0)
                {
                    numberTeam1++;
                }
                else
                {
                    numberTeam2++;
                }
            }
        }
        return posToGet;
        */

    }
    public bool CheckIfIsFull ()
    {
        for (int i = 0; i < numberSpawnPoint; i++)
        {
            if (!stateSpawnPoints[i])
            {
                return false;
            }
        }
        return true;
    }

    public void leaveGame(int id)
    {
        stateSpawnPoints[id] = false;
    }

    private void Update()
    {
        playerNumber = 0;
        for (int i = 0; i < numberSpawnPoint; i++)
        {
            if (stateSpawnPoints[i])
            {
                playerNumber++;
            }
        }
    }
}
