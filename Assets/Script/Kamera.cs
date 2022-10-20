using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Target_Offset;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + Target_Offset, .125f);
    }
}
