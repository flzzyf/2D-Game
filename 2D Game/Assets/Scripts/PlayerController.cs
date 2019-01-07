using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Character character;

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
        if (Input.GetKeyDown(KeyCode.F) && !character.jumping)
        {
            if(character.characterAttack.canAttack)
                character.characterAttack.Attack();
        }
    }
}
