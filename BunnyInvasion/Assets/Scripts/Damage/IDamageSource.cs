namespace DamageNamespace
{
    public interface IDamageSource
    {
        public float DealDamage();
        public float GetAttackCooldown();
        public string GetTag();
    }
}
