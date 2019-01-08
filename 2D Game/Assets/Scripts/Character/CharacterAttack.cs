using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public Vector3 attackCollider_center;
    public float attackCollider_radius = 1;

    Character character;

    public float cooldown = 1;
    float currentCooldown;

    public bool canAttack { get { return currentCooldown <= 0; } }

    public Vector2Int damage = new Vector2Int(2, 4);

    void Start()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        currentCooldown = cooldown;

        character.animator.Play("AD_Attack_1");
        character.rb.AddForce(new Vector3(character.facing, 0.2f, 0) * 5, ForceMode2D.Impulse);
    }

    public void AttackEffect()
    {
        Vector3 center = attackCollider_center;
        center.x *= character.facing;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + center, attackCollider_radius);
        foreach (var item in colliders)
        {
            if (item.tag == "Player" && item.gameObject != gameObject)
            {
                ApplyDamage(character, item.GetComponent<Character>());
            }
        }
    }

    void ApplyDamage(Character _caster, Character _target)
    {
        //施加伤害
        int amount = Random.Range(damage.x, damage.y + 1);

        //背刺暴击判定
        //目标朝向和攻击者相同，(而且施法者朝向目标
        //Vector2.Dot(_target.transform.position - _caster.transform.position, _caster.transform.right) > 0 &&
        if (_caster.facing == _target.facing)
        {
            print("背刺");
            amount *= 2;
        }

        _caster.Heal(amount);
        _target.TakeDamage(character, amount);

        //创建浮动伤害文本
        FloatingTextMgr.instance.Create(_target.transform.position + Vector3.up * 1, amount.ToString());
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + attackCollider_center, attackCollider_radius);
    }
}
