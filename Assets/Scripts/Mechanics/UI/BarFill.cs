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
    private Coroutine currentCoroutine;

    void OnEnable()
    {
        GameEvents.OnFillBar += ChangeBarValue;
    }

    void OnDisable()
    {
        GameEvents.OnFillBar -= ChangeBarValue;
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

    void Update()
    {
        CheckBarValue();
    }

    public void CheckBarValue()
    {
            if (finalValue > uIManager.barValue)
                FillBar();
            else if (finalValue < uIManager.barValue)
                EmptyBar();
    }

    public void ChangeBarValue(float newValue)
    {
        if (uIManager.barValue == firstValue)
        {
            finalValue = firstValue + newValue;
        }
        else
        {
            float lastValue = finalValue;
            finalValue = lastValue + newValue;
        }

        if (finalValue > newMaxValue)
            finalValue = newMaxValue;
        if (finalValue < newMinValue)
            finalValue = newMinValue;
    }

    /// <summary>
    /// Used with an GameEvent, to add value every time the event is called
    /// </summary>
    public void FillBar()
    {
        Debug.Log("enchendo" + finalValue);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        if (finalValue <= newMaxValue)
            currentCoroutine = StartCoroutine(IncreaseOverTime(finalValue));

    }

    /// <summary>
    /// called by an GameEvent, to subtract value from the slider bar
    /// </summary>
    public void EmptyBar()
    {
        Debug.Log("esvaziando" + finalValue);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        if (finalValue >= newMinValue)
            currentCoroutine = StartCoroutine(DecreaseOverTime(finalValue));
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

                if (uIManager.barValue > finalValue)
                    uIManager.barValue = finalValue;

                yield return null;
            }
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

                if (uIManager.barValue < finalValue)
                    uIManager.barValue = finalValue;

                yield return null;
            }
        }
    }
}
