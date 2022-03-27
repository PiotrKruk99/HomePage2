using System.ComponentModel;

namespace homePage2.Models;

public class ResultMsg
{
    public enum ResultType { success, info, warning, danger }
    private bool _result;
    private string _msgText;
    private ResultType _msgType;
    private int _errCode;
    public bool Result { get { return _result; } set { _result = value; } }
    public string MsgText { get { return _msgText; } set { _msgText = value; } }
    public ResultType MsgType { get { return _msgType; } set { _msgType = value; } }
    public int ErrCode { get { return _errCode; } set { _errCode = value; } }
    public ResultMsg(bool result, int errCode = 0, string message = "", ResultType type = ResultType.info)
    {
        _result = result;
        _errCode = errCode;
        _msgText = message;
        _msgType = type;
    }
}