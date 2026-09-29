using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpawnFurniture : MagicFurniture
{
    [Header("MagicSpawnFurniture Class")]
    [SerializeField] private List<GameObject> booksAvailable;
    [SerializeField] private int timeToReuseFurniture;
    private GameObject objectOnScene;

    void Update()
    {
        if (isActivated)
        {
            MagicAuraActivation("Magic", false);
            if (!objectOnScene)
                ReactivateFurniture(timeToReuseFurniture);
        }
        else
            MagicAuraActivation("Magic", true);
    }

    protected override IEnumerator ActivateAfterTime(float waitTime)
    {
        yield return base.ActivateAfterTime(waitTime);

        SummonMagicBook();
        yield return null;
    }

    /// <summary>
    /// summon a random book inside shelve
    /// </summary>
    void SummonMagicBook()
    {
        if (isActivated)
        {
            int amountOfBooks = booksAvailable.Count;
            int randomBook = Random.Range(0, amountOfBooks);

            if (amountOfBooks > 0)
            {
                GameObject bookSummoned = booksAvailable[randomBook];

                if (bookSummoned && !objectOnScene)
                    objectOnScene = Instantiate(bookSummoned, transform.position, bookSummoned.transform.rotation);
            }
        }
    }
}
