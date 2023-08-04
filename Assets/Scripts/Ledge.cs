using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField] Transform _handPosition;
    [SerializeField] Vector3 _handPositionOffset;

    [SerializeField] Transform _finalStandPosition;
    [SerializeField] Vector3 _finalStandPositionOffset;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ledge_Grabber"))
        {
            var player = other.GetComponentInParent<Player>();

            if (player == null)
                return;

            player.SetLedgeGrabPosition(_handPosition.position + _handPositionOffset, _finalStandPosition.position + _finalStandPositionOffset);

        }
    }
}
