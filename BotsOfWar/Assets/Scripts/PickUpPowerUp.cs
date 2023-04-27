using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPowerUp : MonoBehaviour
{
    [SerializeField] float delay = 0.5f;
    Animator animator;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("PickUp"))
        {
            animator = other.GetComponent<Animator>();
            animator.Play("pickUp_anim");
            Destroy(other.gameObject, delay);
        }
    }
}
