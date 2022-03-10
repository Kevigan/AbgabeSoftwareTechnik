using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bomb : MonoBehaviour
{
    public delegate void OnInteractWithBomb(int id);
    public static event OnInteractWithBomb onPickUpBomb;
    public static event OnInteractWithBomb onDropBomb;

    public delegate void OnExplosion();
    public static event OnExplosion onExplosion;

    private bool playerInRange = false;
    private bool itemAttachedToPlayer = false;

    private bool bombInArea = false;

    [SerializeField] private AreaDamage areaDamage;
    [SerializeField] private int id;

    [SerializeField] private float timer = 3f;
    [SerializeField] private Text text;

    [SerializeField] ParticleSystem smoke;

    public GameObject area;
    public GameObject uiTimer;

    [HideInInspector]
    public bool activateTimer = false;

    private void Start()
    {
        PlayerController.interact += AttachToPlayer;
        PlayerController.interact += DetacheFromPlayer;

        BombPlace.onBombInArea += SetBombInArea;
        BombPlace.onBombOutOfArea += SetBombInArea;
    }

    private void Update()
    {
        if (activateTimer)
        {
            timer -= Time.deltaTime;
            int _uiTimer = (int)timer;
            text.text = _uiTimer.ToString();
            if (timer <= 0)
            {
                areaDamage.explode = true;
                onExplosion();
                SoundManager.Instance.PlaySound(Sounds.explosion);
                activateTimer = false;
            }
            uiTimer.transform.LookAt(Camera.main.transform);
        }
    }

    void AttachToPlayer(PlayerController player)
    {
        if (playerInRange && !bombInArea)
        {
            transform.parent = player.PickUpPosition;
            transform.position = player.PickUpPosition.position;
            StartCoroutine(SetItemAttachedToPlayer());
            if (onPickUpBomb != null)
            {
                onPickUpBomb(id);
            }
        }
    }

    void DetacheFromPlayer(PlayerController player)
    {
        if (itemAttachedToPlayer)
        {
            transform.SetParent(null);
            transform.position = player.DropPosition.position;
            itemAttachedToPlayer = false;

            StartCoroutine(WaitBombInArea());
        }
    }

    IEnumerator WaitBombInArea()
    {
        yield return new WaitForSeconds(.1f);
        if (bombInArea)
        {
            smoke.Play();
            onDropBomb(id);
            ActivateAreaDamageTimer();
        }
    }

    IEnumerator SetItemAttachedToPlayer()
    {
        yield return new WaitForSeconds(.1f);
        itemAttachedToPlayer = true;
    }

    void SetBombInArea(int id)
    {
        if (this.id == id)
        {
            bombInArea = !bombInArea;
        }
    }

    void ActivateAreaDamageTimer()
    {
        activateTimer = true;
        uiTimer.SetActive(true);
        area.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerInRange = false;
        }
    }

    private void OnDestroy()
    {
        PlayerController.interact -= AttachToPlayer;
        PlayerController.interact -= DetacheFromPlayer;

        BombPlace.onBombInArea -= SetBombInArea;
        BombPlace.onBombOutOfArea -= SetBombInArea;
    }
}
