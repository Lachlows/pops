using System.Collections;
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

    public GameObject popCorn;
    Transform playerPosition;
    public Vector2 force;
    public int team = 0;

    //public Transform[] transformList;

    // Start is called before the first frame update
    void Start()
    {
        initialSize = transform.localScale;
        directionMarkerCurrentLength = directionMarkerInitLength;
        //rgbd = GetComponent<Rigidbody>();
        playerPosition = GetComponent<Transform>();

        io = SocketIOController.instance;

        input1Action = (SocketIOEvent e) => {

            if (e.data == gameObject.name)
            {
                
                Debug.Log("SHOOT");
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
                spawnProjectile(playerPosition, force);
                isActif = true;
                timer = 0;
                
            }

        };

        input2Action = (SocketIOEvent e) => {
            if (e.data == gameObject.name)
            {
                if (directionMarker)
                {
                    directionMarker.GetComponent<TurnAround>().rotationSpeed *= -1;
                    isActif = true;
                    timer = 0;
                }
            }
        };

        io.On("input1", input1Action);

        io.On("input2",input2Action);

        // Trouver tous les Game Objects avec le tag spécifié
        GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("spawnPoint");

        // Initialiser la liste de Transform
        List<Transform> transformList = new List<Transform>();

        // Ajouter chaque Transform dans la liste
        foreach (GameObject gameObject in gameObjectsWithTag)
        {
            transformList.Add(gameObject.transform);
        }

        if (playerPosition.position == transformList[0].position || playerPosition.position == transformList[2].position || playerPosition.position == transformList[4].position || playerPosition.position == transformList[6].position)
        {
            team = 0;
        }
        else
        {
            team = 1;
        }


    }

    // Update is called once per frame
    void Update()
    {
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
        GameManager.instance.scoreList.Remove(scoreDisplay.GetComponent<ScoreData>());

    }

    void spawnProjectile(Transform Pposition, Vector2 Pforce)
    {
        GameObject projectile = Instantiate (popCorn, Pposition.position, Pposition.rotation);
        if (team == 0)
        {
            projectile.GetComponent<Rigidbody>().AddForce(Pforce);
        } else
        {
            projectile.GetComponent<Rigidbody>().AddForce(-Pforce);
        }
    }
}
