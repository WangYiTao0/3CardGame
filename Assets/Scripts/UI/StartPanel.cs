using System.Collections;
using System.Collections.Generic;
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
                setPanelActive((bool)message);
                break;
            default:
                break;
        }
    }

    private Button _loginBtn;
    private Button _closeBtn;
    private InputField _accountInput;
    private InputField _passwordInput;

    // Use this for initialization
    void Start()
    {
        _loginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        _closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        _accountInput = transform.Find("AccountInput").GetComponent<InputField>();
        _passwordInput = transform.Find("PasswordInput").GetComponent<InputField>();

        _loginBtn.onClick.AddListener(loginClick);
        _closeBtn.onClick.AddListener(closeClick);

        //面板需要默认隐藏
        setPanelActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        _loginBtn.onClick.RemoveListener(loginClick);
        _closeBtn.onClick.RemoveListener(closeClick);
    }

    /// <summary>
    /// 登录按钮的点击事件处理
    /// </summary>
    private void loginClick()
    {
        if (string.IsNullOrEmpty(_accountInput.text))
            return;
        if (string.IsNullOrEmpty(_passwordInput.text)
            || _passwordInput.text.Length < 4
            || _passwordInput.text.Length > 16)
            return;
        
        //需要和服务器交互了
        //TODO
    }

    private void closeClick()
    {
        setPanelActive(false);
    }

}
