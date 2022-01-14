using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapRegion : MonoBehaviour
{
    [SerializeField] private bool isRight = true;

    public delegate void OnLeftTap();
    public delegate void OnRightTap();

    public static event OnLeftTap OnLeftTapPressed;
    public static event OnRightTap OnRightTapPressed;

    private void OnMouseDown()
    {
        if (isRight)
        {
            print("right tap");
            OnRightTapPressed?.Invoke();
        }
        else 
        {
            print("left tap");
            OnLeftTapPressed?.Invoke();
        }
    }
}
