using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject[] target;
    public GameObject startManager;
    public GameObject scoreManager;


    private void Start()
    {
        scoreManager = GameObject.Find("teamScoreManager");
        startManager = GameObject.Find("gameStarter");

        //InvokeRepeating("Spawn", target[0].GetComponent<targetInfo>().MinTimeSpawn, Random.Range(target[0].GetComponent<targetInfo>().MinTimeSpawn, target[0].GetComponent<targetInfo>().MinTimeSpawn), target[0]);
        for (int i =0; i<target.Length; i++)
        {
            StartCoroutine(SpawnCoroutine(target[i]));

        }

        IEnumerator SpawnCoroutine(GameObject typeOfTarget)
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(typeOfTarget.GetComponent<targetInfo>().MinTimeSpawn, typeOfTarget.GetComponent<targetInfo>().MaxTimeSpawn));
                Spawn(typeOfTarget);
            }
        }
    }
    void Spawn (GameObject typeOfTarget)
    {
        toMuchSameObject(typeOfTarget);

        if (startManager.GetComponent<startGame>().gameHasStart && !scoreManager.GetComponent<teamScore>().endGame)
        {
            GameObject targetToSpawn = Instantiate(typeOfTarget, new Vector3(Random.Range( typeOfTarget.GetComponent<targetInfo>().minX, typeOfTarget.GetComponent<targetInfo>().maxX), Random.Range(typeOfTarget.GetComponent<targetInfo>().minY, typeOfTarget.GetComponent<targetInfo>().maxY), 0), Quaternion.identity);
            if (typeOfTarget.GetComponent<targetInfo>().iSRangeTarget)
            {
                float scaleTarget = Random.Range(typeOfTarget.transform.localScale.x*typeOfTarget.GetComponent<targetInfo>().minScale, typeOfTarget.transform.localScale.x * typeOfTarget.GetComponent<targetInfo>().maxScale);
                targetToSpawn.transform.localScale = new Vector3(scaleTarget, scaleTarget, scaleTarget);
            }
        }
    }

    void toMuchSameObject (GameObject typeOfTarget)
    {
        string myGameObjectName= typeOfTarget.name;
        int count = 0;
        ArrayList thisTargetList = new ArrayList();


        GameObject[] objects = GameObject.FindGameObjectsWithTag("target");
        foreach (GameObject obj in objects)
        {
            if (obj.name.StartsWith(myGameObjectName))
            {
                thisTargetList.Add(obj.gameObject);
                count++;
            }
        }
        Debug.Log("Il y a " + count + " fois la " + myGameObjectName);
        if (count >= typeOfTarget.GetComponent<targetInfo>().MaxOfThisTarget)
        {
            //return true;
            //objects[typeOfTarget.GetComponent<targetInfo>().MaxOfThisTarget - 1].SetActive(false);
            Destroy((GameObject)thisTargetList[typeOfTarget.GetComponent<targetInfo>().MaxOfThisTarget - 1]);
            //return false;
        }
        /*else
        {
            return false;
        }*/
    }
}