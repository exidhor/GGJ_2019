using UnityEngine;
using System.Collections.Generic;
using Tools;

public class RatManager : MonoSingleton<RatManager>
{
    [SerializeField] List<GameObject> _targets = new List<GameObject>();

    [SerializeField] Rat _model;

    List<Rat> _rats = new List<Rat>();

    public void Init()
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            Rat r = Instantiate(_model);
            _rats.Add(r);
            r.Init(_targets[i]);
        }
    }

    public bool IsFinish()
    {
        for (int i = 0; i < _rats.Count; i++)
        {
            if (!_rats[i].hasQuit)
                return false;
        }

        return true;
    }

    public void Clear()
    {
        for (int i = 0; i < _rats.Count; i++)
        {
            Destroy(_rats[i].gameObject);
        }

        _rats.Clear();
    }

    Rat Instantiate(Rat d)
    {
        Rat newD = GameObject.Instantiate(d);
        newD.transform.localScale = d.transform.localScale;
        newD.transform.rotation = Quaternion.identity;

        return newD;
    }
}
