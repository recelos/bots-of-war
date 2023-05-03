using System.Runtime.InteropServices;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _startingHealth;
    private float _currentHealth;
    private bool _dead;
    private bool _takingDamage;
    private Transform _healthBar;
    private float _delta;

    private void Awake()
    {
        _currentHealth = _startingHealth;
        _dead = false;
        _takingDamage = false;
        _healthBar = transform.Find("HealthBarKeeper/HealthBar1");
        _delta = 0;
    }
    public void TakeDamage(float damage)
    {
        if (!_takingDamage)
        {
            var proportion = damage / _startingHealth;
            var barScale = _healthBar.transform.localScale;
            var barPosition = _healthBar.transform.position;

            if (_currentHealth == _startingHealth)
                _delta = barScale.x * proportion;

            barScale.x -= _delta;
            barPosition.x -= _delta / 2;
            _healthBar.transform.localScale = barScale;
            _healthBar.transform.position = barPosition;


            // Subtract health points. Set 0 and _startingHealth as the minimum and maximum values for the health bar
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _startingHealth);

            // Don't hurt the player multiple times with the same bullet
            _takingDamage = true;

        }

        // For testing only!
        Debug.Log(_currentHealth);

        if (_currentHealth <= 0)
        {
            if (!_dead)
            {
                // The dead animation starts
                _animator.SetTrigger("Dead");
                var comp = _healthBar.GetComponentInParent<HealthBar>();
                comp.DeactivateBar();
                _dead = true;
            }
        }
    }

    public void SetTakingDamage(bool takingDamage)
    {
        _takingDamage = takingDamage;
    }

    private void DeactivateBot()
    {
        // When the player loses all their health, they are removed from the scene
        gameObject.SetActive(false);
    }
}
