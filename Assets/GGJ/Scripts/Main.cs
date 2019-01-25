using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
	void Update () 
    {
        DraggableManager.instance.Actualize();
        DepthManager.instance.Actualize();
	}
}
