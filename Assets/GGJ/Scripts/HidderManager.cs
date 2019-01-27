using UnityEngine;
using System.Collections.Generic;
using Tools;

public class HidderManager : MonoSingleton<HidderManager>
{
    [SerializeField] int maxRange = 3;
    List<Hidder> _hidders = new List<Hidder>();

    public void CheckForHide(Draggable drag)
    {
        bool found = false;

        for (int i = 0; i < _hidders.Count; i++)
        {
            if(_hidders[i].CanHide(drag))
            {
                _hidders[i].SetHide(drag);
                found = true;
                break;
            }
        }

        drag.SetHide(found);
    }

    public void Spawn()
    {
        List<Hidder> spawnPoint = new List<Hidder>();

        for (int a = 0; a < maxRange; a++)
        {
            for (int i = 0; i < _hidders.Count; i++)
            {
                spawnPoint.Add(_hidders[i]);
            }
        }

        spawnPoint.Shuffle();

        int count = HiddenObjects.instance.count;
        for (int i = 0; i < count && i < spawnPoint.Count; i++)
        {
            Draggable d = HiddenObjects.instance.GetOne();
            spawnPoint[i].SetDrag(d);
        }
    }

    public void Register(Hidder hidder)
    {
        _hidders.Add(hidder);
    }

    public void Unregister(Hidder hidder)
    {
        _hidders.Remove(hidder);
    }

    public void Actualize()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 wpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Hidder h = FindHidder(wpos);

            if (h != null)
            {
                h.Bump();
            }

        }
    }

    Hidder FindHidder(Vector2 wpos)
    {
        float bestDist = float.MaxValue;
        Hidder best = null;

        for (int i = 0; i < _hidders.Count; i++)
        {
            Rect rect = _hidders[i].GetCollider();

            if (rect.Contains(wpos))
            {
                float dist = Vector2.Distance(rect.center, wpos);

                if (dist < bestDist)
                {
                    best = _hidders[i];
                    bestDist = dist;
                }
            }
        }

        return best;
    }
}
