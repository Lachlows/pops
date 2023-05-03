using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popAction : MonoBehaviour
{
    public int teamId = 0;

    private void Start()
    {
        if (teamId == 0)
        {

        }else
        {

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "target")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
