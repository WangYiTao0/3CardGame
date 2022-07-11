using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistPanel : UIBase
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
                setPanelActive((bool)message);
                break;
            default:
                break;
        }
    }

    private Button _registerBtn;
    private Button _closeBtn;
    private InputField _accountInput;
    private InputField _passwordInput;
    private InputField _repeatInput;

    // Use this for initialization
    void Start()
    {
        _registerBtn = transform.Find("RegisterBtn").GetComponent<Button>();
        _closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        _accountInput = transform.Find("AccountInput").GetComponent<InputField>();
        _passwordInput = transform.Find("PasswordInput").GetComponent<InputField>();
        _repeatInput = transform.Find("RepeatInput").GetComponent<InputField>();

        _closeBtn.onClick.AddListener(closeClick);
        _registerBtn.onClick.AddListener(registClick);

        setPanelActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        _closeBtn.onClick.RemoveListener(closeClick);
        _registerBtn.onClick.RemoveListener(registClick);
    }

    /// <summary>
    /// 注册按钮的点击事件处理
    /// </summary>
    private void registClick()
    {
        if (string.IsNullOrEmpty(_accountInput.text))
            return;
        if (string.IsNullOrEmpty(_passwordInput.text)
            || _passwordInput.text.Length < 4
            || _passwordInput.text.Length > 16)
            return;
        if (string.IsNullOrEmpty(_repeatInput.text)
            || _repeatInput.text != _passwordInput.text)
            return;

        //需要和服务器交互了
        //TODO
    }

    private void closeClick()
    {
        setPanelActive(false);
    }
}
