using UnityEngine;
using System.Collections;
using Tools;

public class BumpManager : MonoSingleton<BumpManager>
{
    [SerializeField] Vector2 _strengthRange;
    [SerializeField] AnimationCurve _curveX;
    [SerializeField] AnimationCurve _curveY;
    [SerializeField] float _duration;
    [SerializeField] float _scaleY;

    public float GetStrength()
    {
        return Random.Range(_strengthRange.x, _strengthRange.y);
    }

    public Vector2 Anim(out bool isFinish, float startTime, float strength)
    {
        float nt = (Time.time - startTime) / _duration;

        if(nt > 1)
        {
            nt = 1;
            isFinish = true;
        }
        else
        {
            isFinish = false;
        }

        float ctx = _curveX.Evaluate(nt);
        float cty = _curveY.Evaluate(nt);

        float x = Mathf.LerpUnclamped(0, strength, ctx);
        float y = Mathf.LerpUnclamped(0, Mathf.Abs(strength * _scaleY), cty);

        return new Vector2(x, y);
    }
}
