using UnityEngine;
using System.Collections;
using Tools;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] float _boundingRatio;
    [SerializeField] Vector2 _limits;
    [SerializeField] float _speed;

    Rect _cameraRect;
    Rect _worldBounds;

    Vector2 xRange;
    Vector2 yRange;

    void Awake()
    {
        _cameraRect = GetRect();

        _worldBounds = new Rect(_limits / -2, _limits);

        Vector2 cmin = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 cmax = Camera.main.ViewportToWorldPoint(Vector2.one);

        Vector2 size = cmax - cmin;
        Vector2 extend = size / 2;

        xRange.x = -_limits.x / 2 + extend.x;
        xRange.y = _limits.x / 2 - extend.x;

        yRange.x = -_limits.y / 2 + extend.y;
        yRange.y = _limits.y / 2 - extend.y;
    }

    Rect GetRect()
    {
        float s = _boundingRatio / 2;
        float d = (0.5f - s);
        Vector2 pos = new Vector2(d, d);
        Vector2 size = new Vector2(_boundingRatio, _boundingRatio);
        return new Rect(pos, size);
    }

    public void Actualize()
    {
        Vector2 vwpos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if(!_cameraRect.Contains(vwpos))
        {
            Vector2 closest = OldMathHelper.ClosestPointToRect(_cameraRect, vwpos);
            Vector2 wposClosest = Camera.main.ViewportToWorldPoint(closest);
            Vector2 wposMouse = Camera.main.ViewportToWorldPoint(vwpos);
            Vector2 move = wposMouse - wposClosest;

            Vector2 nmove = move.normalized;
            nmove *= _speed * Time.deltaTime;

            if(nmove.sqrMagnitude < move.sqrMagnitude)
            {
                move = nmove;
            }

            Vector3 cameraPos = Camera.main.transform.position;
            cameraPos.x += move.x;
            cameraPos.y += move.y;

            cameraPos.x = Mathf.Clamp(cameraPos.x, xRange.x, xRange.y);
            cameraPos.y = Mathf.Clamp(cameraPos.y, yRange.x, yRange.y);

            Camera.main.transform.position = cameraPos;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Rect rect = GetRect();

        Vector2 wmin = Camera.main.ViewportToWorldPoint(rect.min);
        Vector2 wmax = Camera.main.ViewportToWorldPoint(rect.max);

        Vector2 size = wmax - wmin;

        Gizmos.DrawWireCube(wmin + size / 2, size);

        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(Vector2.zero, _limits);
    }
}
