using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Champion : MonoBehaviour
{
    [Header("Components")]
    protected GameManager gameManager;
    protected Animator championAnimator;
    protected Rigidbody2D championRB;
    protected SpriteRenderer championSR;
    protected BoxCollider2D championCol;
    [SerializeField] GameObject basicBulletPrefab;

    [Header("Attributes")]
    [SerializeField] protected string championName;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float maxMana;
    [SerializeField] protected float maxStamina;
    [SerializeField] protected float magicDamage;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackResistance;
    [SerializeField] protected float magicResistance;
    [SerializeField] protected float attackPenetration;
    [SerializeField] protected float magicPenetration;
    [SerializeField] private float moveSpeed;
    protected float staminaDrain = 20;
    protected float staminaRegenRate = 5;
    protected float manaRegenRate = 5;

    [Header("Invisible")]
    protected float health;
    protected float mana;
    protected float stamina;

    [Header("Stats")]
    protected bool player;
    protected bool canAttack = true;
    protected bool interacting;

    [Header("Movement")]
    protected bool isRunning;
    protected Vector2 movement;
    private Color originalColor, damageColor;


    // Variaveis Acessiveis
    public string ChampionName { get { return championName; } }
    public float MaxHealth { get { return maxHealth; } }
    public float Health { get { return health; } set { if (value >= 0) { health = value; } } }
    public float MaxMana { get { return maxMana; } }
    public float Mana { get { return mana; } set { if (value >= 0) { mana = value; } } }
    public float MaxStamina { get { return maxStamina; } }
    public float Stamina { get { return stamina; } set { if (value >= 0) { stamina = value; } } }
    public float AttackDamage { get { return attackDamage; } }
    public float MagicDamage { get { return magicDamage; } }
    public float AttackPenetration { get { return attackPenetration; } }
    public float MagicPenetration { get { return MagicPenetration; } }
    public float AttackResistance { get { return attackResistance; } }
    public float MagicResistance { get { return magicResistance; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public bool Interacting { get { return interacting; } }



    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        championSR = GetComponent<SpriteRenderer>();
        championRB = GetComponent<Rigidbody2D>();
        championAnimator = GetComponent<Animator>();
        championCol = GetComponent<BoxCollider2D>();

        health = maxHealth;
        mana = maxMana;
        stamina = maxStamina;

        originalColor = championSR.color;
        damageColor = Color.red;
    }

    protected virtual void Update()
    {
        /*if (target != null)
        {
            DrawCircle(transform.position, attackRange, Color.red);

            if (canAttack) BasicAttack(target);
        }

        else DrawCircle(transform.position, attackRange, Color.blue);
        */


        if (Input.GetKeyDown(KeyCode.J)) 
        {
            TakeDamage(10, 0, 0, 0, false, transform.position);
            stamina -= 10;
        }

        if(Input.GetMouseButton(0) && canAttack)
        {
            BasicAttack();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            interacting = true;
        }

        else interacting = false;

        DetectMovement();
        Regen();
        //RotateToLookAtCursor();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }

    void DetectMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            isRunning = true;
            stamina -= Time.deltaTime * staminaDrain;
        }

        else
        {
            isRunning = false;
        }
    }

    void HandleMovement()
    {
        Vector2 moveVector = new Vector2(movement.x, movement.y);
        moveVector = moveVector.normalized;

        if (isRunning)
        {
            moveVector = moveVector * moveSpeed * 1.5f;
        }

        else
        {
            moveVector = moveVector * moveSpeed;
        }

        championRB.velocity = moveVector;
    }

    void RotateToLookAtCursor()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float angle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void BasicAttack()
    {
        //Debug.Log("Tentou atacar");

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        Vector2 smallCirclePosition = (Vector2)transform.position + direction * attackRange;
        DrawCircle(smallCirclePosition, .5f, Color.red);

        if (attackRange <= 1.25)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(((Vector2)transform.position + direction * attackRange), .5f);

            foreach (Collider2D enemy in hitEnemies)
            {
               if(enemy.gameObject == this.gameObject)
                {
                    continue;
                }

               if(enemy.gameObject.tag == "Champion")
               {
                    enemy.GetComponent<Champion>().TakeDamage(attackDamage, attackPenetration, 0, 0, false, transform.position);
               }
            }

            //Debug.Log("atacou " + hitEnemies.Length + " inimigos");
        }

        else
        {
            // rangedAttack
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject basicBulletInstance = Instantiate(basicBulletPrefab, ((Vector2)transform.position + direction * attackRange), Quaternion.Euler(new Vector3(0, 0, angle)));
            basicBulletInstance.GetComponent<BasicBullet>().Champion = this;

        }

        canAttack = false;
        StartCoroutine(BasicAttackCD(attackSpeed));
    }

    private IEnumerator BasicAttackCD(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }

    public void TakeDamage(float attackDamage, float attackPenetration, float magicDamage, float magicPenetration, bool knockback, Vector2 enemyPos)
    {
        float damage1 = attackDamage * (1 - (attackResistance - attackPenetration) / 100);
        float damage2 = magicDamage * (1 - (magicResistance - magicPenetration) / 100);

        health -= damage1 + damage2;

        StartCoroutine(Blink());

        if (knockback) Knockback(enemyPos);
    }

    private void Knockback(Vector2 enemyPos)
    {
        Vector2 knockbackDirection = (Vector2)transform.position - enemyPos;
        knockbackDirection = knockbackDirection.normalized;
        championRB.AddForce(knockbackDirection * 10);
    }

    private IEnumerator Blink()
    {
        championSR.color = damageColor;
        yield return new WaitForSeconds(.1f);
        championSR.color = originalColor;
        yield return new WaitForSeconds(.1f);
        championSR.color = damageColor;
        yield return new WaitForSeconds(.1f);
        championSR.color = originalColor;
    }

    protected void DrawCircle(Vector3 position, float radius, Color color)
    {
        int segments = 360;
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            Vector3 point1 = new Vector3(x, y, 0) + position;
            angle += 360f / segments;
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            Vector3 point2 = new Vector3(x, y, 0) + position;
            Debug.DrawLine(point1, point2, color);
        }
    }

    void Regen()
    {
        if(mana < maxMana)
        {
            mana += Time.deltaTime * manaRegenRate;
        }

        if (stamina < maxStamina && !isRunning)
        {
            stamina += Time.deltaTime * staminaRegenRate;
        }
    }

    public void SetPlayer(bool a)
    {
        player = a;
    }
}
