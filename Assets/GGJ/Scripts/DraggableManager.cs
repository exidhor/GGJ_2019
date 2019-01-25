using UnityEngine;
using System.Collections.Generic;
using Tools;

public class DraggableManager : MonoSingleton<DraggableManager>
{
    [SerializeField] float _startScale;
    [SerializeField] float _endScale;
    [SerializeField] float _catchDuration;
    [SerializeField] float _releaseDuration;
    [SerializeField] AnimationCurve _catchCurve;
    [SerializeField] AnimationCurve _releaseCurve;

    Draggable _dragging = null;
    Vector2 _offset;

    List<Draggable> _draggables = new List<Draggable>();

    public void Register(Draggable d)
    {
        _draggables.Add(d);
    }

    public void Unregister(Draggable d)
    {
        _draggables.Remove(d);
    }

    public float GetCatchScale(float startTime)
    {
        float nt = (Time.time - startTime) / _catchDuration;
        float ct = _catchCurve.Evaluate(nt);

        return Mathf.LerpUnclamped(_startScale, _endScale, ct);
    }

    public float GetReleaseScale(float startTime)
    {
        float nt = (Time.time - startTime) / _releaseDuration;
        float ct = _releaseCurve.Evaluate(nt);

        return Mathf.LerpUnclamped(_startScale, _endScale, ct);
    }

    // Update is called once per frame
    public void Actualize()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 wpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _dragging = FindDragging(wpos);

            if (_dragging != null)
            {
                _dragging.StartDrag();
                _offset = (Vector2)_dragging.transform.position - wpos;
            }

        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(_dragging != null)
            {
                _dragging.StopDrag();
                _dragging = null;
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if(_dragging != null)
            {
                Vector2 wpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector2 pos = wpos + _offset;
                _dragging.transform.position = pos;
            }
        }
    }

    Draggable FindDragging(Vector2 wpos)
    {
        float bestDist = float.MaxValue;
        Draggable best = null;

        for (int i = 0; i < _draggables.Count; i++)
        {
            Rect rect = _draggables[i].GetCollider();

            if(rect.Contains(wpos))
            {
                float dist = Vector2.Distance(rect.center, wpos);

                if(dist < bestDist)
                {
                    best = _draggables[i];
                    bestDist = dist;
                }
            }
        }

        return best;
    }
}
