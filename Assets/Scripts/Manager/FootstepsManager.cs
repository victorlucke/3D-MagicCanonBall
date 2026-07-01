using System.Collections.Generic;
using UnityEngine;

public class FootstepsManager : BasicFunctionalities
{
    public List<AudioClip> sandSteps = new List<AudioClip>();
    public List<AudioClip> stoneSteps = new List<AudioClip>();
    public List<AudioClip> woodSteps = new List<AudioClip>();

    private enum Surface { sand, stone, wood };
    private Surface surface;

    private void SelectStepList()
    {
        switch (surface)
        {
            case Surface.sand:
                audioEffect = sandSteps;
                break;
            case Surface.stone:
                audioEffect = stoneSteps;
                break;
            case Surface.wood:
                audioEffect = woodSteps;
                break;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Sand")
        {
            surface = Surface.sand;
        }

        if (hit.transform.tag == "Stone")
        {
            surface = Surface.stone;
        }

        if (hit.transform.tag == "Wood")
        {
            surface = Surface.wood;
        }

        SelectStepList();

    }

    void FixedUpdate()
    {
        DetectFloorType();
    }

    void DetectFloorType()
    {
        RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 100))
        {
            if (hit.collider.gameObject.layer == 6)
                surface = Surface.sand;

            if (hit.collider.gameObject.layer == 7)
                surface = Surface.stone;

            if (hit.collider.gameObject.layer == 8)
                surface = Surface.wood;

            SelectStepList();
        }
    }
}
