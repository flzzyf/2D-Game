using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FloatingText : MonoBehaviour
{
    public Text text;

    void Start()
    {
        text.transform.localScale = new Vector3(.1f, .1f, .1f);
        text.transform.DOScale(Vector3.one, .1f);

        text.transform.DOMoveY(transform.position.y + .2f, 2);

        text.DOFade(0, 3);
        Destroy(gameObject, 3);
    }

}
