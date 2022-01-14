using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEditorInternal;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody = null;
    [SerializeField] private float speed = 150f;

    public GameObject CurrentTouchedObject;
    public Garbage collectedGarbage = null;
    public bool isTouchingGarbage = false;


    void Start()
    {
        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();

        DPadDirection.OnPlayerInputFired += HandleOnPlayerInputFired;
        TapRegion.OnRightTapPressed += HandleRightTap;
        TapRegion.OnLeftTapPressed += HandleLeftTap;
    }

    private void HandleOnPlayerInputFired(Vector2 direction)
    {
        Move(direction);
    }

    private void HandleRightTap()
    {
        if(isTouchingGarbage)
            StartCoroutine(nameof(UpdateGarbageComponent));
    }

    private void HandleLeftTap()
    {

    }

    private IEnumerator UpdateGarbageComponent()
    {
        if (collectedGarbage != null)
            Destroy(collectedGarbage);

        var garbage = CurrentTouchedObject.GetComponent<Garbage>();
        Type garbageType = garbage.GetType();
        gameObject.AddComponent(garbageType);
        gameObject.GetComponent<Garbage>().Size = garbage.Size;
        yield return new WaitForEndOfFrame();

        collectedGarbage = GetComponent<Garbage>();
        Destroy(CurrentTouchedObject);
        CurrentTouchedObject = null;
        isTouchingGarbage = false;
    }

    public void Move(Vector2 direction)
    {
        // the y axis will go to z axis due camera position
        rigidbody.velocity = new Vector3(direction.x * speed * Time.deltaTime, 0f, direction.y * speed * Time.deltaTime);
        rigidbody.angularVelocity = rigidbody.velocity;
    }
}
