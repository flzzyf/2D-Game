using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject prefab_missile;

    public float missileSpeed = 10;
    public float missileLifeTime = 1;

    //开火
    public void Fire()
    {
        GameObject missile = Instantiate(prefab_missile, firePoint.position, transform.rotation);

        StartCoroutine(LaunchMissile(missile.transform, missileLifeTime));
    }

    //发射飞弹
    IEnumerator LaunchMissile(Transform _missile, float _lifetime)
    {
        while (_lifetime > 0)
        {
            _lifetime -= Time.deltaTime;

            //命中判定
            if (Physics2D.Raycast(_missile.position, _missile.right, missileSpeed * Time.deltaTime))
            {
                print("hit");
                break;
            }

            _missile.Translate(_missile.right * missileSpeed * Time.deltaTime, Space.World);

            yield return null;
        }

        //移除飞弹
        Destroy(_missile.gameObject);
    }
}
