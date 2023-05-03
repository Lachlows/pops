using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject target;
    public GameObject startManager;
    public GameObject scoreManager;


    private void Start()
    {
        scoreManager = GameObject.Find("teamScoreManager");
        startManager = GameObject.Find("gameStarter");
        InvokeRepeating("Spawn", 0f, Random.Range(1.5f,3f));
    }
    void Spawn ()
    {
        if (startManager.GetComponent<startGame>().gameHasStart && !scoreManager.GetComponent<teamScore>().endGame)
        {
            float scaleTarget = Random.Range(3f, 6f);

            GameObject targetToSpawn = Instantiate(target, new Vector3(Random.Range(-9, 9), Random.Range(-7, 7), 0), Quaternion.identity);
            targetToSpawn.transform.localScale = new Vector3(scaleTarget, scaleTarget, scaleTarget);
        }
    }
}