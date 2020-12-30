using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text soruText, dogruCevapText, yanlisCevapText;

    [SerializeField]
    private GameObject dogruButon, yanlisButon;

    public Soru[] sorular;
    private static List<Soru> cevaplanmamisSorular;

    private Soru gecerliSoru;

    int dogruSayisi, yanlisSayisi;

    void Start()
    {
        dogruSayisi = 0;
        yanlisSayisi = 0;

        if(cevaplanmamisSorular == null || cevaplanmamisSorular.Count==0)
        {
            cevaplanmamisSorular = sorular.ToList<Soru>();
        }

        RastgeleSoruSec();

        Debug.Log("şu ankki soru :" + gecerliSoru.soru + "ve cevabı :" + gecerliSoru.dogruMu);

        
    }

    void RastgeleSoruSec()
    {
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(320f, .2f).SetEase(Ease.OutBack);
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-320f, .2f).SetEase(Ease.OutBack);

        int randomSoruIndex = Random.Range(0, cevaplanmamisSorular.Count);
        gecerliSoru = cevaplanmamisSorular[randomSoruIndex];

        soruText.text = gecerliSoru.soru;

        if(gecerliSoru.dogruMu)
        {
            dogruCevapText.text = "DOĞRU CEVAPLADINIZ!";
            yanlisCevapText.text = "YANLIŞ CEVALADINIZ!";
        }else 
        {
            dogruCevapText.text = "YANLIŞ CEVAPLADINIZ!";
            yanlisCevapText.text = "DOĞRU CEVALADINIZ!";
        }
    }


    IEnumerator sorularArasiBekleRoutine()
    {
        cevaplanmamisSorular.Remove(gecerliSoru);
        yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (cevaplanmamisSorular.Count <= 0)
        {

        }
        else
        {
            RastgeleSoruSec();
        }
    }


    public void dogruButonaBasildi()
    {
        if(gecerliSoru.dogruMu)
        {
            dogruSayisi++;
        }else
        {
            yanlisSayisi++;
        }
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000f, .2f).SetEase(Ease.InBack);
        StartCoroutine(sorularArasiBekleRoutine());
    }

    public void yanlisButonaBasildi()
    {
        if(!gecerliSoru.dogruMu)
        {
            dogruSayisi++;
        }
        else
        {
            yanlisSayisi++;
        }
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-1000f, .2f).SetEase(Ease.InBack);
        StartCoroutine(sorularArasiBekleRoutine());
    }
}
