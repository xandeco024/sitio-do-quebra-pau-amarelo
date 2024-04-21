using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public string description;
    public float attackDamage;
    public float magicDamage;
    public float cooldownTime;

    public enum StateSkill{
        ready,
        active,
        cooldown
    }

    public StateSkill state;
}
