using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float loLim = 0.005f;
    public float hiLim = 0.1f;
    public float fHigh = 10.0f;
    public float fLow = 5.0f;
    public float curAccel = 0f;
    float avgAccel = 0f;
    bool stateH = false;
    public float m_Speed = 20f;

    private Vector3 m_RotationDirection = Vector3.up;

    public Text stepText;

    public int steps = 0;
    public readonly int waitTime = 30;

    private int oldSteps;

    void Awake()
    {
        avgAccel = Input.acceleration.magnitude;
    }

    void Update()
    {
        transform.Rotate(m_RotationDirection * Time.deltaTime * m_Speed);
        if (steps % 10 == 1 && steps != oldSteps)
        {
            ToggleRotationDirection();
        }
        oldSteps = steps;
    }

    public void ToggleRotationDirection()
    {
        Debug.Log("Toggling rotation direction");

        if (m_RotationDirection == Vector3.up)
        {
            m_RotationDirection = Vector3.down;
        }
        else
        {
            m_RotationDirection = Vector3.up;
        }
    }

    void FixedUpdate()
    {
        curAccel = Mathf.Lerp(curAccel, Input.acceleration.magnitude, Time.deltaTime * fHigh);
        avgAccel = Mathf.Lerp(avgAccel, Input.acceleration.magnitude, Time.deltaTime * fLow);
        float delta = curAccel - avgAccel;
        if (!stateH)
        {
            // State is low
            if (delta > hiLim)
            {
                stateH = true;
                steps++;
            }
        }
        else
        {
            // State is high
            if (delta < loLim)
            {
                stateH = false;
            }
        }
        stepText.text = "CA: " + curAccel.ToString() + "\nAA: " + avgAccel.ToString() + "\nDl: " + delta.ToString() + "\nSteps: " + steps.ToString();
    }
}
