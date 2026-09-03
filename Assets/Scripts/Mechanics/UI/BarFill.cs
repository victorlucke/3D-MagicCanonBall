using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarFill : MonoBehaviour
{
    public float newMinValue;
    public float newMaxValue;
    public float speedOfBarFill;
    private float finalValue;
    private float firstValue;
    private bool isToStop;
    private UIManager uIManager;

    void OnEnable()
    {
        GameEvents.OnFillBar += FillBar;
        GameEvents.OnEmptyBar += EmptyBar;
    }

    void OnDisable()
    {
        GameEvents.OnFillBar -= FillBar;
        GameEvents.OnEmptyBar -= EmptyBar;
    }

    void Awake()
    {
        uIManager = UIManager.Instance;
        firstValue = uIManager.barValue;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uIManager.magicBar.minValue = newMinValue;
        uIManager.magicBar.maxValue = newMaxValue;
    }

    public void ChangeBarValue(float newValue)
    {
        if (uIManager.barValue == firstValue)
            finalValue = firstValue + newValue;
        else
        {
            float lastValue = finalValue;
            finalValue = lastValue + newValue;
            Debug.Log(finalValue + " valor final");
        }


    }

    /// <summary>
    /// Used with an GameEvent, to add value every time the event is called
    /// </summary>
    /// <param name="addValue">new value of pickup</param>
    public void FillBar(float addValue)
    {
        if (uIManager.barValue == firstValue)
            finalValue = firstValue + addValue;
        else
        {
            float lastValue = finalValue;
            finalValue = lastValue + addValue;
            Debug.Log(finalValue + " valor final");
        }

        if (finalValue < newMaxValue)
            StartCoroutine(IncreaseOverTime(finalValue));
        else
        {
            finalValue = newMaxValue;
            if (!isToStop)
            {
                StartCoroutine(IncreaseOverTime(finalValue));
                isToStop = true;
            }
        }

        if (isToStop)
            if (uIManager.barValue < newMaxValue)
                isToStop = false;

    }

    /// <summary>
    /// called by an GameEvent, to subtract value from the slider bar
    /// </summary>
    /// <param name="subtractValue">value to subtract</param>
    public void EmptyBar(float subtractValue)
    {
        if (uIManager.barValue == firstValue)
            finalValue = firstValue + subtractValue;
        else
        {
            float lastValue = finalValue;
            finalValue = lastValue + subtractValue;
        }

        if (finalValue > newMinValue)
            StartCoroutine(DecreaseOverTime(finalValue));
        else
        {
            finalValue = newMinValue;
            if (!isToStop)
            {
                StartCoroutine(DecreaseOverTime(finalValue));
                isToStop = true;
            }
        }

        if (isToStop)
            if (uIManager.barValue > newMinValue)
                isToStop = false;
    }

    /// <summary>
    /// increase value of bar over speed * time
    /// </summary>
    /// <param name="finalValue">the current bar value + increment</param>
    /// <returns></returns>
    public IEnumerator IncreaseOverTime(float finalValue)
    {
        if (uIManager.barValue < newMaxValue)
        {
            float currentValue;
            currentValue = uIManager.barValue;

            while (currentValue < finalValue)
            {
                currentValue += 0.1f * speedOfBarFill * Time.deltaTime;

                uIManager.barValue = currentValue;

                yield return null;
            }
            if (uIManager.barValue > finalValue)
                uIManager.barValue = finalValue;
        }
    }

    public IEnumerator DecreaseOverTime(float finalValue)
    {
        if (uIManager.barValue > newMinValue)
        {
            float currentValue;
            currentValue = uIManager.barValue;

            while (currentValue > finalValue)
            {
                currentValue -= 0.1f * speedOfBarFill * Time.deltaTime;

                uIManager.barValue = currentValue;

                yield return null;
            }
            if (uIManager.barValue < finalValue)
                uIManager.barValue = finalValue;
        }
    }
}
