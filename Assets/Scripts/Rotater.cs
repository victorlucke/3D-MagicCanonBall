using UnityEngine;

public class Rotater : MonoBehaviour
{
    void Rotation()
    {
        transform.Rotate(new Vector3 (15, 13, 45) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
}
