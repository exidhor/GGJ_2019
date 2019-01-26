using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeAnim : MonoBehaviour
{
    [SerializeField] float _durationIn;
    [SerializeField] float _durationOut;
    [SerializeField] CanvasGroup _image;

    bool _fadeIn;
    bool _fadeOut;

    float _startTime;

    void Awake()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        _fadeIn = true;
        _fadeOut = false;

        _image.alpha = 0;
        _startTime = Time.time;
    }

    public void FadeOut()
    {
        _fadeIn = false;
        _fadeOut = true;

        _image.alpha = 1;
        _startTime = Time.time;
    }

    void LateUpdate()
    {
        if(_fadeIn)
        {
            float nt = (Time.time - _startTime) / _durationIn;

            if(nt > 1)
            {
                nt = 1;
                _fadeIn = false;

                Invoke("FadeOut", 0.5f);
            }

            _image.alpha = nt;

            return;
        }

        if (_fadeOut)
        {
            float nt = (Time.time - _startTime) / _durationOut;

            if (nt > 1)
            {
                nt = 1;
                _fadeOut = false;
            }

            _image.alpha = 1 - nt;

            return;
        }
    }
}
