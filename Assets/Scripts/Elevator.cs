using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    [SerializeField] Transform _pointA, _pointB, _pointC;
    [SerializeField] float _speed;

    [SerializeField] List<Transform> _floorLevels;
    [SerializeField] int _floorLevel = 2;

    bool _isMovingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_floorLevel <= 0)
            _floorLevel = 0;

        if (_floorLevel >= 2)
            _floorLevel = 2;

        if (transform.position == _pointC.position)
            _isMovingDown = false;
        else if (transform.position == _pointA.position)
            _isMovingDown = true;


        if(transform.position == _floorLevels[_floorLevel].position)
        {
            if(_isMovingDown)
                _floorLevel++;
            else
                _floorLevel--;

            StartCoroutine(StopAtFloorLevel());
        }

        transform.position = Vector3.MoveTowards(transform.position, _floorLevels[_floorLevel].position, _speed * Time.deltaTime);
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


    IEnumerator StopAtFloorLevel()
    {
        _speed = 0f;
        yield return new WaitForSeconds(5f);
        _speed = 2f;
    }
}
