// This is rather additional script to make health bar work properly. It makes health bar not moving when the player can change
// direction of its movement (Update() method).

using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform _healthBar;
    private bool _directionChange;
    private float _posX;

    private void Start()
    {
        _healthBar = transform.Find("HealthBar1");
    }

    // Make the health bar not moving alongside with the player
    private void Update()
    {
        _posX = _healthBar.transform.localPosition.x;
        if (transform.parent.localScale.x == -1 && !_directionChange)
        {
            _healthBar.transform.localPosition = new Vector3(_posX * -1f, _healthBar.transform.localPosition.y,
                _healthBar.transform.localPosition.z);
            _directionChange = true;
        }
        else if(transform.parent.localScale.x == 1 && _directionChange)
        {
            _healthBar.transform.localPosition = new Vector3(_posX * -1f, _healthBar.transform.localPosition.y,
                _healthBar.transform.localPosition.z);
            _directionChange = false;
        }
    }

    // The method is used in PlayerHealth.cs script. It disables health bar after the player is dead.
    public void DeactivateBar()
    {
        var children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            children[i] = transform.GetChild(i).gameObject;

        foreach (GameObject child in children)
            child.SetActive(false);

    }
}
