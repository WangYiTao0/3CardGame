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
                SetPanelActive((bool)message);
                break;
            default:
                break;
        }
    }

    private Button _loginBtn;
    private Button _closeBtn;
    private InputField _accountInputField;
    private InputField _passwordInputField;

    // Use this for initialization
    void Start()
    {
        _loginBtn = transform.Find("LoginBtn").GetComponent<Button>();
        _closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        _accountInputField = transform.Find("AccountInput").GetComponent<InputField>();
        _passwordInputField = transform.Find("PassWardInputField").GetComponent<InputField>();

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
        //TODO
    }

    private void OnCloseClick()
    {
        SetPanelActive(false);
    }

}
