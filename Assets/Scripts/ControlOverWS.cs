﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySocketIO;
using UnitySocketIO.Events;

public class ControlOverWS : MonoBehaviour
{

    SocketIOController io;

    Rigidbody rgbd;

    public float power = 6;
    public Transform directionMarker;
    public float directionMarkerInitLength = 0.1f;
    public float directionMarkerMaxLength = 2;
    public float directionMarkerGrowSpeed = 0.1f;
    public float directionMarkerCurrentLength;
    public float inactifDelay = 10;
    public bool isActif = true;
    private float timer = 0;
    private Vector3 direction;
    private Vector3 initialSize;
    public int score = 0;
    public int scoreIncr = 1;
    public float scoreTickDelay = 2;
    public float scoreTickTimer = 0;
    public GameObject scoreDisplay;
    
    private System.Action<SocketIOEvent> input1Action,input2Action;

    public GameObject popCornT;
    //public GameObject popCornT2;
    Transform playerPosition;
    public Transform shootSpawnT1;
    public Transform shootSpawnT2;
   public Vector2 forceBase;
    public int team = 0;
    int spawnPlace = 0 ;
    public int playerId = 0;

    public int personalPlayerScore = 0;

    GameObject spawnMangager;
    public GameObject startManager;
    public GameObject scoreManager;
    public GameObject handSprite;
    Animator handAnim;


    public float projectileSpeed;

    //public Transform[] transformList;

    // Start is called before the first frame update
    void Start()
    {
        handAnim = handSprite.GetComponent<Animator>();
        spawnMangager = GameObject.Find("spawnPoints");
        startManager = GameObject.Find("gameStarter");
        scoreManager = GameObject.Find("teamScoreManager");

        //gameObject.name = gameObject.name.Replace("\"", "");

        initialSize = transform.localScale;
        directionMarkerCurrentLength = directionMarkerInitLength;
        //rgbd = GetComponent<Rigidbody>();
        playerPosition = GetComponent<Transform>();

        io = SocketIOController.instance;

        input1Action = (SocketIOEvent e) => {
            string[] data = e.data.Replace("\"", "").Split(",");
            Debug.Log("nom : "+data[0]+ "force : "+ data[1] + "angle : "+data[2]);

            if (data[0] == gameObject.name && startManager.GetComponent<startGame>().gameHasStart && !scoreManager.GetComponent<teamScore>().endGame)
            {
                
                Debug.Log("SHOOT");

                string[] forceText = data[1].Split('.');
                Debug.Log("force : " + forceText[0]);
                float force = float.Parse(forceText[0]);

                string[] angleText = data[2].Split('.');
                Debug.Log("angle : " + angleText[0]);
                float angle = float.Parse(angleText[0]);

                Debug.Log("force : " + force + "angle : " + angle);
                /*
                if (directionMarker)
                {
                    direction = directionMarker.position - transform.position;
                }
                else
                {
                    direction = Vector3.up;
                }
                rgbd.AddForce(direction * power, ForceMode.Impulse);
                */
                StartCoroutine(spawnWait(force,angle));
                //spawnProjectile();
                isActif = true;
                timer = 0;
                
            }

        };

        input2Action = (SocketIOEvent e) => {
            string data = e.data.Replace("\"", "");
            if (data == gameObject.name)
            {
                leaveGame();
                /*if (directionMarker)
                {
                    directionMarker.GetComponent<TurnAround>().rotationSpeed *= -1;
                    isActif = true;
                    timer = 0;
                }*/
            }
        };

        io.On("input1", input1Action);

        io.On("input2",input2Action);

        // Trouver tous les Game Objects avec le tag spécifié
        GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("spawnPoint");

        // Initialiser la liste de Transform
        List<Transform> spawnPointPos = new List<Transform>();

        // Ajouter chaque Transform dans la liste
        foreach (GameObject gameObject in gameObjectsWithTag)
        {
            spawnPointPos.Add(gameObject.transform);
        }

        //checker la position pour définir la team
        for (int i = 0; i < spawnPointPos.Count ; i++)
        {
            if (playerPosition.position == spawnPointPos[i].position)
            {
                spawnPlace = i;
                if (gameObject.transform.position.x < 0)
                {
                    team = 0;
                    //handSprite.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    team = 1;
                    //GetComponent<Renderer>().material.color = Color.blue;
                }
                handAnim.SetInteger("team", team);
            }

        }


        //ajout des joueur dans la liste
       /* if (playerPosition.position == spawnPointPos[0].position || playerPosition.position == spawnPointPos[2].position || playerPosition.position == spawnPointPos[4].position || playerPosition.position == spawnPointPos[6].position)
        {

        } 
        else
        {

        }
       */


    }



