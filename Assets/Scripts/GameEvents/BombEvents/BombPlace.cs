using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlace : MonoBehaviour
{
    [SerializeField] private int id;
    public delegate void OnBombInArea(int id);
    public static event OnBombInArea onBombInArea;
    public static event OnBombInArea onBombOutOfArea;

    private bool bombPlaceActive = false;

    private void Start()
    {
        Bomb.onPickUpBomb += ActivateBombPlace;
        Bomb.onDropBomb += DeactivateBombPlace;
        GetComponent<Renderer>().enabled = false;
    }

    void ActivateBombPlace(int id)
    {
        if (this.id == id)
        {
            bombPlaceActive = true;
            GetComponent<Renderer>().enabled = true;
        }
    }
    void DeactivateBombPlace(int id)
    {
        if (this.id == id)
        {
            bombPlaceActive = false;
            GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bomb>() && bombPlaceActive)
        {
            onBombInArea(id);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Bomb>())
        {
            onBombOutOfArea(id);
        }
    }
}

