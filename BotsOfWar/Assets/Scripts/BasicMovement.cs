using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField]private Animator _animator;

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        _animator.SetFloat("Horizontal", movement.x);
        _animator.SetFloat("Vertical", movement.y);
        _animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;
    }
}
