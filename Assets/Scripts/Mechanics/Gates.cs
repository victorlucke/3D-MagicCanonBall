using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Gates : BasicFunctionalities
{
    [SerializeField] private Animator animator;
    [SerializeField] private float soundEffectPlayTime;
    [SerializeField] private GameObject myGateTriggerObject;
    Collider gateTriggerCollider;
    Vector3 gateTriggerObjectDefaultPosition;
    Coroutine PlaySoundCoroutineOnce;

    void Awake()
    {
        gateTriggerCollider = myGateTriggerObject.GetComponent<Collider>();

        gateTriggerObjectDefaultPosition = myGateTriggerObject.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            CallPressurePlateAnimation();
    }

    void Update()
    {
        if (myGateTriggerObject.transform.position != gateTriggerObjectDefaultPosition)
            gateTriggerCollider.enabled = false;
        else
            gateTriggerCollider.enabled = true;
    }

    void CallPressurePlateAnimation()
    {


        if (PlaySoundCoroutineOnce == null)
            PlaySoundCoroutineOnce = StartCoroutine(PlaySoundForSeconds(soundEffectPlayTime));

        if (animator != null)
        {
            animator.SetTrigger("PressPlate");
        }
    }

    private IEnumerator PlaySoundForSeconds(float mySeconds)
    {
        yield return new WaitForSeconds(1.10f);

        AudioSource myAudioSource;
        myAudioSource = GetComponent<AudioSource>();

        myAudioSource.volume = 1;

        PlayLoopSoundEffect(gameObject, audioEffect[0]);

        yield return new WaitForSeconds(mySeconds);

        myAudioSource.volume = 0;

        yield return new WaitForSeconds(0.1f);

        StopSoundEffect(myAudioSource);
        PlaySoundCoroutineOnce = null;
    }
}
