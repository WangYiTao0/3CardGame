using System.Collections;
using System.Collections.Generic;
using Protocol.Code;
using Protocol.Dto;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : UIBase
{
    private void Awake()
    {
        Bind(UIEvent.REGIST_PANEL_ACTIVE);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REGIST_PANEL_ACTIVE:
                SetPanelActive((bool)message);
                break;
            default:
                break;
        }
    }

    private Button _registerBtn;
    private Button _closeBtn;
    private TMP_InputField _accountInputField;
    private TMP_InputField _passwordInputField;
    private TMP_InputField _repeatInputField;

    // Use this for initialization
    void Start()
    {
        _registerBtn = transform.Find("RegisterBtn").GetComponent<Button>();
        _closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        _accountInputField = transform.Find("AccountInputField").GetComponent<TMP_InputField>();
        _passwordInputField = transform.Find("PasswordInputField").GetComponent<TMP_InputField>();
        _repeatInputField = transform.Find("RepeatInputField").GetComponent<TMP_InputField>();

        _closeBtn.onClick.AddListener(OnCloseClick);
        _registerBtn.onClick.AddListener(OnRegisterClick);

        SetPanelActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        _closeBtn.onClick.RemoveListener(OnCloseClick);
        _registerBtn.onClick.RemoveListener(OnRegisterClick);
    }

    /// <summary>
    /// 注册按钮的点击事件处理
    /// </summary>
    private void OnRegisterClick()
    {
        if (string.IsNullOrEmpty(_accountInputField.text))
            return;
        if (string.IsNullOrEmpty(_passwordInputField.text)
            || _passwordInputField.text.Length < 4
            || _passwordInputField.text.Length > 16)
            return;
        if (string.IsNullOrEmpty(_repeatInputField.text)
            || _repeatInputField.text != _passwordInputField.text)
            return;

        //需要和服务器交互了
        //TODO
        AccountDto dto = new AccountDto(_accountInputField.text, _passwordInputField.text);
        SocketMsg socketMsg = new SocketMsg(OpCode.ACCOUNT, AccountCode.REGISTER_CLIENT_REQUEST, dto);
        Dispatch(AreaCode.NET,0,socketMsg);
    }

    private void OnCloseClick()
    {
        SetPanelActive(false);
    }
}
