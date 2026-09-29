using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : EnemyMovement
{
    [Header("MAGE CLASS")]
    [Header("Mage Controller")]
    //public List<GameObject> knowingSpellTest;
    public List<GameObject> spellSlots;
    public int spellCoolDown;
    private bool isCastCooldown;
    private bool isCastingAlready;
    private bool isHurt;

    protected override void Awake()
    {
        StartSeeTarget();

        playerTransform = GameObject.FindWithTag("Player").gameObject.transform;
        animator = GetComponent<Animator>();
        isCastingAlready = false;
        //sawEnemy = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        LookAtTarget();
        LookPlayer();
        UpdateSpellSlotRange((int)GameManager.Instance.DificultyMultiplayer());
        SeeingPlayer();
    }

    /// <summary>
    /// search for player transform in the scene
    /// </summary>
    void LookPlayer()
    {
        if (playerTransform == null)
            if (GameObject.FindWithTag("Player") != null)
                playerTransform = GameObject.FindWithTag("Player").gameObject.transform;
    }

    /// <summary>
    /// change the amount of spells that can be casted
    /// </summary>
    /// <param name="newRange">new amount of spells</param>
    void UpdateSpellSlotRange(int newRange)
    {
        if (spellSlots.Count < newRange)
        {
            for(int i = spellSlots.Count; i < newRange; i++)
            {
                spellSlots.Add(null);
            }
        }else if(newRange < spellSlots.Count)
        {
            for(int i = newRange; i < spellSlots.Count; i++)
            {
                spellSlots.RemoveAt(spellSlots.Count - 1);
            }
        }
    }

    /// <summary>
    /// change animation based in seeing the target
    /// </summary>
    void SeeingPlayer()
    {
        // if (playerTransform != null)
        //     sawEnemy = true;

        // else if (playerTransform == null)
        //     sawEnemy = false;

        StartAnimations(sawEnemy);
    }

    /// <summary>
    /// control mage animations
    /// </summary>
    /// <param name="isToStart">if true, activate animations saw enemy and is casting</param>
    void StartAnimations(bool isToStart)
    {
        bool changeAnimation = isToStart;

        if (animator != null)
        {
            if (animator.GetBool("SawEnemy") != changeAnimation)
                SetAnimatorSawPlayer(changeAnimation);

            if (animator.GetBool("IsCasting") != changeAnimation)
            {
                AnimatorEnterCastMode(changeAnimation);
            }

            if (animator.GetBool("IsCasting"))
            {
                AnimatorCastSpell();
            }
        }
    }

    /// <summary>
    /// change saw enemy property in animator
    /// </summary>
    /// <param name="isSaw"></param>
    void SetAnimatorSawPlayer(bool isSaw)
    {
        animator.SetBool("SawEnemy", isSaw);
    }

    /// <summary>
    /// change isCasting and enterCasting propertys in animator
    /// </summary>
    /// <param name="isToCast"></param>
    void AnimatorEnterCastMode(bool isToCast)
    {
        animator.SetBool("IsCasting", isToCast);

        if (isToCast)
        {
            if (!isCastingAlready)
            {
                animator.SetTrigger("EnterCasting");
                isCastingAlready = true;
            }
        }
        else
            isCastingAlready = false;
    }

    /// <summary>
    /// when mage is in casting spell mode, set enemy distance of the mage and start casting spell animation
    /// </summary>
    void AnimatorCastSpell()
    {
        if (!isCastCooldown)
        {
            if (CheckSpellSlots(spellSlots) >= 0)
            {
                float distanceToPlayer = (playerTransform.position - transform.position).magnitude;
                //Debug.Log("Player Distance: " + distanceToPlayer);

                // if (distanceToPlayer >= 10)
                // {
                //     animator.SetFloat("RandomSpell", Random.Range(0, 3));
                // }
                // else
                animator.SetFloat("EnemyDistance", 5);

                animator.SetTrigger("CastSpell");

                StartCoroutine(WaitBeforeCastAgain());
            }
        }

    }

    IEnumerator WaitBeforeCastAgain()
    {
        isCastCooldown = true;

        yield return new WaitForSeconds(spellCoolDown);

        isCastCooldown = false;
    }

    /// <summary>
    /// This method cast the spell through the event in animation of casting, when spellSlot is empty.
    /// the ideia is to constant cast spells when the previous spell effect is over, randomly, depending on know spells
    /// </summary>
    /// <param name="spell">prefab of the spell you want to cast</param>
    // public void CastSpell(GameObject spell)
    // {
    //     if (playerTransform != null && spellSlot == null)
    //         spellSlot = Instantiate(spell, playerTransform.position, spell.transform.rotation);
    // }


    /// <summary>
    /// This method cast the spell through the event in animation of casting, when spellSlot is empty.
    /// the ideia is to constant cast spells when the previous spell effect is over, randomly, depending on know spells
    /// </summary>
    /// <param name="spell">prefab of the spell you want to cast</param>
    public void CastSpell(GameObject spell)
    {
        if (playerTransform != null)
        {
            if (CheckSpellSlots(spellSlots) >= 0)
            {
                spellSlots[CheckSpellSlots(spellSlots)] = Instantiate(spell, playerTransform.position, spell.transform.rotation);
            }
        }
    }

    /// <summary>
    /// if find a empty space in spellSlots return its index, else return -1
    /// </summary>
    /// <param name="mySpellSlotList">list of spellSlot</param>
    /// <returns> index of null slot or -1 if all full</returns>
    public int CheckSpellSlots(List<GameObject> mySpellSlotList)
    {
        for (int i = 0; i < mySpellSlotList.Count; i++)
        {
            if (mySpellSlotList[i] == null)
            {
                return i;
                break;
            }
        }
        return -1;
    }
}
