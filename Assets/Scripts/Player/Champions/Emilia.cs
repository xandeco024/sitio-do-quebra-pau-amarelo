using UnityEngine;

public class Emilia : Champion
{
    [Header("Components")]
    private Camera cam;
    private LineRenderer lineYoYo;
    [SerializeField] private GameObject yoyoPrefab;


    [Header("Hability 1")]
    [SerializeField] private float timeAddYoYo;
    private float timeCountYoYo;
    [SerializeField] private float yoyoSpeed;

    Vector3 directionYoYo;
    private bool keyYoYoIsPressed = false;
    private GameObject yoyoInstantiate;
    private bool canPlayYoYo;

    public bool CanPlayYoYo  { get { return canPlayYoYo; } }

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

            if (yoyoInstantiate != null)
            {
                lineYoYo = yoyoInstantiate.GetComponent<LineRenderer>();

                Vector3 startPosition = transform.position;
                Vector3 endPosition = yoyoInstantiate.transform.position;

                lineYoYo.SetPosition(0, startPosition);
                lineYoYo.SetPosition(1, endPosition);

                lineYoYo.startWidth = 1f;
                lineYoYo.endWidth = 1f;

                if (timeCountYoYo > 0)
                    timeCountYoYo -= 0.1f;

                else if (timeCountYoYo < 0)
                    timeCountYoYo = 0;
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
            YoYoButton(keyYoYoIsPressed);

        }
    }

    private void YoYoButton(bool keyIsPressed) {

        if (keyIsPressed && canPlayYoYo) {
            Vector3 instantiatePosition = new Vector3(transform.position.x * 1.2f,transform.position.y,transform.position.z);
            yoyoInstantiate = Instantiate(yoyoPrefab, transform.position, transform.rotation);
            
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 aimDirection = (mousePosition - transform.position).normalized;

            float angleRotation = Mathf.Atan2(aimDirection.y,aimDirection.x) * Mathf.Rad2Deg;
            yoyoInstantiate.transform.rotation = Quaternion.Euler(0,0,angleRotation);
            directionYoYo = (yoyoInstantiate.transform.right * yoyoSpeed) * Time.fixedDeltaTime;

            canPlayYoYo = false;

            canMove = false;
            timeCountYoYo += timeAddYoYo;
        }

        if (yoyoInstantiate != null)
        {
            if (timeCountYoYo == 0 && !canPlayYoYo)
                directionYoYo = ((transform.position - yoyoInstantiate.transform.position).normalized * yoyoSpeed) * Time.fixedDeltaTime;

            yoyoInstantiate.GetComponent<Rigidbody2D>().velocity = directionYoYo * yoyoSpeed;
        }

        else{
            canPlayYoYo = true;
            canMove = true;
        }
    }
}
