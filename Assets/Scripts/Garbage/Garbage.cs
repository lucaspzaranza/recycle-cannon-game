using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    //public MonoScript bulletToBecome;

    [SerializeField] protected int _size = 3;
    public int Size
    {
        get => _size;
        set => _size = value;
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("collect item");
            var player = other.GetComponent<Player>();
            player.CurrentTouchedObject = gameObject;
            player.isTouchingGarbage = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            var player = other.GetComponent<Player>();
            if(player.CurrentTouchedObject != null)
            {
                player.CurrentTouchedObject = gameObject;
                player.isTouchingGarbage = false;
            }
        }
    }
}
