using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popRotate : MonoBehaviour
{
    public float rotationSpeed = 60f; // Vitesse de rotation en degrés par seconde (360/0.5)
    public float frequency = 0.2f;

    private float timeElapsed = 0f;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= frequency)
        {
            Quaternion rotation = Quaternion.Euler(0f, 0f, 30f);
            transform.rotation *= rotation;
            timeElapsed = 0f;
        }
    }
}
