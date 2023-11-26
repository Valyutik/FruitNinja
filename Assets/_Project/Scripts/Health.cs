using TMPro;

namespace _Project.Scripts
{
    public sealed class Health
    {
        private readonly int _startHealth;
        private readonly TMP_Text _healthText;
        private int _currentHealth;

        public Health(int startHealth, TMP_Text healthText)
        {
            _startHealth = startHealth;
            _healthText = healthText;
            
            SetHealth(startHealth);
        }
        
        public void RemoveHealth()
        {
            SetHealth(_currentHealth - 1);
        }
        
        public void Restart()
        {
            SetHealth(_startHealth);
        }
        
        public void AddHealth(int value)
        {
            SetHealth(_currentHealth + value);
        }
        
        private void SetHealth(int value)
        {
            _currentHealth = value;
            SetHealthText(value);
        }
        
        private void SetHealthText(int value)
        {
            _healthText.text = value.ToString();
        }
        
        public int GetCurrentHealth()
        {
            return _currentHealth;
        }
    }
}
