    using System.Collections;
using UnityEngine;

public class SummoningSpell : BasicFunctionalities
{
    public GameObject spellEffectObject;
    public GameObject monsterObject;
    public float spellDuration;
    public Vector3 spawnPosition { get; set; } 
    public bool auraFinished { get; set; }
    private bool isSpellOn;

    void Awake()
    {
        isSpellOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(auraFinished && !isSpellOn)
        {
            PlaySoundEffect(audioEffect);
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
