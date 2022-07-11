using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : UIBase
{
    private Button _startBtn;
    private Button _registerBtn;

    // Use this for initialization
    void Start()
    {
        _startBtn = transform.Find("StartBtn").GetComponent<Button>();
        _registerBtn = transform.Find("RegisterBtn").GetComponent<Button>();

        _startBtn.onClick.AddListener(OnStartBtnClick);
        _registerBtn.onClick.AddListener(OnRegistBtnClick);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        _startBtn.onClick.RemoveAllListeners();
        _registerBtn.onClick.RemoveAllListeners();
    }

    private void OnStartBtnClick()
    {
        Dispatch(AreaCode.UI, UIEvent.START_PANEL_ACTIVE, true);
        //GameObject.Find("").gameObject.SetActive(true);
    }


    private void OnRegistBtnClick()
    {
        Dispatch(AreaCode.UI, UIEvent.REGIST_PANEL_ACTIVE, true);
    }

}
