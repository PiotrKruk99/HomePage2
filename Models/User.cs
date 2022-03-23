namespace homePage2.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string authString {get;set;} = string.Empty;
    public DateTime expiryDate {get;set;} = DateTime.Now.AddHours(1);
}