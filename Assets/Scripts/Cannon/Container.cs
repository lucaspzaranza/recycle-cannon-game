using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Container : MonoBehaviour
{
    private const string playerTag = "Player";

    public bool isTouchingPlayer = false;

    [SerializeField] private MonoScript acceptableGarbage;
    [SerializeField] private Cannon cannon;
    private GameObject player;

    void Start()
    {
        TapRegion.OnRightTapPressed += HandleRightTap;
    }

    private void HandleRightTap()
    {
        if (isTouchingPlayer)
            StartCoroutine(nameof(AddGarbageBulletOnCannon));
    }
    private IEnumerator AddGarbageBulletOnCannon()
    {
        var playerScript = player.GetComponent<Player>();
        if (playerScript.collectedGarbage == null) yield break;

        var playerGarbageType = playerScript.collectedGarbage.GetType();
        if (playerGarbageType.IsEquivalentTo(acceptableGarbage.GetClass()))
        {
            if (cannon.garbageTypeScript != null)
            {
                print("era pra destruir o componente " + cannon.garbageTypeScript.GetType().ToString());
                Destroy(cannon.gameObject.GetComponent(cannon.garbageTypeScript.GetType()));
                cannon.garbageTypeScript = null;
            }

            cannon.gameObject.AddComponent(playerGarbageType);
            yield return new WaitForEndOfFrame();

            int currentAmmo = playerScript.collectedGarbage.Size;
            var cannonGarbage = cannon.gameObject.GetComponent<Garbage>();

            cannonGarbage.Size = currentAmmo;
            cannon.garbageTypeScript = cannonGarbage;
            cannon.ammo = currentAmmo;

            Destroy(playerScript.collectedGarbage);
            playerScript.collectedGarbage = null;
            Debug.Log("Lixo coletado com sucesso!");
        }
        else
            Debug.LogWarning("Não pode inserir esse tipo de lixo nessa lixeira!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            isTouchingPlayer = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag)
        {
            isTouchingPlayer = false;
            player = other.gameObject;
        }
    }
}
