using System.Collections;
using System.IO.IsolatedStorage;
using UnityEngine;

public class Mage : EnemyMovement
{
    public GameObject knowingSpell;
    public GameObject spellSlot;
    private bool isCastingAlready;
    private bool isHurt;
    public bool isCastCooldown;
    int i;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").gameObject.transform;
        animator = GetComponent<Animator>();
        isCastingAlready = false;
        sawEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookPlayer();
        SeeingPlayer();
    }

    void LookPlayer()
    {
        if (playerTransform == null)
            if (GameObject.FindWithTag("Player") != null)
                playerTransform = GameObject.FindWithTag("Player").gameObject.transform;
    }

    void SeeingPlayer()
    {
        if (playerTransform != null)
            sawEnemy = true;

        else if (playerTransform == null)
            sawEnemy = false;

        StartAnimations(sawEnemy);
    }

    void StartAnimations(bool isToStart)
    {
        bool changeAnimation = isToStart;

        if (animator != null)
        {
            if (animator.GetBool("SawPlayer") != changeAnimation)
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

    void SetAnimatorSawPlayer(bool isSaw)
    {
        animator.SetBool("SawPlayer", isSaw);
    }

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

    void AnimatorCastSpell()
    {
        if (!isCastCooldown)
        {
            if (spellSlot == null)
            {
                float distanceToPlayer = (playerTransform.position - transform.position).magnitude;
                //Debug.Log("Player Distance: " + distanceToPlayer);

                if (distanceToPlayer >= 10)
                {
                    animator.SetFloat("RandomSpell", Random.Range(0, 3));
                }
                else
                    animator.SetFloat("EnemyDistance", 5);

                animator.SetTrigger("CastSpell");
                Debug.Log(i++);

                StartCoroutine(WaitBeforeCastAgain());
            }
        }

    }

    IEnumerator WaitBeforeCastAgain()
    {
        isCastCooldown = true;

        yield return new WaitForSeconds(1);

        isCastCooldown = false;
    }

    public void CastSpell(GameObject spell)
    {
        if (playerTransform != null && spellSlot == null)
            spellSlot = Instantiate(spell, playerTransform.position, spell.transform.rotation);
    }
}
