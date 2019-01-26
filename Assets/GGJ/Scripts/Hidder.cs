﻿using UnityEngine;
using System.Collections.Generic;

public class Hidder : MonoBehaviour
{
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

    List<Draggable> _drags = new List<Draggable>();

    void Start()
    {
        HidderManager.instance.Register(this);
    }

    void OnDestroy()
    {
        if(HidderManager.internalInstance != null)
        {
            HidderManager.instance.Unregister(this);
        }
    }

    public bool CanHide(Draggable drag)
    {
        Rect r = GetCollider();

        return r.Contains(drag.center);
    }

    public void SetHide(Draggable d)
    {
        _drags.Add(d);
    }

    public void StopHide(Draggable d)
    {
        _drags.Remove(d);
    }

    public void Bump()
    {
        for (int i = 0; i < _drags.Count; i++)
        {
            _drags[i].Bumb();
        }

        _drags.Clear();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);
    }

    public Rect GetCollider()
    {
        Vector2 center = _centerCollider + (Vector2)transform.position;
        Vector2 size = _sizeCollider;
        //size.x *= transform.lossyScale.x;
        //size.y *= transform.lossyScale.y;

        return new Rect(center.x - size.x / 2,
                        center.y - size.y / 2,
                        size.x,
                        size.y);
    }
}
