using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedrinho : Champion
{
    protected override string[] nickNamesArray { get; } = {
    "PedraoGames",
    "Pedro",
    "MestrePedrada",
    "PixelPescador",
    "JogadorPedreiro",
    "PedroExploradorX",
    "ProPedrão",
    "PedroMania",
    "EstrategistaPDR",
    "PedroArrebenta"
    };

    protected override void Awake()
    {
        base.Awake();
        championName = "Pedrinho";
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
