using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Visconde : Champion
{
    protected override void Awake()
    {
        base.Awake();
        championName = "Visconde de Sabugosa";
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (player)
        {
            base.Update();

            //if (isRunning) championAnimator.speed = 2f;
            //else championAnimator.speed = 1;
            //championAnimator.SetFloat("Horizontal", movement.x);
            //championAnimator.SetFloat("Vertical", movement.y);
            //championAnimator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    protected override void FixedUpdate()
    {
        if (player)
        {
            base.FixedUpdate();
        }
    }
}
