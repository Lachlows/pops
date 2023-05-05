using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public GameObject canva;
    public GameObject spawnerMangger;
    public GameObject mainAudio;


    public bool gameHasStart = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameHasStart && spawnerMangger.GetComponent<spawnState>().playerNumber >= 2)
        {
            StartCoroutine(launchGame());
        }
    }

    IEnumerator launchGame()
    {
        yield return new WaitForSeconds(4f);
        mainAudio.SetActive(true);
        canva.SetActive(false);
        gameHasStart = true;
    }

}
