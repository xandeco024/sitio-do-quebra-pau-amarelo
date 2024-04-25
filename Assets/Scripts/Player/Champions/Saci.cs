using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saci : Champion
{
    [Header("Flying Kick Skill")]
    private Skill flyingKickSkill;
    [SerializeField] private float speedKick;

    [Header("Speed Of Thunder Skill")]
    private Skill speedOfThunderSkill;
    [SerializeField] TrailRenderer trailOfThunder;
    [SerializeField] private float speedBoostPercentagem;
    [SerializeField] private float timeOfSpeed;
    private float startSpeed;

    [Header("Hurricane Skill")]
    public Skill hurricaneSkill;
    [SerializeField] private GameObject hurricanePrefab;
    [SerializeField] private float speedHurricane;
    private GameObject instanceHurricane;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        startSpeed = MoveSpeed;
        flyingKickSkill = Skills[0];
        speedOfThunderSkill = Skills[1];
        hurricaneSkill = Skills[2];
    }

    protected override void Update()
    {
        if (isPlayer)
        {
            base.Update();

            if (isRunning) championAnimator.speed = 2f;
            else championAnimator.speed = 1;
            championAnimator.SetFloat("Horizontal", movement.x);
            championAnimator.SetFloat("Vertical", movement.y);
            championAnimator.SetFloat("Speed", movement.sqrMagnitude);
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
            SpeedOfThunder();
            Hurricane();
        }
    }

    private void Hurricane()
    {
        if (hurricaneSkill.state == Skill.StateSkill.active && !instanceHurricane)
        {
            Vector2 instantiatePosition = new(transform.position.x * 1.1f, transform.position.y * 1.1f);
            instanceHurricane = Instantiate(hurricanePrefab, instantiatePosition , Quaternion.identity);
            instanceHurricane.transform.parent = transform;
            instanceHurricane.GetComponent<Rigidbody2D>().velocity = MousePosition().normalized * speedHurricane;
        }

        else if(instanceHurricane)
            hurricaneSkill.state = Skill.StateSkill.cooldown;
    }

    private void SpeedOfThunder()
    {
        print(MoveSpeed);
        if (speedOfThunderSkill.state == Skill.StateSkill.active)
        {      
            if(startSpeed == MoveSpeed)
                MoveSpeed += MoveSpeed * speedBoostPercentagem / 100;

            trailOfThunder.emitting = true;
            StartCoroutine(TurnOffSpeedOfThunder());
        }
    }

    private IEnumerator TurnOffSpeedOfThunder()
    {
        yield return new WaitForSeconds(timeOfSpeed);
        if (startSpeed < MoveSpeed)
            MoveSpeed /= ((speedBoostPercentagem + 100) / 100);

        trailOfThunder.emitting = false;
        speedOfThunderSkill.state = Skill.StateSkill.cooldown;
    }
    private Vector2 MousePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2) transform.position;
        return direction;
    }
}
