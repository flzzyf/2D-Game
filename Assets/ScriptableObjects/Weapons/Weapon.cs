using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    //鼠标点击后（长按不触发）
    public Operation[] OnMouseClick;
    //鼠标长按
    public Operation[] KeepMouseDown;

    public Sprite gunSprite;

    //飞弹设置
    public float missileSpeed = 10;
    public float missileLifeTime = 1;

    //开火后坐力，
    public float recoil = 1.2f;
    //精准度回复速度
    public float precisionRecoverRate = 9;
    //开火间隔
    public float fireInterval = 0.1f;

    public Vector2 firePoint;
}

//操作类型
public enum OperationType { Fire, Wait }

//操作
[System.Serializable]
public class Operation
{
    public OperationType type;
    public float time;
}