using UnityEngine;
using System.Collections.Generic;
using Tools;

public class ContainerManager : MonoSingleton<ContainerManager>
{
    List<Container> _containers = new List<Container>();

    public void Register(Container container)
    {
        _containers.Add(container);
    }

    public void Unregister(Container container)
    {
        _containers.Remove(container);
    }

    public void Actualize()
    {
        if(DraggableManager.instance.isDragging)
        {
            Vector2 wpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Container container = FindContainer(wpos);

            if(container != null)
            {
                if(container.shape == DraggableManager.instance.current.shape)
                {
                    // todo : halo
                }
                else 
                {
                    // todo : halo
                }
            }
        }
    }

    Container FindContainer(Vector2 wpos)
    {
        float bestDist = float.MaxValue;
        Container best = null;

        for (int i = 0; i < _containers.Count; i++)
        {
            Rect rect = _containers[i].GetCollider();

            if (rect.Contains(wpos))
            {
                float dist = Vector2.Distance(rect.center, wpos);

                if (dist < bestDist)
                {
                    best = _containers[i];
                    bestDist = dist;
                }
            }
        }

        return best;
    }
}
