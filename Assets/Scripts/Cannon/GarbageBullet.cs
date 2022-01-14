using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GarbageBullet : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;

    public MonoScript madeByGarbage;
    public float speed;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(0f, speed * Time.deltaTime, 0f);
    }
}
