using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _startingHealth;
    private float _currentHealth;
    private bool _dead;

    private void Awake()
    {
        _currentHealth = _startingHealth;
        _dead = false;
    }
    public void TakeDamage(float damage)
    {
        // Subtract health points. Set 0 and _startingHealth as the minimum and maximum values for the health bar
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _startingHealth);

        if (_currentHealth <= 0)
        {
            if (!_dead)
            {
                gameObject.SetActive(false); // When the player loses all their health, they are removed from the scene (example)
                _dead = true;
            }
        }

    }
}
