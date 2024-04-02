using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }



}

[System.Serializable]
public class Sound
{

}

