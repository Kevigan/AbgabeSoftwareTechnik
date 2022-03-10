using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField] private int damage = 50;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerStats>() is PlayerStats player)
        {
            player.TakeDamage(damage);
            SoundManager.Instance.StopSound(Sounds.battleSound);
            Destroy(GetComponentInParent<EnemyStats>().gameObject);
        }
    }
}
