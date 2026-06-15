using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Awake()
    {
        offset = new Vector3(0, 10, -10);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindPlayer();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
            PositionCamera();
        else
            FindPlayer();
    }

    void FindPlayer()
    {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player").gameObject;
        }
    }

    void PositionCamera()
    {

        transform.position = player.transform.position + offset;
    }
}
