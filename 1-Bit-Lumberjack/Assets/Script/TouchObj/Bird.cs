using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour, ITouchObject
{
    public void OnTouch()
    {
        Debug.Log("Bird");
    }
}
