using UnityEngine;

public class MagicBook : BasicFunctionalities
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlaySoundEffect(audioEffect);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
