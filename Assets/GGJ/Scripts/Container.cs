using UnityEngine;
using System.Collections;

public class Container : MonoBehaviour
{
    public Shape shape
    {
        get { return _shape; }
    }

    [SerializeField] Shape _shape;
    [SerializeField] Vector2 _centerCollider;
    [SerializeField] Vector2 _sizeCollider = Vector2.one;
    [SerializeField] float _angle;

    void Start()
    {
        ContainerManager.instance.Register(this);
    }

    void OnDestroy()
    {
        if(ContainerManager.internalInstance != null)
        {
            ContainerManager.instance.Unregister(this);
        }
    }

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Rect rect = GetCollider();
        Gizmos.DrawWireCube(rect.center, rect.size);

        Gizmos.color = Color.red;

        Vector2 pos = transform.position;
        Vector2 direction = Tools.OldMathHelper.GetDirectionFromAngle(_angle * Mathf.Deg2Rad);

        Gizmos.DrawLine(pos, pos + direction.normalized * 3);
    }
}
