using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 20f;

    

    private void Update()
    {
        if (transform.rotation == Quaternion.Euler(0, -180, 0))
        {
            transform.rotation = Quaternion.identity;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180f, 0), _rotateSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
