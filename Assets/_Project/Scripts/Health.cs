using TMPro;

namespace _Project.Scripts
{
    public sealed class Health
    {
        private readonly TMP_Text _healthText;
        private int _currentHealth;

        public Health(int startHealth, TMP_Text healthText)
        {
            _healthText = healthText;
            
            SetHealth(startHealth);
        }
        
        public void RemoveHealth()
        {
            SetHealth(_currentHealth - 1);
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
    }
}
