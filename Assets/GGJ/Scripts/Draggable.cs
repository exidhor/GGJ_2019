using UnityEngine;
using System.Collections;

public class Draggable : DepthObject
{
    public Shape shape
    {
        get { return _shape; }
    }

    [SerializeField] Shape _shape;
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

    float _startTime = -100f;
    bool _isDragging;

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
        _startTime = Time.time;
        _isDragging = true;

        DepthManager.instance.Unregister(this);
    }

    public void StopDrag()
    {
        _startTime = Time.time;
        _isDragging = false;

        DepthManager.instance.Register(this);
    }

    void LateUpdate()
    {
        float scale = 0;

        if (_isDragging)
        {
            scale = DraggableManager.instance.GetCatchScale(_startTime);
        }
        else
        {
            scale = DraggableManager.instance.GetReleaseScale(_startTime);
        }

        Vector3 vscale = Vector3.one * scale;
        vscale.z = 1;

        transform.localScale = vscale;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if(DraggableManager.internalInstance != null)
        {
            DraggableManager.instance.Unregister(this);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);
    }
}
