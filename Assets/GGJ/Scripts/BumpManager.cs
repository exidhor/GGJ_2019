using UnityEngine;
using System.Collections.Generic;
using Tools;
using System;

public class BumpManager : MonoSingleton<BumpManager>
{
    [SerializeField] Vector2 _strengthRange;
    [SerializeField] AnimationCurve _curveX;
    [SerializeField] AnimationCurve _curveY;
    [SerializeField] float _duration;
    [SerializeField] float _scaleY;

    [Serializable]
    class BumpData
    {
        public string name;
        public Vector2 _strengthRange;
        public AnimationCurve _curveX;
        public AnimationCurve _curveY;
        public float _duration;
        public float _scaleY;
        public Vector2 _delay;
    }

    [SerializeField] List<BumpData> _datas = new List<BumpData>();

    public float GetStrength(string name)
    {
        BumpData bd = null;

        for (int i = 0; i < _datas.Count; i++)
        {
            if (_datas[i].name == name)
            {
                bd = _datas[i];
            }
        }

        return UnityEngine.Random.Range(bd._strengthRange.x, bd._strengthRange.y);
    }

    public float GetDelay(string name)
    {
        BumpData bd = null;

        for (int i = 0; i < _datas.Count; i++)
        {
            if (_datas[i].name.Equals(name))
            {
                bd = _datas[i];
            }
        }

        return UnityEngine.Random.Range(bd._delay.x, bd._delay.y);
    }

    public Vector2 Anim(out bool isFinish, float startTime, float strength, float delay, string name)
    {
        BumpData bd = null;

        for (int i = 0; i < _datas.Count; i++)
        {
            if(_datas[i].name.Equals(name))
            {
                bd = _datas[i];
            }
        }

        float dt = Time.time - startTime;

        if(dt < delay)
        {
            isFinish = false;
            return Vector2.zero;
        }

        dt -= delay;

        float nt = dt / bd._duration;

        if(nt > 1)
        {
            nt = 1;
            isFinish = true;
        }
        else
        {
            isFinish = false;
        }

        float ctx = bd._curveX.Evaluate(nt);
        float cty = bd._curveY.Evaluate(nt);

        float x = Mathf.LerpUnclamped(0, strength, ctx);
        float y = Mathf.LerpUnclamped(0, Mathf.Abs(strength * bd._scaleY), cty);

        return new Vector2(x, y);
    }
}
