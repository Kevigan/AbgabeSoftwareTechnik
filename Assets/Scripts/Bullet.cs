using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float destroyTimer = 5f;
    [SerializeField] private int damage = 10;
    private float _destroyTimer;

    private void Start()
    {
        _destroyTimer = destroyTimer;
    }

    private void OnEnable()
    {
        _destroyTimer = destroyTimer;
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        _destroyTimer -= Time.deltaTime;

        if (_destroyTimer <= 0) gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() is IDamageable victim)
        {
            victim.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
