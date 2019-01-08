using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Character character;

    public ParticleSystem particle;

    void Update()
    {
        float inputH = Input.GetAxisRaw("Horizontal");

        character.Move(inputH);

        //跳跃
        if(Input.GetKeyDown(KeyCode.Space) && !character.jumping)
        {
            character.Jump();
        }

        //攻击
        if ((Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("Fire1")) && !character.jumping)
        {
            if(character.characterAttack.canAttack)
                character.characterAttack.Attack();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(BecomeInvincible(1f));

        }
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
