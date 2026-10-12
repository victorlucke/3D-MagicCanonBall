using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public bool isProjectile { get { return _isProjectile; } set { _isProjectile = value; } }
    protected bool _isProjectile;
    private String nameProjectileLayer;
    private LayerMask projectileLayerIndex;
    private LayerMask defaultLayer;
    private Coroutine StartOnceResetChangesTimer;

    void OnEnable()
    {
        GameEvents.OnCannonFired += ChangeToProjectileLayer;
    }

    void OnDisable()
    {
        GameEvents.OnCannonFired -= ChangeToProjectileLayer;
    }

    void Awake()
    {
        nameProjectileLayer = "Projectile";
        defaultLayer = gameObject.layer;
        projectileLayerIndex = LayerMask.NameToLayer(nameProjectileLayer);
        if (LayerMask.LayerToName(projectileLayerIndex.value) != nameProjectileLayer)
            Debug.Log(gameObject.name + " Cant find Projectile layer");
    }

    void ChangeToProjectileLayer(GameObject objectFired)
    {
        if (objectFired == gameObject)
        {
            gameObject.layer = projectileLayerIndex;
            _isProjectile = true;
            StartOnceResetChangesTimer = StartCoroutine(ResetChangesTimer(5));
        }
    }

    void ChangeToDefaultLayer()
    {
        if (_isProjectile)
        {
            gameObject.layer = defaultLayer;
            
            if (StartOnceResetChangesTimer != null)
            {
                StopCoroutine(StartOnceResetChangesTimer);
                StartOnceResetChangesTimer = null;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ChangeToDefaultLayer();
        }
    }

    IEnumerator ResetChangesTimer(float time)
    {
        yield return new WaitForSeconds(time);

        ChangeToDefaultLayer();
    }
}
