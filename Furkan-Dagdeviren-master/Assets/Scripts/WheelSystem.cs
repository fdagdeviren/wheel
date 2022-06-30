using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class WheelSystem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _wheel;
    [SerializeField] private GameObject _pinBody;
    [SerializeField] private Text _text;

    [SerializeField] List<GameObject> Gifts = new List<GameObject>();
    [SerializeField] List<TextMeshProUGUI> Texts = new List<TextMeshProUGUI>();

    private int _amount;
    private int _giftAmount;

    private string finaltext;

    private bool _wayLeft;
    private bool _finish;

    private void Start()
    {
        _amount = 0;
        _pinBody.GetComponent<PinBody>().enabled = false;
        _finish = false;
        _wayLeft = false;
    }
    //Sonda ortaya cekme ve koseye çekme DoTween ile de yapılabilirdi,moveTowers ile de bir cok farkli yolu var
    private void Update()
    {
        if (_finish)
        {
                Texts[_giftAmount - 1].enabled = false;
            if (_wayLeft)
                Gifts[_giftAmount - 1].transform.Translate(new Vector2(-3f, -4) * 2f * Time.deltaTime);
            else
                Gifts[_giftAmount - 1].transform.Translate(new Vector2(0, -1) * 1.3f * Time.deltaTime);
        }
    }
    //Degerleri sifirlama
    public void Reset()
    {
        Texts[_giftAmount - 1].enabled = true;
        _amount = 0;
        _pinBody.GetComponent<PinBody>().enabled = false;
        _finish = false;
        _wayLeft = false;
        _text.text = "Spin";
    }
    //Her cagirdigimizda Random olarak cevirdim carki
    public void AddTorqueImpulse(float angularChangeInDegrees)
    {
        if (_amount == 0)
        {
            _amount = -1;
            StartCoroutine(coroutineA());
            var impulse = ((angularChangeInDegrees * Mathf.Deg2Rad) * _rigidbody.inertia) * Random.Range(0.5f, 4f);
            _rigidbody.AddTorque(impulse, ForceMode2D.Impulse);
        }
        else if(_amount==1)
        {
            Gifts[_giftAmount-1].transform.localScale = Gifts[_giftAmount-1].transform.localScale * 2.5f;
            _finish = true;
            StartCoroutine(coroutineB());
            _amount++;
        }
    }
    // Genel olarak 8. saniye de bitiyor dönme islemi 9. saniyeden sonra calistirdim, carkin hizina bakarakta yapilabilirdi
    //GetComponent kullanmayı cok tercih etmiyorum normalde ama degisiklik fazla yapmamak icin GetComponent kullandim
    IEnumerator coroutineA()
    {
        yield return new WaitForSeconds(9.0f);
        finaltext = _pinBody.GetComponent<PinBody>().TriggerText;
        _giftAmount = Convert.ToInt32(finaltext.Substring(4));
        _text.text = "Claim";
        _amount=1;
        ReturnAmount(_giftAmount);
    }
    void ReturnAmount(int i)
    {
        Debug.Log(Gifts[i-1].GetComponent<Gift>().type + " " + Gifts[i-1].GetComponent<Gift>()._amount);
    }

    IEnumerator coroutineB()
    {
        yield return new WaitForSeconds(2f);
        _wayLeft = true;
        Gifts[_giftAmount - 1].transform.localScale = Gifts[_giftAmount - 1].transform.localScale /2f;
    }
}
