using UnityEngine;
using System.Collections.Generic;
using System;

public class Anim : MonoBehaviour
{
    [Serializable]
    class AnimData
    {
        [SerializeField] float _duration;

        [Header("Rotation")]
        [SerializeField] float _startRotateZ;
        [SerializeField] float _endRotateZ;
        [SerializeField] AnimationCurve _rotateCurve;

        [Header("Translation X")]
        [SerializeField] float _startX;
        [SerializeField] float _endX;
        [SerializeField] AnimationCurve _curveX;

        [Header("Translation Y")]
        [SerializeField] float _startY;
        [SerializeField] float _endY;
        [SerializeField] AnimationCurve _curveY;

        public float offsetTime
        {
            get { return _time - _duration; }
        }

        float _time;

        public void Init(float startTime)
        {
            _time = startTime;
        }


        public bool Actualize(Transform transform)
        {
            _time += Time.deltaTime;

            float nt = _time / _duration;

            if(nt > 1)
            {
                nt = 1;
            }

            float ctz = _rotateCurve.Evaluate(nt);
            float z = Mathf.LerpAngle(_startRotateZ, _endRotateZ, ctz);

            transform.rotation = Quaternion.Euler(0, 0, z);

            float ctx = _curveX.Evaluate(nt);
            float x = Mathf.LerpUnclamped(_startX, _endX, ctx);

            float cty = _curveY.Evaluate(nt);
            float y = Mathf.LerpUnclamped(_startY, _endY, cty);

            Vector3 pos = transform.position;
            pos.x = x;
            pos.y = y;

            transform.position = pos;

            return nt < 1;
        }
    }

    [SerializeField] List<AnimData> _anims = new List<AnimData>();

    int _currentIndex = 0;

    public void Init()
    {
        _anims[_currentIndex].Init(0f);
        _anims[_currentIndex].Actualize(transform);
    }

    void LateUpdate()
    {
        bool isFinish = _anims[_currentIndex].Actualize(transform);

        if (isFinish)
        {
            AnimData previous = _anims[_currentIndex];

            _currentIndex++;

            if(_currentIndex >= _anims.Count)
            {
                _currentIndex = 0;
            }

            _anims[_currentIndex].Init(previous.offsetTime);
        }
    }
}