    // Update is called once per frame
    void Update()
    {
        if (!startManager.GetComponent<startGame>().gameHasStart && scoreManager.GetComponent<teamScore>().endGame)
        {
            handSprite.gameObject.GetComponent<Renderer>().enabled = false;
        }
                else{
            handSprite.gameObject.GetComponent<Renderer>().enabled = true;
        }

        timer += Time.deltaTime;
        if (timer > inactifDelay)
        {
            isActif = false;
        }

        /*if (isActif)
        {
            resetLength();
        }
        else
        {
            growUp();
        }*/

        scoreTickTimer += Time.deltaTime;
        if (scoreTickTimer > scoreTickDelay)
        {
            updateScore();
            scoreTickTimer = 0;
        }
    }

    private void updateScore()
    {

        score += scoreIncr;
        //if(scoreDisplay)
        //scoreDisplay.GetComponent<ScoreData>().updateDisplay(score);
    }
    
    private void growUp()
    {
        if (directionMarkerCurrentLength < directionMarkerMaxLength)
        {
            directionMarkerCurrentLength += directionMarkerGrowSpeed * Time.deltaTime;    
        }
        directionMarker.localScale = new Vector3(directionMarkerCurrentLength,initialSize.y,initialSize.z);

    }

    private void resetLength()
    {
        directionMarkerCurrentLength = directionMarkerInitLength;
        directionMarker.localScale = new Vector3(directionMarkerCurrentLength,initialSize.y,initialSize.z);
    }

    private void OnDestroy()
    {
        io.Off("input1", input1Action);
        io.Off("input2", input2Action);
        Destroy(scoreDisplay);
        //GameManager.instance.scoreList.Remove(scoreDisplay.GetComponent<ScoreData>());

    }

    void spawnProjectile(float forceP, float angle)
    {
        //Vector2 forceVect = force;
        Rigidbody rb;
        forceP *= projectileSpeed * Time.deltaTime;

        if (team == 0)
        {
            GameObject projectile = Instantiate(popCornT, shootSpawnT1.position, shootSpawnT1.rotation);
            //popAction script = projectile.GetComponent<popAction>();
            rb = projectile.GetComponent<Rigidbody>();

            float angleEnRadians = angle * Mathf.Deg2Rad;

            Vector2 vecteur = new Vector2(forceP, 0);


            float x = vecteur.x * Mathf.Cos(angleEnRadians) - vecteur.y * Mathf.Sin(angleEnRadians);
            float y = vecteur.x * Mathf.Sin(angleEnRadians) + vecteur.y * Mathf.Cos(angleEnRadians);



            rb.AddForce(new Vector2(y, x));

            projectile.GetComponent<popAction>().playerObject = gameObject;
        }
        else
        {
            GameObject projectile = Instantiate(popCornT, shootSpawnT2.position, shootSpawnT2.rotation);
            /* projectile.GetComponent<popAction>().teamId = 1;
             projectile.GetComponent<Rigidbody>().AddForce(-forceVect);*/

            rb = projectile.GetComponent<Rigidbody>();


            float angleEnRadians = angle * Mathf.Deg2Rad;

            Vector2 vecteur = new Vector2(forceP, 0);


            float x = vecteur.x * Mathf.Cos(angleEnRadians) - vecteur.y * Mathf.Sin(angleEnRadians);
            float y = vecteur.x * Mathf.Sin(angleEnRadians) + vecteur.y * Mathf.Cos(angleEnRadians);



            rb.AddForce(new Vector2(-y,-x));

            projectile.GetComponent<popAction>().playerObject = gameObject;
            projectile.GetComponent<popAction>().teamId = 1;

        }
    }
    void leaveGame()
    {
        gameObject.transform.GetComponentInChildren<TMPro.TextMeshPro>().text = "none";
        gameObject.name = "none";
        spawnMangager.GetComponent<spawnState>().leaveGame(playerId);
        gameObject.SetActive(false);
    }

    public void addinScoreToPerso(int point)
    {
        personalPlayerScore += point;
    }
    IEnumerator spawnWait(float forceP, float angle)
    {
        handAnim.SetTrigger("shoot");
        yield return new WaitForSeconds(0.2f);
        spawnProjectile( forceP, angle);
    }

}
