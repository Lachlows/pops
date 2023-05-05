using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popAction : MonoBehaviour
{
    public int teamId = 0;
    //GameObject playerScore;
    public GameObject scoreManager;
    public int scoreMalus = -1;

    public GameObject playerObject;
    public GameObject image;

    public Color colorT1;
    public Color colorT2;


    public List<AudioClip> audioClips = new List<AudioClip>();

    private void Start()
    {
        scoreManager = GameObject.Find("teamScoreManager");
        GameObject playerScore = GameObject.Find("teamScoreManager");



        if (teamId == 0)
        {
            GetComponent<Renderer>().material.color = Color.red;
            image.GetComponent<Renderer>().material.color = colorT1;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.blue;
            image.GetComponent<Renderer>().material.color = colorT2;

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject playerScore = GameObject.Find("teamScoreManager");

        if (collision.gameObject.tag == "Player")
        {

            if (collision.gameObject.GetComponent<ControlOverWS>().team==0 && playerScore.GetComponent<teamScore>().ScoreT1>0)
            {
                playerScore.GetComponent<teamScore>().addinScoreToTeam(0, scoreMalus);

            } else if (collision.gameObject.GetComponent<ControlOverWS>().team == 1 && playerScore.GetComponent<teamScore>().ScoreT2 > 0)
            {
                playerScore.GetComponent<teamScore>().addinScoreToTeam(1, scoreMalus);
            }
            if (collision.gameObject.GetComponent<ControlOverWS>().personalPlayerScore> 0)
            {
                collision.gameObject.GetComponent<ControlOverWS>().addinScoreToPerso(scoreMalus);
            }
            int randomIndex = Random.Range(0, audioClips.Count);
            AudioClip randomClip = audioClips[randomIndex];
            GetComponent<AudioSource>().PlayOneShot(randomClip);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "target")
        {

            collision.gameObject.GetComponent<targetInfo>().life--;
            if (collision.gameObject.GetComponent<targetInfo>().life <= 0)
            {
                if (collision.gameObject.GetComponent<targetInfo>().allTeam || collision.gameObject.GetComponent<targetInfo>().teamIdTarget == teamId)
                {
                    playerScore.GetComponent<teamScore>().addinScoreToTeam(teamId, collision.gameObject.GetComponent<targetInfo>().pointToTeam);
                    playerObject.GetComponent<ControlOverWS>().addinScoreToPerso(collision.gameObject.GetComponent<targetInfo>().pointToTeam);  
                }
                StartCoroutine(Despawn(collision.gameObject));
            }
            else
            {
                int randomIndex = Random.Range(0, audioClips.Count);
                AudioClip randomClip = audioClips[randomIndex];
                GetComponent<AudioSource>().PlayOneShot(randomClip);
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "projectile")
        {
            if (collision.gameObject.GetComponent<ControlOverWS>().team != teamId)
            {
                int randomIndex = Random.Range(0, audioClips.Count);
                AudioClip randomClip = audioClips[randomIndex];
                GetComponent<AudioSource>().PlayOneShot(randomClip);
                Destroy(gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        if (scoreManager.GetComponent<teamScore>().endGame)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Despawn(GameObject target)
    {
        int randomIndex = Random.Range(0, audioClips.Count);
        AudioClip randomClip = audioClips[randomIndex];
        GetComponent<AudioSource>().PlayOneShot(randomClip);
        target.GetComponent<targetInfo>().spotLight.intensity = 4f;

        yield return new WaitForSeconds(0.2f);
        Destroy(target);

        Destroy(gameObject);
    }
}
