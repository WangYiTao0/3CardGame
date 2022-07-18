using System.Collections;
using System.Collections.Generic;
using Protocol.Code;
using Protocol.Dto;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : UIBase
{

    private void Awake()
    {
        Bind(UIEvent.START_PANEL_ACTIVE);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.START_PANEL_ACTIVE:
                SetPanelActive((bool)message);
                break;
            default:
                break;
        }
    }

    private Button _loginBtn;
    private Button _closeBtn;
    private TMP_InputField _accountInputField;
    private TMP_InputField _passwordInputField;

    // Use this for initialization
    void Start()
    {
        _loginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        _closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        _accountInputField = transform.Find("AccountInputField").GetComponent<TMP_InputField>();
        _passwordInputField = transform.Find("PasswordInputField").GetComponent<TMP_InputField>();

        _loginBtn.onClick.AddListener(OnLoginClick);
        _closeBtn.onClick.AddListener(OnCloseClick);

        //面板需要默认隐藏
        SetPanelActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        _loginBtn.onClick.RemoveListener(OnLoginClick);
        _closeBtn.onClick.RemoveListener(OnCloseClick);
    }

    // private AccountDto _dto = new AccountDto();
    // private SocketMsg _socketMsg = new SocketMsg();
    /// <summary>
    /// 登录按钮的点击事件处理
    /// </summary>
    private void OnLoginClick()
    {
        if (string.IsNullOrEmpty(_accountInputField.text))
            return;
        if (string.IsNullOrEmpty(_passwordInputField.text)
            || _passwordInputField.text.Length < 4
            || _passwordInputField.text.Length > 16)
            return;
        
        //需要和服务器交互了
        AccountDto dto = new AccountDto(_accountInputField.text, _passwordInputField.text);
        SocketMsg socketMsg = new SocketMsg(OpCode.ACCOUNT, AccountCode.LOGIN, dto);
        // _dto.Account = _accountInputField.text;
        // _dto.Password = _accountInputField.text;
        // _socketMsg.OpCode = OpCode.ACCOUNT;
        // _socketMsg.SubCode = AccountCode.REGISTER_CREQ;
        // _socketMsg.Value = _dto;
        Dispatch(AreaCode.NET,0,socketMsg);
    }

    private void OnCloseClick()
    {
        SetPanelActive(false);
    }

}
