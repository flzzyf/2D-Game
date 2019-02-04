using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Character character;

    public ParticleSystem particle;

    public GunController gunController;

    void Update()
    {
        float inputH = Input.GetAxisRaw("Horizontal");

        character.Move(inputH);

        //跳跃
        // if (Input.GetKeyDown(KeyCode.Space) && !character.jumping)
        // {
        //     character.Jump();
        // }

        if (mouseDown)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                print("up");
                mouseDown = false;

            }
            else
            {
                print("downing");
            }

        }

        if (mouseDown != true && Input.GetButtonDown("Fire1"))
        {
            mouseDown = true;
        }

        //单击攻击键
        // if (Input.GetButtonUp("Fire1") && !character.jumping)
        // {
        //     gunController.OnMouseClick();
        //     print("点击");
        // }

        // //持续按住攻击键
        // if (Input.GetButton("Fire1"))
        // {
        //     print("长按");

        //     if (Input.GetButtonUp("Fire1"))
        //         print("qwe");

        //     //gunController.Fire();
        // }

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     StartCoroutine(BecomeInvincible(1f));
        // }

        //character.AnimationTrigger("attack");
        //前推
        //character.Push(new Vector2(character.facing, 0.2f) * 5);
    }

    bool mouseDown;
    public float mouseDownTime = 0.3f;
    IEnumerator MouseDown()
    {
        float timer = mouseDownTime;
        while (timer > 0 && Input.GetMouseButton(0))
        {
            yield return null;

            timer -= Time.deltaTime;
        }

        mouseDown = true;
    }

    IEnumerator BecomeInvincible(float _duration)
    {
        particle.Play();
        character.IsInvincible = true;

        yield return new WaitForSeconds(_duration);

        particle.Stop();
        character.IsInvincible = false;
    }
}
