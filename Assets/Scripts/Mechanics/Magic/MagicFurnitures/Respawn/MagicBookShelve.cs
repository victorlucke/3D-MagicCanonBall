using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBookShelve : MagicFurniture
{
    [Header("MagicBookShelve Class")]
    [SerializeField] private List<GameObject> booksAvailable;
    [SerializeField] private int timeToReuseFurniture;
    private GameObject bookOnScene;

    void Update()
    {
        if (isActivated)
        {
            MagicAuraActivation("Magic", false);
            if (!bookOnScene)
                ReactivateFurniture(timeToReuseFurniture);
        }
        else
            MagicAuraActivation("Magic", true);
    }

    protected override IEnumerator ActivateAfterTime(float waitTime)
    {
        yield return StartCoroutine(base.ActivateAfterTime(waitTime));

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

                if (bookSummoned && !bookOnScene)
                    bookOnScene = Instantiate(bookSummoned, transform.position, bookSummoned.transform.rotation);
            }
        }
    }
}
