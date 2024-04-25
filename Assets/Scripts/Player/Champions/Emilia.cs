using System.Collections;
using UnityEngine;
public class Emilia : Champion
{
    [Space]
    [Header("YoYo Skill")]
    public Skill yoyoSkill;
    [SerializeField] private AudioClip throwYoYoSound;
    [SerializeField] private AudioClip backYoYoSound;
    [SerializeField] private LineRenderer lineYoYo;
    [SerializeField] private GameObject yoyoPrefab;
    [SerializeField] private float rangeYoYo;
    [SerializeField] private float speedYoYo;
    public GameObject instanceYoYo;
    public bool yoyoReturn = false;
    private Vector3 yoyoDirection;

    [Space]
    [Header("Doll Friend Skill")]
    private Skill dollFriendSkill;
    [SerializeField] private GameObject dollFriendPrefab;
    [SerializeField] private GameObject bulletDollPrefab;
    [SerializeField] private float cooldownBulletDoll;
    [SerializeField] private float timeActiveDoll;
    [SerializeField] private float bulletDollSpeed;
    [SerializeField] private float bulletDollLife;
    private GameObject instanceDollFriend;
    private GameObject instanceBulletDoll;
    private bool canDollAttack = true;
    private bool dollIsActive = false;

    [Space]
    [Header("Scream Bullet Skill")]
    private Skill screamBulletSkill;
    [SerializeField] private GameObject screamBulletPrefab;
    private GameObject instanceScreamBullet;
    [SerializeField] private float screamBulletLife;
    protected override void Awake()
    {
        base.Awake();
        championName = "Emilia";
    }

    protected override void Start()
    {
        base.Start();
        yoyoSkill = Skills[0];
        dollFriendSkill = Skills[1];
        screamBulletSkill = Skills[2];
    }

    protected override void Update()
    {
        if (isPlayer)
        {
            base.Update();

            if(dollIsActive && instanceDollFriend)
                Destroy(instanceDollFriend, timeActiveDoll);
        }

        else
        {
            BasicIA();
        }
    }

    protected override void FixedUpdate()
    {
        if (isPlayer)
        {
            base.FixedUpdate();
            YoYoButton();
            DollFriend();
            ScreamBullet();
        }
    }

    private void YoYoButton()
    {
        if(yoyoSkill.state == Skill.StateSkill.active)
        {
            if(!instanceYoYo)
            {
                instanceYoYo = Instantiate(yoyoPrefab, transform.position,Quaternion.identity);
                instanceYoYo.transform.parent = transform;
                instanceYoYo.transform.rotation = AimDirection();
                instanceYoYo.GetComponent<YoYoButton>().audioSource.clip = throwYoYoSound;
                instanceYoYo.GetComponent<YoYoButton>().audioSource.Play();
                yoyoDirection = instanceYoYo.transform.right.normalized;
            }

            else
            {
                if (Vector3.Distance(instanceYoYo.transform.position, transform.position) >= rangeYoYo)
                {
                    yoyoReturn = true;
                    yoyoDirection = (transform.position - instanceYoYo.transform.position).normalized;
                    instanceYoYo.GetComponent<YoYoButton>().audioSource.clip = backYoYoSound;
                    instanceYoYo.GetComponent<YoYoButton>().audioSource.Play();
                }

                instanceYoYo.GetComponent<Rigidbody2D>().velocity = yoyoDirection * speedYoYo;
                LineYoYo(); 
            }
        }
    }

    private void LineYoYo()
    {
        lineYoYo = instanceYoYo.GetComponent<LineRenderer>();

        Vector3 startPosition = transform.position;
        Vector3 endPosition = instanceYoYo.transform.position;

        lineYoYo.SetPosition(0, startPosition);
        lineYoYo.SetPosition(1, endPosition);

        lineYoYo.startWidth = 1f;
        lineYoYo.endWidth = 1f;
    }

    private void DollFriend()
    {
        if (dollFriendSkill.state == Skill.StateSkill.active)
        {
           
            if (!instanceDollFriend && dollIsActive == false)
            {
                Vector2 instancePosition = new Vector2(transform.position.x * 1.1f, transform.position.y);
                instanceDollFriend = Instantiate(dollFriendPrefab, instancePosition, Quaternion.identity);
                instanceDollFriend.transform.parent = transform;
                dollIsActive = true;
            }

            else if(!instanceDollFriend && dollIsActive == true)
            {
                dollIsActive = false;
                dollFriendSkill.state = Skill.StateSkill.cooldown;
            }

           

            if (Input.GetMouseButtonDown(0) && canDollAttack)
            {
                canDollAttack = false;
                instanceBulletDoll = Instantiate(bulletDollPrefab, instanceDollFriend.transform.position, AimDirection());
                instanceBulletDoll.transform.parent = instanceDollFriend.transform;
                instanceBulletDoll.GetComponent<Rigidbody2D>().velocity = instanceBulletDoll.transform.right * bulletDollSpeed;
                StartCoroutine(DollAttackCD(cooldownBulletDoll));
            }
        }
    }
    
    private void ScreamBullet()
    {
        if(screamBulletSkill.state == Skill.StateSkill.active && !instanceScreamBullet)
        {
            instanceScreamBullet = Instantiate(screamBulletPrefab, transform.position, Quaternion.identity);
            instanceScreamBullet.transform.parent = transform;
            instanceScreamBullet.transform.rotation = AimDirection();
        }

        if (instanceScreamBullet){
            Destroy(instanceScreamBullet, screamBulletLife);
            screamBulletSkill.state = Skill.StateSkill.cooldown;
        }
    }
    private IEnumerator DollAttackCD(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        canDollAttack = true;
    }

    private Quaternion AimDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimPosition = (mousePosition - transform.position).normalized;
        float rotationZ = Mathf.Atan2(aimPosition.y,aimPosition.x) *Mathf.Rad2Deg;
        return Quaternion.Euler(0,0, rotationZ);
    }
}
