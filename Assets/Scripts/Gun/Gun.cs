using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject prefab_missile;

    //飞弹设置
    public float missileSpeed = 10;
    public float missileLifeTime = 1;

    //开火后坐力，
    public float recoil = 0.1f;
    //当前精准度
    float currentPrecision;
    //精准度回复速度
    public float precisionRecoverRate = 1;

    //开火间隔
    public float fireInterval = 0.1f;
    float currentCD;

    void Update()
    {
        //消除当前后坐力降低的精准度
        if (currentPrecision > 0)
        {
            currentPrecision -= Time.deltaTime * precisionRecoverRate;

            transform.Rotate(-Vector3.forward * Time.deltaTime * 10 * precisionRecoverRate);
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

        currentCD += fireInterval;

        //发射飞弹
        GameObject missile = Instantiate(prefab_missile, firePoint.position, transform.rotation);
        StartCoroutine(LaunchMissile(missile.transform, missileLifeTime));

        //模拟后坐力
        currentPrecision += recoil;

        transform.Rotate(Vector3.forward * recoil * 10);
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
