using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Camera mainCamera;

    public Gun gun;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = mousePos - (Vector2)transform.position;

        transform.right = dir;
    }

    public void Fire()
    {
        gun.Fire();
    }
}
