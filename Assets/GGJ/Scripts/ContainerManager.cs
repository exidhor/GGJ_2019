﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Tools;

public class ContainerManager : MonoSingleton<ContainerManager>
{
    [SerializeField] Text _chaos;
    [SerializeField] Text _score;

    [SerializeField] string _defaultEndMessage = "R.I.P. but this is not complete ...";

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
                if(container.containing == null && container.shape == DraggableManager.instance.current.shape)
                {
                    Draggable drag = DraggableManager.instance.current;
                    Vector3 dragPos = drag.transform.position;
                    dragPos.x = container.transform.position.x;
                    dragPos.y = container.transform.position.y;
                    drag.transform.position = dragPos;

                    Vector3 scale = drag.transform.localScale;
                    scale.x = Mathf.Abs(scale.x) * Mathf.Sign(container.transform.localScale.x);
                    drag.transform.localScale = scale;
                }
                else
                {
                    // todo : halo
                }
            }
        }
    }

    void RecomputeTexts()
    {
        int score = 0;
        int chaos = 0;

        for (int i = 0; i < _containers.Count; i++)
        {
            score += _containers[i].GetScore();
            chaos += _containers[i].GetChaos();
        }

        _score.text = "Score : " + score;
        _chaos.text = "Chaos : " + chaos;
    }

    public void ReleaseDrag(Draggable drag)
    {
        Container c = FindContainer(drag.center);

        if (c == null)
        {
            return;
        }

        if(c.containing == null && c.shape == DraggableManager.instance.current.shape)
        {
            c.Fill(drag);
            RecomputeTexts();
        }
        else
        {
            drag.Bumb("bush");
        }
    }

    public void OnCatch(Draggable drag)
    {
        for (int i = 0; i < _containers.Count; i++)
        {
            _containers[i].TryToRelease(drag);
        }

        RecomputeTexts();
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

    public bool CanFinish()
    {
        for (int i = 0; i < _containers.Count; i++)
        {
            if(_containers[i].containing == null)
            {
                return false;
            }
        }

        return true;
    }

    public string GetMessage()
    {
        bool isFullPanoply = true;

        PanoplyType type = _containers[0].containing.type;

        for (int i = 1; i < _containers.Count; i++)
        {
            if (type != _containers[i].containing.type)
            {
                isFullPanoply = false;
                break;
            }
        }

        if(isFullPanoply)
        {
            return PanoplyManager.instance.Get(type).endMessage;
        }

        return _defaultEndMessage;
    }
}
