using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabico : Champion
{
    protected override string[] nickNamesArray { get; } = {
    "RabicoAstut„o",
    "Rabico¡giluzado",
    "Furac„oRabicalh„o",
    "RabicoMaluquete",
    "RabicoLigeirinho",
    "TurboRabicoide",
    "RabicoFugidinho",
    "RabicoGuerreir„o",
    "TomaNoRabico",
    "Otaku"
    };

    protected override void Awake()
    {
        base.Awake();
        championName = "Rabico";
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
