using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] Transform _pointA, _pointB;
    [SerializeField] float _speed;


    bool _isMovingLeft = false;
    bool _isStopped = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position == _pointB.position && !_isStopped)
        {
            _isMovingLeft = true;
            _isStopped = true;
        }
        else if (transform.position == _pointA.position && !_isStopped)
        {
            _isMovingLeft = false;
            _isStopped = true;
        }
        else if(_isStopped)
        {
            _speed = 0f;
            StartCoroutine(MovePlatformAfterTime());
        }

        if (_isMovingLeft)
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }


    IEnumerator MovePlatformAfterTime()
    {
        while (_speed == 0)
        {
            yield return new WaitForSeconds(3f);
            _isStopped = false;
            _speed = 5f;
        }
        yield return null;
    }
}