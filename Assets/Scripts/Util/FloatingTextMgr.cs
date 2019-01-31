using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextMgr : Singleton<FloatingTextMgr>
{
    public GameObject prefab_floatingText;

    public void Create(Vector3 _pos, string _text, Color _color)
    {
        GameObject text = Instantiate(prefab_floatingText, _pos, Quaternion.identity);
        text.GetComponent<FloatingText>().text.color = _color;
        text.GetComponent<FloatingText>().text.text = _text;
    }
}
