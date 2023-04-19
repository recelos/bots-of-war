using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField]private Animator _animator;

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        _animator.SetFloat("Vertical", Input.GetAxis("Vertical"));

        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position = transform.position + horizontal * Time.deltaTime;
        Vector3 vertical = new Vector3(0, Input.GetAxis("Vertical"), 0);
        transform.position = transform.position + vertical * Time.deltaTime;
    }
}
