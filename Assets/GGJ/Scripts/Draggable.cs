using UnityEngine;
using System.Collections;

public class Draggable : DepthObject
{
    public Shape shape
    {
        get { return _shape; }
    }

    public Vector2 center
    {
        get { return GetCollider().center; }
    }

    public bool isHide
    {
        get { return _isHide; }
    }

    [SerializeField] Shape _shape;
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;

    float _startTimeDrag = -100f;
    bool _isDragging;

    float _startTimeBump = -100f;
    float _bumpingStrength;
    bool _isBumping;
    Vector2 _originBump;

    bool _isHide;

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

    public void SetHide(bool hide)
    {
        _isHide = hide;
    }

    protected override void Start()
    {
        base.Start();

        DraggableManager.instance.Register(this);
    }

    public void StartDrag()
    {
        _startTimeDrag = Time.time;
        _isDragging = true;
        _isBumping = false;

        DepthManager.instance.Unregister(this);
    }

    public void StopDrag()
    {
        _startTimeDrag = Time.time;
        _isDragging = false;

        DepthManager.instance.Register(this);
    }

    void LateUpdate()
    {
        float scale = 0;

        if (_isDragging)
        {
            scale = DraggableManager.instance.GetCatchScale(_startTimeDrag);
        }
        else
        {
            scale = DraggableManager.instance.GetReleaseScale(_startTimeDrag);
        }

        Vector3 vscale = Vector3.one * scale;
        vscale.x *= Mathf.Sign(transform.localScale.x);
        vscale.z = 1;

        transform.localScale = vscale;

        if(_isBumping)
        {
            bool isFinish;
            Vector2 move = BumpManager.instance.Anim(out isFinish, _startTimeBump, _bumpingStrength);
        
            if(isFinish)
            {
                _isBumping = false;
            }

            Vector3 pos = transform.position;
            pos.x = _originBump.x + move.x;
            pos.y = _originBump.y + move.y;
            transform.position = pos;
        }
    }

    public void Bumb()
    {
        _isHide = false;
        _isBumping = true;
        _startTimeBump = Time.time;
        _bumpingStrength = BumpManager.instance.GetStrength();
        _bumpingStrength *= Mathf.Sign(Random.value - 0.5f);
        _originBump = transform.position;
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
