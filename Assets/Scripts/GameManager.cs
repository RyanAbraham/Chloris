﻿using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float loLim = 0.005f;
    public float hiLim = 0.1f;
    public float fHigh = 10.0f;
    public float fLow = 0.2f;
    public readonly int waitTime = 30;
    public GameObject coin;

    private float curAccel = 0f;
    private float avgAccel = 0f;
    private int steps = 0;
    private int oldSteps;
    private bool stateH = false;
    private GameObject[] pickups = new GameObject[3];

    // Yamo cube and/or testing variables
    private Vector3 m_RotationDirection = Vector3.up;
    public float m_Speed = 20f;
    public Text stepText;

    void Awake()
    {
        avgAccel = Input.acceleration.magnitude;
    }

    void Update()
    {
        transform.Rotate(m_RotationDirection * Time.deltaTime * m_Speed);
        if (steps % 10 == 1 && steps != oldSteps) {
            ToggleRotationDirection();
        }
        oldSteps = steps;
    }

    public void ToggleRotationDirection()
    {
        //Debug.Log("Toggling rotation direction");

        if (m_RotationDirection == Vector3.up) {
            m_RotationDirection = Vector3.down;
        } else {
            m_RotationDirection = Vector3.up;
        }
    }

    void FixedUpdate()
    {
        //steps++;
        curAccel = Mathf.Lerp(curAccel, Input.acceleration.magnitude, Time.deltaTime * fHigh);
        avgAccel = Mathf.Lerp(avgAccel, Input.acceleration.magnitude, Time.deltaTime * fLow);
        float delta = curAccel - avgAccel;
        if (!stateH) {
            // State is low
            if (delta > hiLim) {
                stateH = true;
                steps++;
                // Handle pickups[0]
                if (pickups[0] != null) Destroy(pickups[0]);
                for (int i = 1; i < pickups.Length; i++) {
                    Debug.Log(i);
                    Debug.Log(pickups[i]);
                    if (pickups[i] != null) {
                        Debug.Log("woo");
                        pickups[i].transform.position = new Vector3(0, i-1, -2);
                        pickups[i - 1] = pickups[i];
                        pickups[i] = null;
                    }
                }
                if (Random.Range(1,10) <= 2) {
                    pickups[pickups.Length - 1] = Instantiate(coin);
                    pickups[2].transform.position = new Vector3(0, 2, -2);
                }
            }
        } else {
            // State is high
            if (delta < loLim) {
                stateH = false;
            }
        }
        //stepText.text = "CA: " + curAccel.ToString() + "\nAA: " + avgAccel.ToString() + "\nDl: " + delta.ToString() + "\nSteps: " + steps.ToString();
    }
}
