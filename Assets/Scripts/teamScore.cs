using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class teamScore : MonoBehaviour
{
    public int ScoreT1 = 0;
    public int ScoreT2 = 0;

    public TextMeshProUGUI textT1;
    public TextMeshProUGUI textT2;

    public void addinScoreToTeam (int teamId)
    {
        if (teamId==0)
        {
            ScoreT1++;
        }else
        {
            ScoreT2++;
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
    }
}
