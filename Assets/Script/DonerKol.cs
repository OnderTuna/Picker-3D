using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonerKol : MonoBehaviour
{
    bool Don;
    [SerializeField] private float DonusDeger;

    void Update()
    {
        if(Don == true)
        {
            transform.Rotate(0, 0, DonusDeger, Space.Self);
        }
    }

    public void DonmeyeBasla()
    {
        Don = true;
    }
}
