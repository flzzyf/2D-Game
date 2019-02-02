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
        if (Input.GetKeyDown(KeyCode.Space) && !character.jumping)
        {
            character.Jump();
        }

        //攻击
        if ((Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1")) && !character.jumping)
        {
            gunController.Fire();

            // if(character.characterAttack.canAttack)
            //     character.characterAttack.Attack();
        }

        if (Input.GetButton("Fire1"))
        {
            if (Input.GetButtonUp("Fire1"))
            {
                print("up");
            }

            gunController.Fire();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(BecomeInvincible(1f));
        }

        //character.AnimationTrigger("attack");
        //前推
        //character.Push(new Vector2(character.facing, 0.2f) * 5);
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
