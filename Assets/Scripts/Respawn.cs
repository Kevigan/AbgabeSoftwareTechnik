using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{ 
    [SerializeField] private Transform respawnPos;
    private PlayerController player;


    private void Awake()
    {
    }

    IEnumerator EnableCharController()
    {
        yield return new WaitForSeconds(2);
        player.transform.position = respawnPos.transform.position;
        player.EnableCharacterController();
        SoundManager.Instance.StopSound(Sounds.Playerdeath);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() is PlayerController player)
        {
            player.DisableCharacterController();
            this.player = player;
            SoundManager.Instance.PlaySound(Sounds.Playerdeath);
            StartCoroutine(EnableCharController());
        }
    }
}
