using System.ComponentModel.DataAnnotations;

namespace lab3b_vd.DTO.Admin;

public class RegisterDto
{
    [Length(2, 20, ErrorMessage = "name should be more then 2 letters")]
    public string Username { get; set; }

    [Length(2, 20, ErrorMessage = "password should be from 2 to 20 elements")]
    public string Password { get; set; }
}
