using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestingScript : MonoBehaviour
{
    [SerializeField] TMP_Text PlayerBank;
    [SerializeField] InputField WithdrawAmount;
    [SerializeField] InputField DepositAmount;
    [SerializeField] TMP_Text WorldBank;
    public GameData gd;

    public void Start()
    {
        gd.intData["InputAmount"] = 0;
        WithdrawAmount.text = "$" + gd.intData["InputAmount"].ToString();
        DepositAmount.text = "$" + gd.intData["InputAmount"].ToString();
        WorldBank.text = "$" + gd.intData["WorldBank"].ToString();
        PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
    }
    public void Deposit()
    {
        if (gd.intData["InputAmount"] > gd.intData["PlayerBank"])
        {
            print("you stupid");
            return;
        }


        gd.intData["PlayerBank"] -= gd.intData["InputAmount"];
        gd.intData["WorldBank"] += gd.intData["InputAmount"];
        WorldBank.text = "$" + gd.intData["WorldBank"].ToString();
        PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
    }

    private void Awake()
    {
        WithdrawAmount.onValueChanged.AddListener(UpdateValue);
        //WithdrawAmount.SetTextWithoutNotify(gd.intData["InputAmount"].ToString());

        DepositAmount.onValueChanged.AddListener(UpdateValue);
        //DepositAmount.SetTextWithoutNotify(gd.intData["InputAmount"].ToString());
    }

    public void WithDraw()
    {
        if (gd.intData["InputAmount"] > gd.intData["WorldBank"])
        {
            print("you stupid");
            return;
        }

        gd.intData["PlayerBank"] += gd.intData["InputAmount"];
        gd.intData["WorldBank"] -= gd.intData["InputAmount"];
        WorldBank.text = "$" + gd.intData["WorldBank"].ToString();
        PlayerBank.text = "$" + gd.intData["PlayerBank"].ToString();
    }


    private void UpdateValue(string v)
    {
        gd.intData["InputAmount"] = int.Parse(v);
    }
}
