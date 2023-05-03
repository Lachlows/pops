using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popAction : MonoBehaviour
{
    public int teamId = 0;
    GameObject playerScore;

    private void Start()
    {
        GameObject playerScore = GameObject.Find("teamScoreManager");

        if (teamId == 0)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject playerScore = GameObject.Find("teamScoreManager");

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "target")
        {
            playerScore.GetComponent<teamScore>().addinScoreToTeam(teamId);
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
        else if (collision.gameObject.tag == "projectile")
        {
            Destroy(gameObject);
        }
    }
}
