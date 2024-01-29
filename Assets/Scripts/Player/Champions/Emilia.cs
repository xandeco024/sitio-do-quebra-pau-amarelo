using UnityEngine;
using System.Collections;
public class Emilia : Champion
{
    [Header("Components")]
    private Camera cam;
    private LineRenderer lineYoYo;
    [SerializeField] private GameObject yoyoPrefab;


    [Header("Hability 1")]
    [SerializeField] private float rangeYoYo;
    [SerializeField] private float yoyoSpeed;
    [SerializeField] private float cooldownYoYo;
    Vector3 directionYoYo;
    private GameObject yoyoInstantiate;
    private bool keyYoYoIsPressed = false;
    private bool canApplyCooldown = false;
    private bool canPlayYoYo = true;
    public bool CanPlayYoYo  { get { return canPlayYoYo; } }

    [Header("Hability 2")]
    [SerializeField] private GameObject dollFriendPrefab;
    [SerializeField] private GameObject bulletDollFriendPrefab;
    [SerializeField] private float dollAttackSpeed;
    [SerializeField] private float timeDollDissapear;
    [SerializeField] private float cooldownDollFriend;
    private GameObject dollFriendInstantiate = null;
    private GameObject bulletDollFriendInstantiate;
    private Vector3 directionBullet;
    private bool keyDollIsPressed;
    private bool keyBulletIsPressed;
    private bool canInvokeDoll = true;
    private bool dollCanAttack = true;

    protected override void Awake()
    {
        base.Awake();
        championName = "Emilia";
    }
    protected override void Start()
    {
        base.Start();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    protected override void Update()
    {
        if (isPlayer)
        {
            base.Update();
            keyYoYoIsPressed = Input.GetKeyDown(KeyCode.Z);
            keyDollIsPressed = Input.GetKeyDown(KeyCode.X);
            keyBulletIsPressed = Input.GetMouseButtonDown(0);

            CooldownHabilityYoYo();
            DollFriend();

            if(dollFriendInstantiate != null){
                Destroy(dollFriendInstantiate,timeDollDissapear);
                StartCoroutine("DollFriendCD");
            }
            

            if(yoyoInstantiate != null && !canPlayYoYo)
                StartCoroutine("CooldownHabilityYoYo");

            if (yoyoInstantiate != null)
            {
                lineYoYo = yoyoInstantiate.GetComponent<LineRenderer>();

                Vector3 startPosition = transform.position;
                Vector3 endPosition = yoyoInstantiate.transform.position;

                lineYoYo.SetPosition(0, startPosition);
                lineYoYo.SetPosition(1, endPosition);

                lineYoYo.startWidth = 1f;
                lineYoYo.endWidth = 1f;
            }
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
            if(keyYoYoIsPressed){
                YoYoButton();
            }

            VelocityYoYoButton();
        }
    }

    private void YoYoButton() {

        print(canPlayYoYo);
        if (canPlayYoYo)
        {
            Vector3 instantiatePosition = new Vector3(transform.position.x * 1.5f, transform.position.y, transform.position.z);
            yoyoInstantiate = Instantiate(yoyoPrefab, transform.position, transform.rotation);

            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 aimDirection = (mousePosition - transform.position).normalized;

            float angleRotation = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            yoyoInstantiate.transform.rotation = Quaternion.Euler(0, 0, angleRotation);
            directionYoYo = (yoyoInstantiate.transform.right * yoyoSpeed) * Time.fixedDeltaTime;

            canPlayYoYo = false;
        }  
    }

    private void VelocityYoYoButton(){

        if (yoyoInstantiate != null)
        {
            print("oi");
            if (Vector2.Distance(yoyoInstantiate.transform.position, transform.position) >= rangeYoYo)
                directionYoYo = ((transform.position - yoyoInstantiate.transform.position).normalized * yoyoSpeed) * Time.fixedDeltaTime;

            yoyoInstantiate.GetComponent<Rigidbody2D>().velocity = directionYoYo * yoyoSpeed;
        }
    }

    private void DollFriend()
    {
        if (keyDollIsPressed && canInvokeDoll)
        {
            Vector3 instantiatePosition = new Vector3(transform.position.x * 1.1f, transform.position.y, transform.position.z);
            dollFriendInstantiate = Instantiate(dollFriendPrefab, instantiatePosition, transform.rotation);
            dollFriendInstantiate.transform.parent = transform;
            canInvokeDoll = false;
            canPlayYoYo = false;
        }

        else if (dollFriendInstantiate != null && keyBulletIsPressed && dollCanAttack)
        {
            Vector3 instantiatePosition = new Vector3(transform.position.x * 1.2f, transform.position.y, transform.position.z);
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 aimDirection = (mousePosition - transform.position).normalized;

            float angleRotation = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            bulletDollFriendInstantiate = Instantiate(bulletDollFriendPrefab,instantiatePosition,Quaternion.Euler(0,0,angleRotation));
            dollCanAttack = false;
            StartCoroutine("DollFriendAttackCD");
        }
    }

    private IEnumerator DollFriendCD(){
        yield return new WaitForSeconds(cooldownDollFriend);
        canInvokeDoll = true;   
    }

    private IEnumerator DollFriendAttackCD(){
        yield return new WaitForSeconds(AttackSpeed);
        dollCanAttack = true;
    }

    private IEnumerator CooldownHabilityYoYo() {
       yield return new WaitForSeconds(cooldownYoYo);
       canPlayYoYo = true;
    }
}
