using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody = null;
    [SerializeField] private float speed = 150f;
    [SerializeField] private float rotationLimit = 30f;
    [SerializeField] private Transform showSpawnPos;
    [SerializeField] private List<GameObject> _bulletsPrefabs = null;

    public Garbage garbageTypeScript = null;
    public int ammo = 0;

    private int signal = 1, lastSignal = -1;
    private float rotationDirection = 0f;

    public List<GameObject> BulletsPrefabs => _bulletsPrefabs;

    void Start()
    {
        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody>();

        DPadDirection.OnCannonInputFired += HandleOnCannonInputFired;
        GarbageFire.OnFireButtonPressed += FireGarbageBullet;
    }

    private void FixedUpdate()
    {
        if (rotationDirection != 0f)
            RotateCannon(rotationDirection);
    }

    private void HandleOnCannonInputFired(Vector2 direction)
    {
        rotationDirection = direction.x;
    }

    private void FireGarbageBullet()
    {
        if (garbageTypeScript == null || ammo == 0)
        {
            Debug.LogWarning("Você está sem munição! Colete lixo para reciclagem e conseguir mais poder de fogo.");
            return;
        }

        // Escolhendo a bala de lixo pelo tipo de lixo do qual ela foi feita.
        GameObject currentBulletPrefab = BulletsPrefabs.
            SingleOrDefault(prefab => prefab
            .GetComponent<GarbageBullet>()
            .madeByGarbage
            .GetClass()
            .IsEquivalentTo(garbageTypeScript.GetType()));

        if (currentBulletPrefab == null) return;

        var newBullet = Instantiate(currentBulletPrefab, showSpawnPos.position, Quaternion.identity);
        newBullet.transform.localRotation = transform.rotation;
        ammo--;

        if(ammo == 0)
            DestroyGarbageBullet();
    }

    private void DestroyGarbageBullet()
    {
        Destroy(garbageTypeScript);
        garbageTypeScript = null;
    }

    public void RotateCannon(float direction)
    {
        signal = (direction < 0) ? -1 : 1;

        float newRotation = speed * Time.deltaTime * -signal;
        float newRotationY = transform.eulerAngles.y + Mathf.Abs(newRotation);
     
        if(signal == lastSignal) // Permite rotação se estiver mudando de sinal
        {
            if ((newRotationY >= 0f && newRotationY <= rotationLimit) || (newRotationY >= (360f - rotationLimit) && newRotationY <= 360f))
                transform.Rotate(0f, 0f, newRotation);
        }
        else
        {
            lastSignal = signal;
            transform.Rotate(0f, 0f, newRotation);
        }
    }
}
