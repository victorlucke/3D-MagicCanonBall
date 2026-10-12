using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarFill : MonoBehaviour
{
    public float newMinValue;
    public float newMaxValue;
    public float speedOfBarFill;
    private Slider thisBarFill;
    private float finalValue;
    private float firstValue;
    private Coroutine currentCoroutine;
    private Coroutine noManaCoroutine;

    void Awake()
    {
        thisBarFill = GetComponent<Slider>();
        
        firstValue = thisBarFill.value;

        thisBarFill.minValue = newMinValue;
        thisBarFill.maxValue = newMaxValue;
    }

    void Update()
    {
        //ChangeBarValue(GameManager.Instance.manaAmount);
        CheckBarValue(GameManager.Instance.manaAmount);
        if (GameManager.Instance.NoMana) NoMana();
    }

    /// <summary>
    /// Save the final value of bar, making dont exceed max or min values. Final value wil be used later to update bar
    /// </summary>
    /// <param name="newValue"></param>
    public void ChangeBarValue(float newValue)
    {
        if (thisBarFill.value == firstValue)
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
    /// Chack if final value has changed to upddate bar value
    /// </summary>
    public void CheckBarValue(float myFinalValue)
    {
        if (myFinalValue > thisBarFill.value)
            FillBar(myFinalValue);
        else if (myFinalValue < thisBarFill.value)
            EmptyBar(myFinalValue);
    }

    /// <summary>
    /// Used with an GameEvent, to add value every time the event is called
    /// </summary>
    /// <param name="myFinalValue"></param>
    public void FillBar(float myFinalValue)
    {
        //Debug.Log("enchendo" + finalValue);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        if (myFinalValue <= newMaxValue)
            currentCoroutine = StartCoroutine(IncreaseOverTime(myFinalValue));

    }

    /// <summary>
    /// called by an GameEvent, to subtract value from the slider bar
    /// </summary>
    /// <param name="myFinalValue"></param>
    public void EmptyBar(float myFinalValue)
    {
        //Debug.Log("esvaziando" + finalValue);
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        if (myFinalValue >= newMinValue)
            currentCoroutine = StartCoroutine(DecreaseOverTime(myFinalValue));
    }

    /// <summary>
    /// increase value of bar over speed * time
    /// </summary>
    /// <param name="myFinalValue">the current bar value + increment</param>
    /// <returns></returns>
    public IEnumerator IncreaseOverTime(float myFinalValue)
    {
        if (thisBarFill.value < newMaxValue)
        {
            float currentValue;
            currentValue = thisBarFill.value;

            while (currentValue < myFinalValue)
            {
                currentValue += 0.1f * speedOfBarFill * Time.deltaTime;

                thisBarFill.value = currentValue;

                if (thisBarFill.value > myFinalValue)
                    thisBarFill.value = myFinalValue;

                yield return null;
            }
        }
    }

    /// <summary>
    /// Decrease value of bar over speed * time
    /// </summary>
    /// <param name="myFinalValue">the current bar value - decrement</param>
    /// <returns></returns>
    public IEnumerator DecreaseOverTime(float myFinalValue)
    {
        if (thisBarFill.value > newMinValue)
        {
            float currentValue;
            currentValue = thisBarFill.value;

            while (currentValue > myFinalValue)
            {
                currentValue -= 0.1f * speedOfBarFill * Time.deltaTime;

                thisBarFill.value = currentValue;

                if (thisBarFill.value < myFinalValue)
                    thisBarFill.value = myFinalValue;

                yield return null;
            }
        }
    }

    /// <summary>
    /// Start blink bar color only when the bar stop blink the last time
    /// </summary>
    private void NoMana()
    {
        GameManager.Instance.NoMana = false;
        if (noManaCoroutine == null)
        {
            noManaCoroutine = StartCoroutine(ChangeManaColor());
        }
    }

    /// <summary>
    /// Make bar blink red over time
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeManaColor()
    {
        Image myImage;
        Color defaultColor;
        Color warningColor;

        myImage = GetComponentInChildren<Image>();

        defaultColor = Color.white;
        warningColor = Color.red;

        UIManager.Instance.ChangeImageColor(myImage, warningColor);

        yield return new WaitForSeconds(.5f);
        UIManager.Instance.ChangeImageColor(myImage, defaultColor);

        yield return new WaitForSeconds(.5f);
        UIManager.Instance.ChangeImageColor(myImage, warningColor);

        yield return new WaitForSeconds(.5f);
        UIManager.Instance.ChangeImageColor(myImage, defaultColor);
        noManaCoroutine = null;
    }
}
