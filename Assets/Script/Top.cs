using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
        
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ToplariSay"))
        {
            _GameManager.ToplariSay();
        }
    }
}
