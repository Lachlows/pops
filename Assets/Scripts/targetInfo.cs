using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetInfo : MonoBehaviour
{
    public int pointToTeam = 1;
    public float MinTimeSpawn = 2.5f;
    public float MaxTimeSpawn = 5f;
    public int MaxOfThisTarget = 6;

    public bool iSRangeTarget = false;
    public float minScale = 1f;
    public float maxScale = 3f;

    public int life = 1;

    public bool movable = false;

    public float minX=-9;
    public float maxX=9;

    public float minY=-7;
    public float maxY=7;

    public float speed = 5f; // vitesse de d�placement de l'objet
    public float changeDirectionTime = 2f; // temps entre chaque changement de direction

    private Vector3 direction; // direction de d�placement
    private float timer; // compteur de temps
    public int teamIdTarget = 0;
    public bool allTeam = true;

    private void Start()
    {
        if(movable)
        {
            // initialisation de la direction al�atoire
            direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            direction.Normalize();
        }
    }
    void Update()
    {
        if (movable)
        {
            // mise � jour du compteur de temps
            timer += Time.deltaTime;

            // si le temps �coul� est sup�rieur ou �gal au temps de changement de direction
            if (timer >= changeDirectionTime)
            {
                // g�n�re une nouvelle direction al�atoire
                direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                direction.Normalize();

                // r�initialise le compteur de temps
                timer = 0f;
            }

            // calcule la nouvelle position de l'objet en fonction de sa direction et de sa vitesse
            Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

            // v�rifie si la nouvelle position est � l'int�rieur de l'espace limit�
            if (Mathf.Abs(newPosition.x) > maxX || Mathf.Abs(newPosition.y) > maxY)
            {
                // si la nouvelle position est � l'ext�rieur de l'espace limit�, inverse la direction
                direction = -direction;
                newPosition = transform.position + direction * speed * Time.deltaTime;
            }

            // met � jour la position de l'objet
            transform.position = newPosition;
        }
    }
}
