using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asansor : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private Animator BariyerAnim;

    public void BariyerKaldir()
    {
        BariyerAnim.Play("BariyerKaldir");
    }
    public void Bitti()
    {
        _GameManager.ToplayiciHareketDurumu = true;
    }
}
