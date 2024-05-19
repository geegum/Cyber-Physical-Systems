﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleIKSolver2 : MonoBehaviour
{

    public Transform pivot2, upper2, lower2, effector2, tip2;
    public Vector3 target2 = Vector3.forward;
    public Vector3 normal2 = Vector3.up;


    float upperLength, lowerLength, effectorLength, pivotLength;
    Vector3 effectorTarget, tipTarget;

    void Reset()
    {
        pivot2 = transform;
        try
        {
            upper2 = pivot2.GetChild(0);
            lower2 = upper2.GetChild(0);
            effector2 = lower2.GetChild(0);
            tip2 = effector2.GetChild(0);
        }
        catch (UnityException)
        {
            Debug.Log("Could not find required transforms, please assign manually.");
        }
    }

    void Awake()
    {
        upperLength = (lower2.position - upper2.position).magnitude;
        lowerLength = (effector2.position - lower2.position).magnitude;
        effectorLength = (tip2.position - effector2.position).magnitude;
        pivotLength = (upper2.position - pivot2.position).magnitude;
    }


    void Solve()
    {
        var pivotDir = effectorTarget - pivot2.position;
        pivot2.rotation = Quaternion.LookRotation(pivotDir);

        var upperToTarget = (effectorTarget - upper2.position);
        var a = upperLength;
        var b = lowerLength;
        var c = upperToTarget.magnitude;

        var B = Mathf.Acos((c * c + a * a - b * b) / (2 * c * a)) * Mathf.Rad2Deg;
        var C = Mathf.Acos((a * a + b * b - c * c) / (2 * a * b)) * Mathf.Rad2Deg;

        if (!float.IsNaN(C))
        {
            var upperRotation = Quaternion.AngleAxis((-B), Vector3.right);
            upper2.localRotation = upperRotation;
            var lowerRotation = Quaternion.AngleAxis(180 - C, Vector3.right);
            lower2.localRotation = lowerRotation;
        }
        var effectorRotation = Quaternion.LookRotation(tipTarget - effector2.position);
        effector2.rotation = effectorRotation;
    }

    void Update()
    {
        tipTarget = target2;
        effectorTarget = target2 + normal2 * effectorLength;
        Solve();
    }

}