using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teamScore : MonoBehaviour
{
    public int ScoreT1 = 0;
    public int ScoreT2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addinScoreToTeam (int teamId)
    {
        if (teamId==0)
        {
            ScoreT1++;
        }else
        {
            ScoreT2++;
        }
    }
}
