using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject garbage;
    [SerializeField] protected Rigidbody rigibody;
    [SerializeField] protected List<MonoScript> defeatedBy;
    [SerializeField] protected int life = 3;

    [SerializeField] protected float speed = 3f;
    public float Speed => speed;

    void Start()
    {
        if (rigibody == null)
            rigibody = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rigibody.velocity = new Vector3(0f, 0f, -speed * Time.deltaTime);
    }

    private bool BulletDefeatsEnemy(GarbageBullet currentBullet)
    {
        bool hasBulletWhichDestroysTheEnemy = 
            defeatedBy.Any(bulletScript => bulletScript.GetClass().IsEquivalentTo(currentBullet.madeByGarbage.GetClass()));
        return hasBulletWhichDestroysTheEnemy;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            var bullet = other.gameObject.GetComponent<GarbageBullet>();
            if(BulletDefeatsEnemy(bullet))
            {
                Destroy(other.gameObject);
                life--;
                if(life == 0)
                {
                    Instantiate(garbage, transform.position, new Quaternion(0.7f, 0f, 0f, 0.7f)); 
                    Destroy(gameObject);
                    EnemySpawnerPool.instance.currentEnemyCount--;
                }
            }
        }
    }
}
