using UnityEngine;
using System.Collections.Generic;
using Tools;

public class DepthManager : MonoSingleton<DepthManager>
{
    [SerializeField] float _startDepth;
    [SerializeField] float _stepDepth;

    List<DepthObject> _objects = new List<DepthObject>();

    public void Register(DepthObject depthObject)
    {
        _objects.Add(depthObject);
    }

    public void Unregister(DepthObject depthObject)
    {
        _objects.Remove(depthObject);
    }

    // Update is called once per frame
    public void Actualize()
    {
        _objects.Sort(Compare);

        for (int i = 0; i < _objects.Count; i++)
        {
            Vector3 pos = _objects[i].transform.position;
            pos.z = _startDepth + _stepDepth * i;
            _objects[i].transform.position = pos;
        }
    }

    int Compare(DepthObject d0, DepthObject d1)
    {
        float d0x = d0.transform.position.x;
        float d0y = d0.transform.position.y;

        float d1x = d1.transform.position.x;
        float d1y = d1.transform.position.y;

        if(d0y != d1y)
        {
            return d0y.CompareTo(d1y);
        }

        return d0x.CompareTo(d1x);
    }
}
