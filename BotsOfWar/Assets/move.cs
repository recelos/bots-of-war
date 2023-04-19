using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    
    [SerializeField] float steerSpeed = 1f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float boostSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        float moveAmountX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveAmountY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(moveAmountX, 0, 0);
        transform.Translate(0, moveAmountY, 0);
    }
}
