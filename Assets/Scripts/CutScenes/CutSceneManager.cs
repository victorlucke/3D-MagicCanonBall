using System;
using System.Collections.Generic;
using UnityEngine;

public class MageCutScene : MonoBehaviour
{
    private static int currentAnimationIndex;
    public static bool isRuning;
    private static int runingAnimationHashName;

    [Serializable]
    public struct AnimatorData
    {
        public string nameParameter;
        public bool isbool;
        public float numericValue;
        public bool boolValue;
        public int animIndex;
    }

    public List<AnimatorData> animatorDataList;
    // public List<float> floatParameterValue;
    // public List<string> nameParameters;
    // public List<bool> boolParameterValue;

    void Awake()
    {
        if (!isRuning)
        {
            currentAnimationIndex = 0;
            isRuning = true;
        }
    }

    void Update()
    {
        if (isRuning)
        {
            RunAnimation(animatorDataList);
        }
    }

    void RunAnimation(List<AnimatorData> animDataList)
    {
        Animator myAnimator = GetComponent<Animator>();
        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
        int currentAnimHashName = stateInfo.shortNameHash;
        //Debug.Log(stateInfo.normalizedTime +" index "+ currentAnimationIndex);

        if (stateInfo.normalizedTime >= 0.9 && currentAnimHashName != runingAnimationHashName)
        {
            runingAnimationHashName = stateInfo.shortNameHash;
            
            foreach (var data in animDataList)
            {
                if (data.animIndex == currentAnimationIndex + 1)
                {
                    if (data.isbool)
                    {
                        currentAnimationIndex = data.animIndex;
                        SetBoolParameter(myAnimator, data.nameParameter, data.boolValue);
                        break;
                    }
                    else
                    {
                        currentAnimationIndex = data.animIndex;
                        SetNumericParameter(myAnimator, data.nameParameter, data.numericValue);
                        break;
                    }
                }
            }
        }
    }

    void SetBoolParameter(Animator animator, string nameParameter, bool valueParemeter)
    {
        Debug.Log(nameParameter +" "+ valueParemeter);
        animator.SetBool(nameParameter, valueParemeter);
        animator.SetTrigger(nameParameter);
        // boolParameterData
    }

    void SetNumericParameter(Animator animator, string nameParameter, float valueParemeter)
    {
        animator.SetFloat(nameParameter, valueParemeter);
        animator.SetInteger(nameParameter, (int)valueParemeter);
    }

    void CutScene()
    {
        isRuning = false;
    }
}
