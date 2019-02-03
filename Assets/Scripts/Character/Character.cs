using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 3;

    //组件
    public GameObject gfx;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Rigidbody2D rb;

    [HideInInspector]
    public CharacterAttack characterAttack;

    bool facingRight { get { return !spriteRenderer.flipX; } }

    //正在跳跃
    [HideInInspector]
    public bool jumping;

    float inputH;

    public int hpMax = 1;
    int hpCurrent;

    public Slider slider_hp;

    public float facing { get { return facingRight ? 1 : -1; } }

    public GameObject effect_death;

    int invincibleCount;
    public bool IsInvincible
    {
        get { return invincibleCount > 0; }
        set { if (value) invincibleCount++; else invincibleCount--; }
    }

    void Awake()
    {
        animator = gfx.GetComponent<Animator>();
        spriteRenderer = gfx.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        characterAttack = GetComponent<CharacterAttack>();

        hpCurrent = hpMax;
        //UpdateStatusBar();
    }

    void Update()
    {
        if (inputH != 0)
        {
            //如果人物向右走，当前朝向却不是朝右，翻转角色
            if (inputH > 0 != facingRight)
            {
                Flip();
            }

        }

        animator.SetFloat("speed", Mathf.Abs(inputH));
    }

    public void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    void FixedUpdate()
    {
        if (inputH != 0)
        {
            //rb.AddForce(Vector3.right * inputH * speed * Time.fixedDeltaTime, ForceMode.Impulse);
            //rb.MovePosition(transform.position + transform.right * inputH * speed * Time.fixedDeltaTime);
            transform.Translate(transform.right * inputH * speed * Time.deltaTime);

            //inputH = 0;
        }
    }

    void UpdateStatusBar()
    {
        slider_hp.value = (float)hpCurrent / hpMax;
    }

    //碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //目标在下，则结束跳跃状态
        if (Vector3.Dot(transform.up, collision.transform.position - transform.position) < 0)
        {
            jumping = false;
            animator.SetBool("jumping", false);
        }
    }

    public void Move(float _dir)
    {
        inputH = _dir;
    }

    //跳跃
    public void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

        jumping = true;
        //跳跃动画
        animator.SetBool("jumping", true);
    }

    //被击飞
    public void Hit(Vector3 _caster)
    {
        //如果施法者在左侧则方向为1
        float dir = Vector3.Dot(transform.right, _caster - transform.position) < 0 ? 1 : -1;

        rb.AddForce(new Vector3(1 * dir, 1, 0) * 2, ForceMode2D.Impulse);
    }

    public void TakeDamage(Character _attacker, int _amount)
    {
        hpCurrent -= _amount;

        if (hpCurrent <= 0)
            Death();
        else
        {
            UpdateStatusBar();

            Blink();

            Hit(_attacker.transform.position);
        }
    }

    public void Heal(int _amount)
    {
        //判断有造成治疗
        if (hpCurrent == hpMax)
            return;

        int healAmount = Mathf.Min(hpMax, hpCurrent + _amount) - hpCurrent;
        FloatingTextMgr.instance.Create(transform.position + Vector3.up * 1, healAmount.ToString(), Color.green);

        hpCurrent += healAmount;

        UpdateStatusBar();
    }

    void Death()
    {
        Destroy(transform.gameObject);

        GameObject fx = Instantiate(effect_death, transform.position, Quaternion.identity);
        Destroy(fx, fx.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

    //人物图闪烁
    public void Blink()
    {
        StartCoroutine(BlinkCor(2));
    }

    IEnumerator BlinkCor(int _times)
    {
        for (int i = 0; i < _times; i++)
        {
            IsInvincible = true;

            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.2f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.2f);

            IsInvincible = false;
        }
    }

    //触发动画
    public void AnimationTrigger(string _name)
    {
        animator.SetTrigger(_name);
    }
}
