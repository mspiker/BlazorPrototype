namespace BlazorWebAPIAuthentication.Model
{
    public class AddUserModel
    {
        public string UserEmail { get; set; } = string.Empty;
        public string[] Roles { get; set; } = new string[] { };
    }
}
