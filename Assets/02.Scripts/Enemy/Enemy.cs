using UnityEngine;

public enum EnemyState { Move, Knockback, Frozen, Burn, Dead }

public class Enemy : MonoBehaviour
{
    // 에너미 스테이터스
    public float Health; // 체력
    public float MovementSpeed; // 이동속도
    public float FrozeTime; // 얼려지는 시간 계수 - 1 이면 함수에서 호출된 시간만큼 얼어있음
    public float KnockbackTime; // 뒤로 날아가는 시간
    public float KnockbackResistance; // 뒤로 날아가는 거리 계수 - 1 이면 호출된 거리만큼 뒤로 날아감
    public float BurningTime; // 타는 시간
    public float Flammable; // 타고있을때 더 불이 적용되면 불 데미지가 얼마나 증가하는지 - 1 이면 불 데미지 그만큼 추가
    public int Reward;

    //다중 상태 병렬 처리
    private bool isFrozen = false;
    private bool isKnockback = false;
    private float knockbackPower = 0;
    private bool isBurning = false;
    private float burningDamage = 0;
    private float burningTickTimer = 0;
    private float burningTick = 0.5f;
    private bool isDead = false;

    private float freezeTimer = 0f;
    private float knockbackTimer = 0f;
    private float burningTimer = 0f;

    void FixedUpdate()
    {
        if (isDead) return;

        if (Health <= 0)
        {
            Die();
            return;
        }

        //상태 타이머 감소
        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0) isFrozen = false;
        }

        if (isKnockback)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0) isKnockback = false;
        }

        if (isBurning)
        {
            burningTimer -= Time.deltaTime;
            burningTickTimer -= Time.deltaTime;
            if (burningTimer <= 0)
            {
                isBurning = false;
                burningTickTimer = 0;
                burningDamage = 0;
            }

        }

        //이동 로직
        if (!isFrozen && !isKnockback)
        {
            Move();
        }

        if (isKnockback)
        {
            Knockback();
        }

        //도트 데미지
        if (isBurning && burningTickTimer <= 0)
        {
            burningTickTimer = burningTick;
            Burning();
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.right * MovementSpeed * Time.deltaTime);
    }

    private void Knockback()
    {
        Debug.Log("넉백");
        transform.Translate(Vector2.left * (KnockbackResistance * knockbackPower / KnockbackTime) * Time.deltaTime);
    }

    private void Burning()
    {
        Debug.Log($"불탐 : {Health}");
        Health -= burningDamage;
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("죽음");
        Destroy(gameObject);
    }

    /// <summary>
    /// 넉백을 적용시키는 함수, 매개변수는 날아가는 거리
    /// </summary>

    public void ApplyKnockback(float hittedKnockbackPower)
    {
        isKnockback = true;
        knockbackPower = hittedKnockbackPower;
        knockbackTimer = KnockbackTime;
    }

    /// <summary>
    /// 얼음을 적용시키는 함수, 매개변수는 얼리는 시간
    /// </summary>

    public void ApplyFrozen(float freezePower)
    {
        isFrozen = true;
        freezeTimer = FrozeTime * freezePower;
    }

    /// <summary>
    /// 불을 적용시키는 함수, 매개변수는 불타는 데미지
    /// </summary>

    public void ApplyBurning(float fireDamage)
    {
        isBurning = true;
        burningTimer = BurningTime;
        if (burningDamage == 0)
        {
            burningDamage = fireDamage;
        }
        else
        {
            burningDamage += fireDamage * Flammable;
        }
    }

    /// <summary>
    /// 데미지 적용
    /// </summary>

    public void TakeDamage(float amount)
    {
        Health -= amount;
    }
}
