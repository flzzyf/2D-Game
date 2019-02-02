using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject prefab_missile;

    public Weapon weapon;

    //当前精准度
    float currentPrecision;
    //当前冷却进度
    float currentCD;

    void Update()
    {
        //消除当前后坐力降低的精准度
        if (currentPrecision > 0)
        {
            currentPrecision -= Time.deltaTime * weapon.precisionRecoverRate;

            transform.Rotate(-Vector3.forward * Time.deltaTime * 10 * weapon.precisionRecoverRate);
        }

        //开火间隔
        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
    }

    //开火
    public void Fire()
    {
        //冷却中
        if (currentCD > 0)
            return;

        currentCD += weapon.fireInterval;

        //发射飞弹
        GameObject missile = Instantiate(prefab_missile, firePoint.position, transform.rotation);
        StartCoroutine(LaunchMissile(missile.transform, weapon.missileLifeTime));

        //模拟后坐力
        currentPrecision += weapon.recoil;

        transform.Rotate(Vector3.forward * weapon.recoil * 10);
    }

    //发射飞弹
    IEnumerator LaunchMissile(Transform _missile, float _lifetime)
    {
        while (_lifetime > 0)
        {
            _lifetime -= Time.deltaTime;

            RaycastHit2D hit = Physics2D.Raycast(_missile.position, _missile.right, weapon.missileSpeed * Time.deltaTime);
            //命中判定
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Vector2 dir = _missile.right;
                    hit.collider.gameObject.GetComponent<Character>().Push(dir.normalized * 3);
                }

                break;
            }

            _missile.Translate(_missile.right * weapon.missileSpeed * Time.deltaTime, Space.World);

            yield return null;
        }

        //移除飞弹
        Destroy(_missile.gameObject);
    }
}
