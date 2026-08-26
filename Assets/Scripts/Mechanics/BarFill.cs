using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class BarFill : MonoBehaviour
{
    public Slider myBar;
    public float newMinValue;
    public float newMaxValue;
    public float speedOfIncrement;
    private float finalValue;
    private float firstValue;
    private bool isToStop;

    void OnEnable()
    {
        GameEvents.OnFillBar += FillBar;
    }

    void OnDisable()
    {
        GameEvents.OnFillBar -= FillBar;
    }

    void Awake()
    {
        firstValue = myBar.value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myBar.minValue = newMinValue;
        myBar.maxValue = newMaxValue;
    }

    /// <summary>
    /// Used with an event listener, to increase value every time a pickup is collected
    /// </summary>
    /// <param name="pickupValue">new value of pickup</param>
    public void FillBar(float pickupValue)
    {
        if (myBar.value == firstValue)
            finalValue = firstValue + pickupValue;
        else
        {
            float lastValue = finalValue;
            finalValue = lastValue + pickupValue;
        }

        if (finalValue < newMaxValue)
            StartCoroutine(IncreaseOverTime(finalValue));
        else
            if (!isToStop)
            {
                StartCoroutine(IncreaseOverTime(finalValue));
                isToStop = true;
            }

        if (isToStop)
            if (myBar.value < newMaxValue)
                isToStop = false;

    }

    /// <summary>
    /// increase value of bar over speed * time
    /// </summary>
    /// <param name="finalValue">the current bar value + increment</param>
    /// <returns></returns>
    public IEnumerator IncreaseOverTime(float finalValue)
    {
        if (myBar.value < newMaxValue)
        {
            float currentValue;
            currentValue = myBar.value;

            while (currentValue < finalValue)
            {
                currentValue += 0.1f * speedOfIncrement * Time.deltaTime;

                myBar.value = currentValue;

                yield return null;
            }
            if (myBar.value > finalValue)
                myBar.value = finalValue;
        }
    }
}
