using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Camera mainCamera;

    public Gun gun;

    //持续开火
    public bool fireing;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        //修改枪朝向
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - (Vector2)transform.position;

        transform.right = dir;

        //左右翻转
        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector2(1, -1);
        else
            transform.localScale = Vector2.one;

    }

    public void Fire()
    {
        gun.Fire();
    }
}
