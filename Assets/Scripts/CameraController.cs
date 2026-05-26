using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Awake()
    {
        player = GameObject.Find("Player").gameObject;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
        //Debug.Log("offset "+offset);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player != null)
            transform.position = player.transform.position + offset;
    }
}
