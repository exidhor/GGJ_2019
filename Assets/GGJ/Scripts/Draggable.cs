﻿using UnityEngine;
using System.Collections;

public class Draggable : DepthObject
{
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

    public Rect GetCollider()
    {
        Vector2 center = _centerCollider + (Vector2)transform.position;
        Vector2 size = _sizeCollider;
        size.x *= transform.lossyScale.x;
        size.y *= transform.lossyScale.y;

        return new Rect(center.x - size.x / 2, 
                        center.y - size.y / 2,
                        size.x, 
                        size.y);
    }

    protected override void Start()
    {
        base.Start();

        DraggableManager.instance.Register(this);
    }

    public void StartDrag()
    {
        // todo
    }

    public void StopDrag()
    {
        // todo
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        DraggableManager.instance.Unregister(this);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}
