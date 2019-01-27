using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tools;

public class Subtitle : MonoSingleton<Subtitle>
{
    [SerializeField] string _string;
    [SerializeField] Text _text;
    [SerializeField] float _time;

    float _startTime;

    // Use this for initialization
    public void StartAnim()
    {
        if(gameObject.activeInHierarchy && !_text.gameObject.activeInHierarchy)
        {
            _startTime = Time.time;
            _text.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_startTime != 0)
        {
            if(Time.time - _startTime > _time)
            {
                _startTime = 0;
                _text.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
