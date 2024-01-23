using UnityEngine;

public class Emilia : Champion
{
    [Header("Components")]
    [SerializeField] private GameObject yoyo;
    [SerializeField] private LineRenderer lineYoYo;

    private bool keyYoYoIsPressed = false;
    [SerializeField] private float timeYoYo;
    [SerializeField] private float yoyoSpeed;
    private bool canPlayYoYo;
    protected override void Awake()
    {
        base.Awake();
        championName = "Emilia";
    }
    protected override void Start()
    {
        base.Start();
        lineYoYo.positionCount = 2;
    }

    protected override void Update()
    {
        if (isPlayer)
        {
            base.Update();
            keyYoYoIsPressed = Input.GetKeyDown(KeyCode.Z);
            //PoisonousFood();
        }

        else
        {
            BasicIA();
        }
    }

    
    protected override void FixedUpdate()
    {
        if(isPlayer)
        {
            base.FixedUpdate();
            YoYoButton(keyYoYoIsPressed);
        }
    }
    
    private void YoYoButton(bool keyIsPressed){
        Vector2 directionYoYo = HandleMovement();
        lineYoYo.SetPosition(0,yoyo.transform.position);
        lineYoYo.SetPosition(1,yoyo.transform.position + Vector3.down * 2);

        //primeiro instanciamos o ioiô caso ele aperte o butão da habilidade definimos uma direção para ele ir
        if(keyIsPressed){
            canMove = false;
            canPlayYoYo = false;

            yoyo.GetComponent<Rigidbody2D>().velocity = directionYoYo * yoyoSpeed * Time.fixedDeltaTime;

            while(timeYoYo > 0)
                timeYoYo -= 0.1f;
            
            //depois que o tempo do lançamento acabar a gente faz o IoIo retornar ao player;
            if(timeYoYo == 0){
                directionYoYo = (Vector2) transform.position - directionYoYo;
                yoyo.GetComponent<Rigidbody2D>().velocity = directionYoYo * yoyoSpeed *  Time.fixedDeltaTime;
                
                lineYoYo.SetPosition(0,transform.position);
                lineYoYo.SetPosition(1,transform.position + Vector3.down * 2);
            }
        }
    }
}
