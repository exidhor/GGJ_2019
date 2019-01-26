using UnityEngine;
using System.Collections.Generic;
using Tools;

public class HiddenObjects : MonoSingleton<HiddenObjects>
{
    public int count
    {
        get { return _lefts.Count; }
    }

    [SerializeField] Draggable[] _draggables = new Draggable[0];

    List<Draggable> _lefts = new List<Draggable>();

    void Awake()
    {
        for (int i = 0; i < _draggables.Length; i++)
        {
            _lefts.Add(_draggables[i]);
        }
    }

    public Draggable GetOne()
    {
        int index = Random.Range(0, _lefts.Count);
        Draggable d = _lefts[index];
        _lefts.RemoveAt(index);

        return Instantiate(d);
    }

    Draggable Instantiate(Draggable d)
    {
        Draggable newD = GameObject.Instantiate(d);
        newD.transform.localScale = d.transform.localScale;
        newD.transform.rotation = Quaternion.identity;

        return newD;
    }
}
