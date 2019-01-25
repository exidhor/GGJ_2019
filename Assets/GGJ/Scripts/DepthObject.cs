using UnityEngine;
using System.Collections;

public class DepthObject : MonoBehaviour
{
    protected virtual void Start()
    {
        DepthManager.instance.Register(this);
    }

    protected virtual void OnDestroy()
    {
        if(DepthManager.internalInstance != null)
        {
            DepthManager.instance.Unregister(this);
        }
    }
}
