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
    public void TakeDamage(float _damage)
    {
        // Odejmij punkty zdrowia. 0 i _startingHealth to minimalna i maksymalna wartoœæ pasku zdrowia
        _currentHealth = Mathf.Clamp(_currentHealth - _damage, 0, _startingHealth);

        if (_currentHealth <= 0)
        {
            if (!_dead)
            {
                gameObject.SetActive(false); // Kiedy gracz straci ¿ycie, to jest usuwany ze sceny (przyk³ad)
                _dead = true;
            }
        }

    }
}
