using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //传送目的地
    public Transform destination;

    public GameObject ui;

    GameObject player;

    void Start()
    {
        ui.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player != null)
        {
            player.transform.position = destination.position;
            player = null;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = collider.gameObject;

            ui.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = null;

            ui.SetActive(false);
        }
    }
}
