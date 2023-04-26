using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField]private Animator _animator;
    private bool _facesRight;

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            _facesRight = false;
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            _facesRight = true;
        }

        _animator.SetFloat("Horizontal", movement.x);
        _animator.SetFloat("Vertical", movement.y);
        _animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;
    }

    public bool isFacingRight(){
        return _facesRight;
    }
}
