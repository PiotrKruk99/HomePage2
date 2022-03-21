namespace homePage2.Models;

public class ErrorMsg
{
    public enum ErrorType { success, info, warning, danger }
    private string _msgText;
    private ErrorType _msgType;
    public string MsgText { get { return _msgText; } set { _msgText = value; } }
    public ErrorType MsgType { get { return _msgType; } set { _msgType = value; } }
    public ErrorMsg(string msg, ErrorType type)
    {
        _msgText = msg;
        _msgType = type;
    }
}