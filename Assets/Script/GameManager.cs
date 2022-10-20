using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]
public class TopAlaniTeknik›slemler //Bu sayede checkpoint islemlerini yonet.
{
    public Animator TopAlaniAsansor;
    public TextMeshProUGUI SayiText;
    public int AtilmasiGerekenTop;
    public GameObject[] Toplar;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Toplayici;
    [SerializeField] private GameObject[] ToplayiciPaletler;
    [SerializeField] private GameObject[] BonusToplar;
    bool PaletlerVarMi;
    [SerializeField] private GameObject TopKontrolObjesi;
    public bool ToplayiciHareketDurumu;

    int AtilanTopSayisi;
    int ToplamCheckPointSayisi;
    int MevcutCheckPointIndex;
    float ParmakPozX;

    [SerializeField] private List<TopAlaniTeknik›slemler> _TopAlaniTeknik›slemler = new List<TopAlaniTeknik›slemler>(); //Bir level icerisinde birden fazla checkpoint var. Bu checkpoint islemlerini bu class listesi sayesinde yonet.

    void Start()
    {
        ToplamCheckPointSayisi = _TopAlaniTeknik›slemler.Count - 1;
        ToplayiciHareketDurumu = true;
        //_TopAlaniTeknik›slemler[0].SayiText.text = AtilanTopSayisi + "/" + _TopAlaniTeknik›slemler[0].AtilmasiGerekenTop;

        for (int i = 0; i < _TopAlaniTeknik›slemler.Count; i++)
        {
            _TopAlaniTeknik›slemler[i].SayiText.text = AtilanTopSayisi + "/" + _TopAlaniTeknik›slemler[i].AtilmasiGerekenTop;
        }
    }

    void Update()
    {
        if (ToplayiciHareketDurumu)
        {
            Toplayici.transform.position += 5f * Time.deltaTime * Toplayici.transform.forward;

            if (Time.timeScale != 0)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Toplayici.transform.position = Vector3.Lerp(Toplayici.transform.position, new Vector3(Toplayici.transform.position.x - .1f, Toplayici.transform.position.y, Toplayici.transform.position.z), .05f);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    Toplayici.transform.position = Vector3.Lerp(Toplayici.transform.position, new Vector3(Toplayici.transform.position.x + .1f, Toplayici.transform.position.y, Toplayici.transform.position.z), .05f);
                }
            }

            if (Time.timeScale != 0) //Mobil icin.
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 TouchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            ParmakPozX = TouchPos.x - Toplayici.transform.position.x;
                            break;
                        case TouchPhase.Moved:
                            if (TouchPos.x - ParmakPozX > -1.4 && TouchPos.x - ParmakPozX < 1.4)
                            {
                                Toplayici.transform.position = Vector3.Lerp(Toplayici.transform.position, new Vector3(TouchPos.x - ParmakPozX, Toplayici.transform.position.y, Toplayici.transform.position.z), 5f);
                            }
                            break;
                    }
                }
            }
        }        
    }

    public void SiniraGelindi()
    {
        if(PaletlerVarMi)
        {
            foreach (var item in ToplayiciPaletler)
            {
                item.SetActive(false);
            }
        }

        ToplayiciHareketDurumu = false;

        Collider[] ColliderHit = Physics.OverlapBox(TopKontrolObjesi.transform.position, TopKontrolObjesi.transform.localScale / 2, Quaternion.identity); //Sinira geldiimiz an bir box olusturmak istiyoruz.

        int i = 0;
        while (i < ColliderHit.Length) //Menzile dahil objelere ulas.
        {
            ColliderHit[i].gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, .8f), ForceMode.Impulse);
            i++;
        }
        //Debug.Log(i);

        Invoke(nameof(AsamaKontrol), 2f); //Toplar 2 saniye icerisinde alana dustugu icin 2 saniye sonra calis dedik.
    }

    //public void OnDrawGizmos() //Belirli bir koordinata gˆre bize cizgi saglar.
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(TopKontrolObjesi.transform.position, TopKontrolObjesi.transform.localScale);
    //}

    public void ToplariSay()
    {
        AtilanTopSayisi++;
        //_TopAlaniTeknik›slemler[0].SayiText.text = AtilanTopSayisi + "/" + _TopAlaniTeknik›slemler[0].AtilmasiGerekenTop;

        _TopAlaniTeknik›slemler[MevcutCheckPointIndex].SayiText.text = AtilanTopSayisi + "/" + _TopAlaniTeknik›slemler[MevcutCheckPointIndex].AtilmasiGerekenTop;
    }

    void AsamaKontrol()
    {
        if (AtilanTopSayisi >= _TopAlaniTeknik›slemler[MevcutCheckPointIndex].AtilmasiGerekenTop)
        {
            foreach (var item in _TopAlaniTeknik›slemler[MevcutCheckPointIndex].Toplar)
            {
                item.SetActive(false);
            }

            _TopAlaniTeknik›slemler[MevcutCheckPointIndex].TopAlaniAsansor.Play("Asansor");

            if (MevcutCheckPointIndex == ToplamCheckPointSayisi)
            {
                Debug.Log("Win!");
                Time.timeScale = 0;
            }
            else
            {
                MevcutCheckPointIndex++;
                AtilanTopSayisi = 0;
            }
        }
        else
        {
            Debug.Log("Kaybettin");
        }
    }

    public void PaletleriOrtayaCikar()
    {
        PaletlerVarMi = true;
        foreach (var item in ToplayiciPaletler)
        {
            item.SetActive(true);
        }
    }

    public void BonusTopIslem(int BonusTopIndex)
    {
        BonusToplar[BonusTopIndex].SetActive(true);
    }
}
