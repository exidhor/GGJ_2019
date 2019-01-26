using UnityEngine;
using System.Collections;
using Tools;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] float _boundingRatio;
    [SerializeField] float _speed;

    Rect _rect;

    void Awake()
    {
        _rect = GetRect();
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

        if(!_rect.Contains(vwpos))
        {
            Vector2 closest = OldMathHelper.ClosestPointToRect(_rect, vwpos);
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
    }
}
