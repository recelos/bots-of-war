// The script has all the neccesary methods for bots to track their health status

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private float _startingHealth;
    private float _currentHealth;
    public bool dead;
    private Transform _healthBar; // health bar of the player
    private float _healthBarDecrement; // how much should the health bar decrease after receiving a single hit

    private void Awake()
    {
        _currentHealth = _startingHealth;
        dead = false;
        _healthBar = transform.Find("HealthBarKeeper/HealthBar1");
        _healthBarDecrement = 0;
    }

    public void TakeDamage(float damage)
    {
        // Take all necessary actions to modify health bar
        HealthBarManipulation(damage);

        // Subtract health points. Set 0 and _startingHealth as the minimum and maximum values for the health bar
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _startingHealth);

            // For testing only!
            // Debug.Log(_currentHealth);

        // Take all the neccesary actions when the player is dead or not
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (_currentHealth <= 0 && !dead)
        {
            //this.GetComponent<BasicMovement>().enabled = false;
            // The dead animation starts
            _animator.SetTrigger("Dead");
            var comp = _healthBar.GetComponentInParent<HealthBar>();
            comp.DeactivateBar();
            dead = true;
        }
    }

    private void HealthBarManipulation(float damage)
    {
        var proportion = damage / _startingHealth;
        var barScale = _healthBar.transform.localScale;
        var barPosition = _healthBar.transform.position;

        if (_currentHealth == _startingHealth)
            _healthBarDecrement = barScale.x * proportion;

        barScale.x -= _healthBarDecrement; // reduce health bar
        barPosition.x -= _healthBarDecrement / 2; // change the position of health bar
        _healthBar.transform.localScale = barScale;
        _healthBar.transform.position = barPosition;
    }

    private void DeactivateBot()
    {
        // When the player loses all their health, they are removed from the scene
        gameObject.SetActive(false);
    }
    
    public void SetHealth(float health)
    {
        _startingHealth = health;
        _currentHealth = _startingHealth;
    }
    public float GetHealth(){
        return _currentHealth;
    }
}
