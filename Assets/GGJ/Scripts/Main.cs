using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        // INIT GLOBAL

        if(!SceneManager.instance.hasLoad)
        {
            RatManager.instance.Init();
        }

        HidderManager.instance.Spawn();
    }

    void Update () 
    {
        bool animFi = SceneManager.instance.hasLoad || RatManager.instance.IsFinish();
        //if (animFi)
        //{
        //    RatManager.instance.Clear();
        //}
        if(!End.instance.isFinish)
        {
            DepthManager.instance.Actualize();
        }

        if(animFi && !End.instance.isFinish)
        {
            DraggableManager.instance.Actualize();
            ContainerManager.instance.Actualize();
            HidderManager.instance.Actualize();
            CameraManager.instance.Actualize();
        }


        End.instance.Actualize();
	}
}
