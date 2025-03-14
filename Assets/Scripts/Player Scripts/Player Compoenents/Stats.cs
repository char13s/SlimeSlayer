﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
[System.Serializable]
public class Stats
{
    //Variables
    private int health;
    private int attack;
    private int defense;
    private int stamina;
    private int staminaLeft;

    private int mp;
    private float speed;
    private int healthLeft;

    private int mpLeft;
    private byte level = 1;
    private int exp = 0;
    private int requiredExp;

    private int baseAttack;
    private int baseDefense;
    private int baseMp;
    private int baseHealth;

    private float attackBoost;
    private float defenseBoost;
    private int mpBoost;
    private int healthBoost;

    private int swordLevel;
    private int demonFistLevel;
    private int swordProficency;

    private byte kryllLevel;

    private int abilitypoints;
    private int transformationMod;
    private int darkLvl;
    private int electricLvl;
    private int iceLvl;
    private int lightLvl;
    private int fireLvl;
    private int teleportLvl;
    private int timeLvl;

    //Events
    public static event UnityAction onHealthChange;
    public static event UnityAction onStaminaLeft;
    public static event UnityAction onLevelUp;
    public static event UnityAction onShowingStats;
    public static event UnityAction onBaseStatsUpdate;
    public static event UnityAction onObjectiveComplete;
    public static event UnityAction<int> onPowerlv;
    public static event UnityAction sendSpeed;
    public static event UnityAction<int> onOrbGain;
    public static event UnityAction<int> sendMp;
    public static event UnityAction<int> updateMP;
    public static event UnityAction increase;
    public static event UnityAction decrease;
    //Properties
    #region Getters and Setters
    public int Health { get { return health; } set { health = Mathf.Max(0, value); } }
    public int HealthLeft { get { return healthLeft; } set { healthLeft = Mathf.Clamp(value, 0, health); CalculateStatsOutput(); if (onHealthChange != null) { onHealthChange(); } } }
    //public int MPLeft { get { return MpLeft; } set { MpLeft = Mathf.Clamp(value, 0, mp); CalculateStatsOutput(); if (onMPLeft != null) { onMPLeft(); } } }

    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int MP { get { return mp; } set { mp = value; if(sendMp!=null) sendMp(mp); } }//
    public float Speed { get { return speed; } set { speed = value; sendSpeed.Invoke(); } }
    public byte Level { get => level; set => level = value; }
    public int Exp { get => exp; set { exp = value; UpdateUi(); } }
    public int BaseAttack { get => baseAttack; set { baseAttack = Mathf.Clamp(value, 0, 300); CalculateStatsOutput(); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public int BaseDefense { get => baseDefense; set { baseDefense = Mathf.Clamp(value, 0, 300); CalculateStatsOutput(); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public int BaseMp { get => baseMp; set { baseMp = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public int BaseHealth { get => baseHealth; set { baseHealth = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public float AttackBoost { get => attackBoost; set { attackBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); CalculateStatsOutput(); SetStats(); } }
    public float DefenseBoost { get => defenseBoost; set { defenseBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); CalculateStatsOutput(); SetStats(); } }
    public int MpBoost { get => mpBoost; set { mpBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); CalculateStatsOutput(); } }
    public int HealthBoost { get => healthBoost; set { healthBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); CalculateStatsOutput(); } }

    public int RequiredExp { get => requiredExp; set => requiredExp = value; }
    public int Abilitypoints { get => abilitypoints; set { abilitypoints = value; if (onBaseStatsUpdate != null) onBaseStatsUpdate(); if(onOrbGain!=null) onOrbGain(abilitypoints); } }

    public int SwordProficency { get => swordProficency; set => swordProficency = value; }
    public int SwordLevel { get => swordLevel; set => swordLevel = value; }
    public byte KryllLevel { get => kryllLevel; set => kryllLevel = value; }
    public int MPLeft { get => mpLeft; set { mpLeft = Mathf.Clamp(value, 0, mp); if(updateMP!=null) updateMP(MPLeft);
            } }

    public int DarkLvl { get => darkLvl; set => darkLvl = value; }
    public int ElectricLvl { get => electricLvl; set => electricLvl = value; }
    public int IceLvl { get => iceLvl; set => iceLvl = value; }
    public int LightLvl { get => lightLvl; set => lightLvl = value; }
    public int FireLvl { get => fireLvl; set => fireLvl = value; }
    public int TeleportLvl { get => teleportLvl; set => teleportLvl = value; }
    public int TimeLvl { get => timeLvl; set => timeLvl = value; }
    public int Stamina { get => stamina; set => stamina = value; }
    public int StaminaLeft { get => staminaLeft; set { staminaLeft = Mathf.Clamp(value, 0, 100); if(onStaminaLeft!=null) onStaminaLeft(); } }

    public int TransformationMod { get => transformationMod; set { transformationMod = Mathf.Clamp(value, 1, 100); CalculateStatsOutput(); } }

    public int CalculateExpNeed() { int expNeeded = 4 * (Level * Level * Level); return Mathf.Abs(Exp - expNeeded); }
    public int ExpCurrent() { return Exp - (4 * ((Level - 1) * (Level - 1) * (Level - 1))); }
    #endregion
    public void AddExp(int points) {
        exp += points;
    }
    public void DisplayAbilities() {
        if (onShowingStats != null) {
            onShowingStats();
        }
    }
    public void Start() {
        SetStats();
        // Player.weaponSwitch += SetStats;
        //PerfectGuardBox.sendAmt += ChangeMpLeft;
        //PlayerInputs.transformed += OnTransformation;
        //SkillTreeNode.sendOrbs += AdjustOrbs;
        //Enemy.sendOrbs += AdjustOrbs;
        PerfectGuardBox.sendAmt += AdjustMp;
        GuardBox.sendAmt += AdjustMp;
        //HealRelic.heal += Heal;
        //EnemyFireball.dmg += OutsideDamage;
        //PlayerAnimationEvents.transform += OnTransformation;
        if (onHealthChange != null) {
            onHealthChange();
        }
        if (onLevelUp != null) { onLevelUp(); }
    }
    private void UpdateUi() {
        if (onHealthChange != null) {
            onHealthChange();
        }

        if (onLevelUp != null) { onLevelUp(); }
    }
    private void SetStats() {
        // + mpBoost
        TransformationMod = 1;
        Stamina = 99;
        StaminaLeft = Stamina;
        baseHealth = 120;
        healthLeft = baseHealth;
        baseMp = 30;
        Health = baseHealth;// + healthBoost
        MP = baseMp+MpBoost;
        MPLeft = 0;
        BaseAttack = 10;
        BaseDefense = 5;

        if (onHealthChange != null) {
            onHealthChange();
        }
        
        //onMPLeft.Invoke();
        //CalculateStatsOutput();

    }
    //private void ChangeMpLeft(int amt) => MPLeft += amt;
    private void CalculateStatsOutput() {
        //calculated everytime health or Mp is changed.
        /*Attack=(HealthLeft/Health+mpLeft)+baseAttack;
        Defense=(HealthLeft/Health+mpLeft)+baseDefense;
        onPowerlv.Invoke((HealthLeft / Health + mpLeft) * (baseDefense+baseAttack));*/
       // Debug.Log("Attack" + Attack);
        Attack = (int)(BaseAttack * (1 + AttackBoost) * transformationMod);
        Defense =(int)(BaseDefense * (1 + DefenseBoost * transformationMod));
        MP = baseMp + MpBoost;
        Health =baseHealth+ HealthBoost;
    }
    private void AddToAttackBoost() {
        //Upgrading Attacks on Attack boost affect here
        //
        CalculateStatsOutput();
    }
    private void AddToDefenseBoost() {
        //Upgrading Defense boost affect here
        //
        CalculateStatsOutput();
    }
    private void OnTransformation(bool val) {
        if (val) {
            AttackBoost = BaseAttack;
        }
        else {
            AttackBoost = 0;
        }
        //An Mp boost should be given here which would contribute to an attack otput boost
        //but also drains Mp and stamina the longer its held.
        //CalculateStatsOutput();
    }
    private void AdjustOrbs(int val) {
        Abilitypoints += val;
    }
    public void IncreaseHealth() {
        Health += 10;
        //Debug.Log(Health);
    }
    public void IncreaseMPSlowly() {
        MPLeft += (mp / 100);
        increase.Invoke();
    }
    public void AdjustMp(int amt) {
        MPLeft += amt;
    }
    public void DecreaseMPSlowly() {
        MPLeft -= (mp / 100);
        decrease.Invoke();
    }
    private void AddToStats() {
        if (mpLeft / (mp / 2) > 1)
            AttackBoost = mpLeft / (mp / 2);
    }
    private void Heal() {
        HealthLeft += (int)(Health * 0.2);
    }
    private void OutsideDamage(int val) {
        HealthLeft -= val;
    }
}
