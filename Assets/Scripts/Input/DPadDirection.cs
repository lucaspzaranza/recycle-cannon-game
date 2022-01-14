using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPadDirection : MonoBehaviour
{
    public Vector2 direction;

    [SerializeField] private DPad dPad;

    public delegate void OnCannonInput(Vector2 direction);
    public delegate void OnPlayerInput(Vector2 direction);

    public static event OnPlayerInput OnPlayerInputFired;
    public static event OnPlayerInput OnCannonInputFired;

    private void OnMouseDrag()
    {
        if (dPad.dPadMovement == DPadMovement.PlayerMovement)
            OnPlayerInputFired?.Invoke(direction);
        else
            OnCannonInputFired?.Invoke(direction);
    }

    private void OnMouseUp()
    {
        if (dPad.dPadMovement == DPadMovement.PlayerMovement)
            OnPlayerInputFired?.Invoke(Vector2.zero);
        else
            OnCannonInputFired?.Invoke(Vector2.zero);
    }
}
