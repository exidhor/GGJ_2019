using UnityEngine;
using System.Collections.Generic;
using Tools;
using System;

[Serializable]
public class PanoplyData
{
    public PanoplyType type;
    public int score;
    public int chaos;
    public string endMessage;
}

public class PanoplyManager : MonoSingleton<PanoplyManager>
{
    [SerializeField] PanoplyData[] _panoplies = new PanoplyData[0];

    public PanoplyData Get(PanoplyType type)
    {
        for (int i = 0; i < _panoplies.Length; i++)
        {
            if(_panoplies[i].type == type)
            {
                return _panoplies[i];
            }
        }

        return null;
    }
}
