using HealthSystem.Property;

namespace HealthSystem
{
    public interface IHealth
    {
        public bool ReadyForDamage { get; }
        public HealthProperty Health { get; }

        public void ApplyHeal(int value);
        public void ApplyDamage(int damage);
    }
}
