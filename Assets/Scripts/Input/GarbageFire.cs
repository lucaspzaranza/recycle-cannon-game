using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageFire : MonoBehaviour
{
    public delegate void FireButtonPressed();
    public static event FireButtonPressed OnFireButtonPressed;

    private void OnMouseDown()
    {
        OnFireButtonPressed?.Invoke();
    }
}
