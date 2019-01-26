using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tools;

public class End : MonoSingleton<End>
{
    [SerializeField] Validator _validator;
    [SerializeField] GameObject _canvas;
    [SerializeField] Text _endText;
    [SerializeField] float _timePerLetter = 1;

    string _string;
    int _letterIndex;
    float _currentLetterTime;

    bool _isWritting;

    public void Actualize()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 wpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Rect r = _validator.GetCollider();

            if (r.Contains(wpos))
            {
                AskForEnd();
            }
        }

        if(_isWritting)
        {
            _currentLetterTime += Time.deltaTime;

            if(_currentLetterTime > _timePerLetter)
            {
                _currentLetterTime -= _timePerLetter;
                _letterIndex++;

                if(_letterIndex == _string.Length)
                {
                    _isWritting = false;
                }
            }

            _endText.text = _string.Substring(0, _letterIndex);
        }
    }

    void AskForEnd()
    {
        if (ContainerManager.instance.CanFinish())
        {
            _canvas.SetActive(true);

            _string = "EASY END !!";
            _isWritting = true;
        }
    }
}
