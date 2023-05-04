using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class teamScore : MonoBehaviour
{
    public int ScoreT1 = 0;
    public int ScoreT2 = 0;

    public int maxScore = 0;

    public TextMeshProUGUI textT1;
    public TextMeshProUGUI textT2;
    public TextMeshProUGUI textTeam1Win1;
    public TextMeshProUGUI textTeam2Win1;
    public TextMeshProUGUI textTeam1Win2;
    public TextMeshProUGUI textTeam2Win2;
    public string textToWinT1 = "Score Team 1 : ";
    public string textToWinT2 = "Score Team 2 : ";

    bool winTeam1 =false;
    bool winTeam2 =false;

    public GameObject winLayerT1;
    public GameObject winLayerT2;
    public bool endGame = false;

    public Animator animatorBarreT1;
    public Animator animatorBarreT2;

    private void Start()
    {
        //animatorBarreT1 = barreT1.GetComponent<Animator>();
       // animatorBarreT2 = barreT2.GetComponent<Animator>();
    }

    public void addinScoreToTeam (int teamId, int point)
    {
        if (teamId==0)
        {
            ScoreT1+= point;
        }else
        {
            ScoreT2 += point;
        }
        updateScore();

    }
    void updateScore ()
    {
        Debug.Log("Score Team 1 : " + ScoreT1 + " Score Team 2 : " + ScoreT2);
        //string scoreTextToPrintT1 = ScoreT1.ToString("00");
        textT1.text = ScoreT1.ToString("00");
        //string scoreTextToPrintT2 = ScoreT2.ToString("00");
        textT2.text = ScoreT2.ToString("00");

        // animatorBarreT1.Play("calcT1", -1, (ScoreT1 / maxScore) * 100);
        // animatorBarreT2.Play("calcT2", -1, (ScoreT2 / maxScore) * 100);

        //animatorBarreT1.Play("calcT1", 0, 0.5f);
        float scoreT1Pourcent = ((float)ScoreT1 / maxScore);
        float scoreT2Pourcent = ((float)ScoreT2 / maxScore);

        Debug.Log("time barre 1 : " + scoreT1Pourcent + " time barre 2 : " + scoreT2Pourcent);


        animatorBarreT1.Play("calcT1", 0, scoreT2Pourcent);
        animatorBarreT1.CrossFade("calcT1", 0.2f);
        animatorBarreT2.Play("calcT2", 0, scoreT1Pourcent);
        //animatorBarreT2.Play("calcT2", 0,0.5f);
        animatorBarreT2.CrossFade("calcT2", 0.2f);

        if (ScoreT1>= maxScore)
        {
            winLayerT1.SetActive(true);
            textTeam1Win1.text = textToWinT1+ ScoreT1.ToString("00");
            textTeam2Win1.text = textToWinT2 + ScoreT2.ToString("00");

            winTeam1 = true;
            endGame = true;
        } else if (ScoreT2 >= maxScore) {
            winLayerT2.SetActive(true);
            textTeam1Win2.text = textToWinT1 + ScoreT1.ToString("00");
            textTeam2Win2.text = textToWinT2 + ScoreT2.ToString("00");
            winTeam2 = true;
            endGame = true;
        }
    }
}
