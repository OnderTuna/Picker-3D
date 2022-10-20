using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopItem : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private string Durum;
    [SerializeField] private int BonusTopIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ToplayiciSinirObjesi"))
        {
            if (Durum == "Palet")
            {
                _GameManager.PaletleriOrtayaCikar();
                gameObject.SetActive(false);
            }
            else if (Durum == "Bonus")
            {
                _GameManager.BonusTopIslem(BonusTopIndex);
                gameObject.SetActive(false);
            }
        }
    }
}
