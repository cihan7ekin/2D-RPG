using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [Header("General details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType upgradeType;
    [SerializeField] private float coolDown;
    private float lastTimeUsed;

    private void Awake()
    {
        lastTimeUsed = Time.time - coolDown;
    }

    public void SetSkillUpgrade(UpgradeData upgradeData)
    {
        upgradeType = upgradeData.upgradeType;
        coolDown = upgradeData.cooldown;
    }

    public bool Unlocked(SkillUpgradeType upgradeToCheck) => upgradeToCheck == upgradeType;

    public bool CanUseSkill()
    {
        if (OnCooldown())
            return false;

        return true;
    }

    private bool OnCooldown() => Time.time < lastTimeUsed + coolDown;
    public float SetOnCooldown() => lastTimeUsed = Time.time;
    public float SetOnCooldownBy(float cooldownReduction) => lastTimeUsed += cooldownReduction;
}
