using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Character character;

    public float eyeHeight = 1;
    public float eyeSight = 3;

    public float pathFinder_x;
    public float pathFinder_length = 1f;

    void Update()
    {
        //搜索前方目标
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + transform.up * eyeHeight, transform.right * character.facing, eyeSight);
        foreach (var item in hits)
        {
            if(item.collider.transform.parent != transform && item.collider.gameObject.tag == "Player")
            {
                if (character.characterAttack.canAttack)
                    character.characterAttack.Attack();
            }
        }

        //判断前方可走
        bool walkable = false;
        foreach (var item in Physics2D.RaycastAll(transform.position + transform.right * pathFinder_x * character.facing, -transform.up, pathFinder_length))
        {
            if (item.collider.transform.parent != transform && item.collider.gameObject.tag == "Ground")
            {
                walkable = true;

                character.Move(character.facing);
            }
        }

        if(!walkable)
        {
            character.Flip();
            character.Move(0);

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + transform.up * eyeHeight, transform.position + transform.up * eyeHeight + transform.right * eyeSight);

        Gizmos.color = Color.blue;
        if(Application.isPlaying)
        Gizmos.DrawLine(transform.position + transform.right * pathFinder_x * character.facing, transform.position + transform.right * pathFinder_x + -transform.up * character.facing * pathFinder_length);
    }
}
