using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    private LineRenderer _lr;

    [SerializeField]
    private Transform[] _points;

    private void Start()
    {
        _lr = GetComponent<LineRenderer>();
        _lr.positionCount = 2;
    }

    private void Update()
    {
        if (Timer10.paused)
        {
            _lr.enabled = true;
            for (int i = 0; i < _points.Length; i++)
            {
                _lr.SetPosition(i, _points[i].position);
            }
        }
        else
        {
            _lr.enabled = false;
        }
    }
}
