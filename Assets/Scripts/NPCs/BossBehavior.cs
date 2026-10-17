using Unity.VisualScripting;
using UnityEngine;

public class BossBehavior : SeeTarget
{
    [Header("BossBehavior Class")]
    private bool isCutSceneActive;
    [SerializeField] SeeTarget EyeScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        EyeScript.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckBossCutScene();
        CheckBossDefeated();
    }

    void CheckBossCutScene()
    {
        if(sawEnemy && !isCutSceneActive)
        {
            isCutSceneActive = true;
            BossBehavior thisScript;
            thisScript = gameObject.GetComponent<BossBehavior>();

            GameManager.Instance.ChangeGamePhase(GameManager.GamePhase.Boss);

            EyeScript.enabled = true;
        }
    }

    void CheckBossDefeated()
    {
        if (EyeScript.isUnhealth)
        {
            GameManager.Instance.ChangeGamePhase(GameManager.GamePhase.End);
        }
    }
}
