using Lab7.Models;

namespace Lab7.Utils
{
    public static class Security
    {
        public static bool CheckIsAdmin(HttpContext context)
        {
            var check = context.Session.Get("isAdmin");
            if (check == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool CheckIsCommentUser(HttpContext context, Comments comment)
        {
            if (comment.SessionId != context.Session.Id || comment.Role == "admin")
            {
                return false;
            }
            else
            {
                    return true;
            }
        }
    }
}
