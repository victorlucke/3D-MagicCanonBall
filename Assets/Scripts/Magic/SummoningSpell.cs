using System.Collections;
using UnityEngine;

public class SummoningSpell : BasicFunctionalities
{
    public bool auraFinished;
    public GameObject spellEffectObject;
    public GameObject monsterObject;
    public float spellDuration;
    public Vector3 spawnPosition;
    private bool isSpellOn;

    void Awake()
    {
        spellDuration = 10;
        isSpellOn = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(auraFinished && !isSpellOn)
        {
            PlaySoundEffect();
            StartCoroutine(SpellLifeTime(spawnPosition));
            isSpellOn = true;
        }
    }

    private IEnumerator SpellLifeTime(Vector3 newSpawnPosition)
    {

        GameObject newFinishEffect = Instantiate(spellEffectObject, newSpawnPosition, spellEffectObject.transform.rotation);

        GameObject newMonster = Instantiate(monsterObject, newSpawnPosition, monsterObject.transform.rotation);
        
        yield return new WaitForSeconds(spellDuration);

        Destroy(newFinishEffect);
        Destroy(newMonster);
        Destroy(this.gameObject);
    }
}
