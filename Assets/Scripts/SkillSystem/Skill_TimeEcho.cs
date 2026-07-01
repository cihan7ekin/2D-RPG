using UnityEngine;

public class Skill_TimeEcho : Skill_Base
{
    [SerializeField] private GameObject timeEchoPrefab;
    [SerializeField] private float timeEchoDuration;

    [Header("Attack Upgrades")]
    [SerializeField] private int maxAttacks = 3;
    [SerializeField] private float duplicateChance = 0.3f;

    [Header("Heal Wisp Upgrades")]
    [SerializeField] private float damagePercentHealed = 0.3f;
    [SerializeField] private float cooldownReducedInSeconds;
    public float wispMoveSpeed = 10f;

    public float GetPercentageOfDamageHealed()
    {
        if (ShouldBeWisp() == false)
            return 0;

        return damagePercentHealed;
    }

    public float GetCooldownReduceInSeconds()
    {
        if (upgradeType != SkillUpgradeType.TimeEcho_CleanseWisp)
            return 0;

        return cooldownReducedInSeconds;
    }

    public bool CanRemoveNegativeEffects()
    {
        if (upgradeType != SkillUpgradeType.TimeEcho_CooldownWisp)
            return false;

        return true;
    }

    public bool ShouldBeWisp()
    {
        return Unlocked(SkillUpgradeType.TimeEcho_HealWisp) || Unlocked(SkillUpgradeType.TimeEcho_CleanseWisp) || Unlocked(SkillUpgradeType.TimeEcho_CooldownWisp);
    }

    public float GetDuplicateChance()
    {
        if (upgradeType != SkillUpgradeType.TimeEcho_ChanceToDuplicate)
            return 0;

        return duplicateChance;
    }

    public int GetMaxAttack()
    {
        if (Unlocked(SkillUpgradeType.TimeEcho_SingleAttack) || Unlocked(SkillUpgradeType.TimeEcho_ChanceToDuplicate))
            return 1;
        if (Unlocked(SkillUpgradeType.TimeEcho_MultiAttack))
            return maxAttacks;

        return 0;
    }

    public float GetEchoDuration()
    {
        return timeEchoDuration;
    }

    public override void TryUseSkill()
    {
        if (CanUseSkill() == false)
            return;

        CreateTimeEcho();
    }

    public void CreateTimeEcho(Vector3? targetPosition = null)
    {
        Vector3 position = targetPosition ?? transform.position;

        GameObject timeEcho = Instantiate(timeEchoPrefab, position, Quaternion.identity);
        timeEcho.GetComponent<SkillObject_TimeEcho>().SetupEcho(this);
        SetSkillOnCooldown();
    }
}
