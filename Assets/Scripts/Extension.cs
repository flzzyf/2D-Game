using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    //施加力
    public static void Push(this GameObject _go, Vector2 _dir)
    {
        if (_go.GetComponent<Rigidbody2D>() != null)
            _go.GetComponent<Rigidbody2D>().AddForce(_dir, ForceMode2D.Impulse);
    }
}
