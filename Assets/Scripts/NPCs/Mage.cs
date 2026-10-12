using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : NavMeshMovement
{
    [Header("MAGE CLASS")]
    [Header("Mage Controller")]
    private static int timesBeingHit;
    //public List<GameObject> knowingSpellTest;
    public List<GameObject> spellSlots;
    public int spellCoolDown;
    private bool isCastCooldown;
    private bool isCastingAlready;
    private bool isHurt;

    void OnEnable()
    {
        GameEvents.OnHitTarget += CheckBeingHit;
    }

    void OnDisable()
    {
        GameEvents.OnHitTarget -= CheckBeingHit;
    }

    protected override void Awake()
    {
        base.Awake();
        timesBeingHit = 0;
        isCastingAlready = false;
        //sawEnemy = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        ResetHurtState();
        LookAtTarget();
        
        if (currentTargetDetected)
            TryCastSpellsOnTarget(currentTargetDetected.transform);
    }

    void ResetHurtState()
    {
        if (GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.Over)
            timesBeingHit = 0;
    }

    /// <summary>
    /// This method is to be used when action event hit is called. It simulates damage on this prefab
    /// </summary>
    /// <param name="hitObject"></param>
    void CheckBeingHit(GameObject hitObject)
    {
        if (hitObject == gameObject)
        {
            AnimatorTakingHit();
            timesBeingHit++;
            Hurted();
        }
    }

    /// <summary>
    /// Check if is hit too many times, to set hurt state, where character cant make any action
    /// </summary>
    void Hurted()
    {
        if (timesBeingHit >= 3)
        {
            isHurt = true;
            AnimatorHurted(isHurt);
        }
    }

    /// <summary>
    /// If target inst hurt prepare his magic slots to cast an know spells through animations
    /// </summary>
    void TryCastSpellsOnTarget(Transform myTargetLocation)
    {
        AnimatorHurted(isHurt);

        if (!isHurt && myTargetLocation)
        {
            UpdateSpellSlotRange((int)GameManager.Instance.DificultyMultiplayer());
            ChangeAnimationSpellsOnTarget(sawEnemy, myTargetLocation);
        }
        else if (myTargetLocation)
        {
            ChangeAnimationSpellsOnTarget(false, myTargetLocation);
        }
        else Debug.Log("No target in " + gameObject.name);

    }

    /// <summary>
    /// change the amount of spells that can be casted
    /// </summary>
    /// <param name="newRange">new amount of spells</param>
    void UpdateSpellSlotRange(int newRange)
    {
        if (spellSlots.Count < newRange)
        {
            for (int i = spellSlots.Count; i < newRange; i++)
            {
                spellSlots.Add(null);
            }
        }
        else if (newRange < spellSlots.Count)
        {
            for (int i = newRange; i < spellSlots.Count; i++)
            {
                spellSlots.RemoveAt(spellSlots.Count - 1);
            }
        }
    }

    /// <summary>
    /// control mage animations
    /// </summary>
    /// <param name="isToStart">if true, activate animations saw enemy and is casting</param>
    void ChangeAnimationSpellsOnTarget(bool isToStart, Transform myTargetLocation)
    {
        bool changeAnimation = isToStart;

        if (animator != null)
        {
            if (animator.GetBool("SawEnemy") != changeAnimation)
                AnimatorSawPlayer(changeAnimation);

            if (animator.GetBool("IsCasting") != changeAnimation)
                AnimatorEnterCastMode(changeAnimation);

            if (animator.GetBool("IsCasting"))
                AnimatorCastSpellOnTarget(myTargetLocation);
        }
    }

    /// <summary>
    /// change saw enemy property in animator
    /// </summary>
    /// <param name="isthisSaw"></param>
    void AnimatorSawPlayer(bool isthisSaw)
    {
        animator.SetBool("SawEnemy", isthisSaw);
    }

    /// <summary>
    /// change isCasting and enterCasting propertys in animator
    /// </summary>
    /// <param name="isToThisCast"></param>
    void AnimatorEnterCastMode(bool isToThisCast)
    {
        animator.SetBool("IsCasting", isToThisCast);

        if (isToThisCast)
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
    void AnimatorCastSpellOnTarget(Transform myTargetLocation)
    {
        if (!isCastCooldown)
        {
            if (CheckSpellSlots(spellSlots) >= 0)
            {
                float distanceToPlayer = (myTargetLocation.position - transform.position).magnitude;
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

    /// <summary>
    /// Simulate damage activating the animation when tanking hit
    /// </summary>
    void AnimatorTakingHit()
    {
        animator.SetTrigger("TakedDamage");
        AnimatorEnterCastMode(false);
    }

    void AnimatorHurted(bool isThisHurt)
    {
        animator.SetBool("IsDamaged", isThisHurt);
    }

    /// <summary>
    /// Wait cooldown before allowing cast new spells
    /// </summary>
    /// <returns></returns>
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
        if (currentTargetDetected.transform != null)
        {
            if (CheckSpellSlots(spellSlots) >= 0)
            {
                spellSlots[CheckSpellSlots(spellSlots)] = Instantiate(spell, currentTargetDetected.transform.position, spell.transform.rotation);
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
            }
        }
        return -1;
    }
}
