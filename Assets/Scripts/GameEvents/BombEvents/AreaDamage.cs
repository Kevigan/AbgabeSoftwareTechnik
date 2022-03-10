using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaDamage : MonoBehaviour
{
    [SerializeField] private int damage = 50;
    [HideInInspector]
    public bool explode = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IDamageable>() is IDamageable victim && explode)
        {
            victim.TakeDamage(damage);
            Destroy(transform.parent.gameObject);
        }
        if (explode)
        {
            Destroy(transform.parent.gameObject);
        }
    }

}
