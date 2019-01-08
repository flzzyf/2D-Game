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
        if(WalkableForward())
        {
            character.Move(character.facing);
        }
        else
        {
            character.Move(0);
            character.Flip();
        }
    }

    bool WalkableForward()
    {
        bool walkable = false;
        //判断前方地面
        foreach (var item in Physics2D.RaycastAll(transform.position + transform.right * pathFinder_x * character.facing + transform.up * .1f, -transform.up, pathFinder_length))
        {
            if (item.collider.transform.parent != transform && item.collider.gameObject.tag == "Ground")
            {
                walkable = true;
                break;
            }
        }

        //判断前方墙壁
        foreach (var item in Physics2D.RaycastAll(transform.position + transform.right * pathFinder_x * character.facing + transform.up * 1, transform.right * character.facing, pathFinder_length))
        {
            if (item.collider.transform.parent != transform && item.collider.gameObject.tag == "Ground")
            {
                walkable = false;
                break;
            }
        }

        return walkable;
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
