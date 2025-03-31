using System.Collections;
using UnityEngine;

public enum EnemyState { Move, Knockback, Frozen, Burn, Dead }

public class Enemy : MonoBehaviour
{
    // 이동 맵
    private WaypointPath path;
    private int currentWaypointIndex = 0;

    // 에너미 스테이터스
    public EnemyData enemyData;
    private SpriteRenderer spriteRenderer;
    private float Health; // 체력
    private float MovementSpeed; // 이동속도
    private float FrozeTime; // 얼려지는 시간 계수 - 1 이면 함수에서 호출된 시간만큼 얼어있음
    private float KnockbackTime; // 뒤로 날아가는 시간
    private float KnockbackResistance; // 뒤로 날아가는 거리 계수 - 1 이면 호출된 거리만큼 뒤로 날아감
    private float BurningTime; // 타는 시간
    private float Flammable; // 타고있을때 더 불이 적용되면 불 데미지가 얼마나 증가하는지 - 1 이면 불 데미지 그만큼 추가
    private int Reward;

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

    //피격 색상 변경
    private Color originalColor;
    private Coroutine colorChangeCoroutine;

    //데이터 연결
    void Awake()
    {
        if (path == null)
        {
            path = ((EnemyFactory)FactoryManager.Instance.path[nameof(EnemyFactory)]).path;
        }
        InitializeFromData();
    }

    void OnEnable()
    {
        currentWaypointIndex = 0;

        isDead = false;
        isFrozen = false;
        isKnockback = false;
        knockbackPower = 0;
        isBurning = false;
        burningDamage = 0;
        burningTickTimer = 0;

        freezeTimer = 0f;
        knockbackTimer = 0f;
        burningTimer = 0f;

        // spriteRenderer.color = originalColor;
        if (colorChangeCoroutine != null)
        {
            colorChangeCoroutine = null;
        }
    }

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

    public void InitializeFromData()
    {
        if (enemyData == null)
        {
            Debug.LogWarning("EnemyData가 할당되지 않았습니다.");
            return;
        }

        Health = enemyData.Health;
        MovementSpeed = enemyData.MovementSpeed;
        FrozeTime = enemyData.FrozeTime;
        KnockbackTime = enemyData.KnockbackTime;
        KnockbackResistance = enemyData.knockbackDistance;
        BurningTime = enemyData.BurningTime;
        Flammable = enemyData.Flammable;
        Reward = Mathf.RoundToInt(enemyData.RewardCoin);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemyData.SpriteImage;
        originalColor = spriteRenderer.color;
    }

    private void Move()
    {
        if (path == null || path.WaypointCount == 0) return;

        Transform target = path.GetWaypoint(currentWaypointIndex);
        transform.position = Vector2.MoveTowards(transform.position, target.position, MovementSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= path.WaypointCount)
            {
                currentWaypointIndex = 0;
                Debug.Log("맵 최종 지점 도착 오류");
            }
        }
    }

    private void Knockback()
    {
        Debug.Log("넉백");

        // 이전 웨이포인트가 없으면 반응하지 않음
        int previousIndex = currentWaypointIndex - 1;
        if (previousIndex < 0) return;

        Transform target = path.GetWaypoint(previousIndex);
        transform.position = Vector2.MoveTowards(transform.position, target.position, (KnockbackResistance * knockbackPower / KnockbackTime) * Time.deltaTime); //속도 변경 필요

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex--;

            if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    private void Burning()
    {
        Debug.Log($"불탐 : {Health}");
        Health -= burningDamage;
        ChangeColorTemporarily(new Color(1f, 0.5f, 0.5f));
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("죽음");
        ObjectPoolManager.Instance.ReturnObject<EnemyFactory>(this.gameObject);
        GameManager.Instance.commander.AddGold(Reward);
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
        ChangeColorTemporarily(new Color(0.5f, 0.5f, 1f), FrozeTime * freezePower);
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

    public void GetOver()
    {
        isDead = true;
        Debug.Log("적이 넘어감");
        ObjectPoolManager.Instance.ReturnObject<EnemyFactory>(this.gameObject);
    }

    //색상 변경 함수
    private void ChangeColorTemporarily(Color newColor, float duration = 0.2f)
    {
        if (colorChangeCoroutine != null)
            return;

        colorChangeCoroutine = StartCoroutine(ChangeColorRoutine(newColor, duration));
    }

    private IEnumerator ChangeColorRoutine(Color color, float duration)
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
        colorChangeCoroutine = null;
    }
}
