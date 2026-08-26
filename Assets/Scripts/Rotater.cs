using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float XRotate;
    public float YRotate;
    public float ZRotate;

    void Awake()
    {
        
    }

    /// <summary>
    /// Rotate gameobject over time (Default data 15, 13 45)
    /// </summary>
    void Rotation()
    {
        transform.Rotate(new Vector3(XRotate, YRotate, ZRotate) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
}
