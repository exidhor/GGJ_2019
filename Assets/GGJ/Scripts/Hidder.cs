using UnityEngine;
using System.Collections.Generic;

public class Hidder : MonoBehaviour
{
    [Header("Infos")]
    [SerializeField] float _multiX = 1.1f;
    [SerializeField] float _multiY = 1.3f;
    [SerializeField] string _name;
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

    List<Draggable> _drags = new List<Draggable>();

    void OnEnable()
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
        Rect r = GetCollider(true);

        return r.Contains(drag.center);
    }

    public void SetHide(Draggable d)
    {
        _drags.Add(d);
    }

    public void SetDrag(Draggable d)
    {
        Rect r = GetCollider();

        float x = UnityEngine.Random.Range(r.xMin, r.xMax);
        float y = UnityEngine.Random.Range(r.yMin, r.yMax);

        Vector3 pos = d.transform.position;
        pos.x = x;
        pos.y = y;
        d.transform.position = pos;

        d.SetHide(true);
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
            _drags[i].Bumb(_name);
        }

        _drags.Clear();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);

        Gizmos.color = Color.red;
        rect = GetCollider(true);
        Gizmos.DrawWireCube(rect.center, rect.size);
    }

    public Rect GetCollider(bool mult = false)
    {
        Vector2 center = _centerCollider + (Vector2)transform.position;
        Vector2 size = _sizeCollider;
        //size.x *= transform.lossyScale.x;
        //size.y *= transform.lossyScale.y;

        return new Rect(center.x - size.x * (mult ? _multiX : 1f) / 2,
                        center.y - size.y / 2,
                        size.x * (mult ? _multiX : 1f),
                        size.y * (mult ? _multiY : 1f));
    }

}
