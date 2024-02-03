using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Visconde : Champion
{

    protected override string[] nickNamesArray { get; } = {
    "ViscondeSábirol",
    "SábioDigitalizado",
    "MestreViscondido",
    "EstrategistaViscondelícia",
    "VisionárioViscondelicioso",
    "ViscondeDesbravadorzão",
    "LordViscondelicious",
    "ViscondeEspecialistão",
    "DigitalMasterViscondeFera",
    "ViscondeMágicoMente"
    };

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
        if (isPlayer)
        {
            base.Update();
            //PoisonousFood();
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
        }
    }
}
