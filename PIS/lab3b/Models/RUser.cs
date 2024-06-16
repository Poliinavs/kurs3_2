namespace lab3b_vd.Models;

public class RUser : User
{
    public IEnumerable<string> Roles { get; set; }

    public RUser() : base()
    {
        Roles = new List<string>();
    }

    public RUser(User user) : base(user)
    {
        Roles = new List<string>();
    }

    public RUser(User user, IEnumerable<string> roles) : base(user)
    {
        Roles = roles;
    }
}
