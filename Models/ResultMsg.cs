using System.ComponentModel;

namespace homePage2.Models;

public class ResultMsg
{
    public enum ResultType { success, info, warning, danger }
    private bool _result;
    private string _msgText;
    private ResultType _msgType;
    public bool Result { get { return _result; } set { _result = value; } }
    public string MsgText { get { return _msgText; } set { _msgText = value; } }
    public ResultType MsgType { get { return _msgType; } set { _msgType = value; } }
    public ResultMsg(bool result, string message, ResultType type)
    {
        _result = result;
        _msgText = message;
        _msgType = type;
    }
}