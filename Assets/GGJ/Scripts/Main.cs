using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        // INIT GLOBAL

        HidderManager.instance.Spawn();
    }

    void Update () 
    {
        DraggableManager.instance.Actualize();
        ContainerManager.instance.Actualize();
        HidderManager.instance.Actualize();
        DepthManager.instance.Actualize();
        CameraManager.instance.Actualize();
        End.instance.Actualize();
	}
}
