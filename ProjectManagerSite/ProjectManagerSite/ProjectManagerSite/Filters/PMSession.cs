using PMDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectManagerSite.Filters
{
    [Table]
    public class PMSession : Entity<PMSession>
    {
        [Column]
        public Guid User_Id { get; set; }

        [Column]
        public DateTime? FinishDate { get; set; }

        public PMSession()
        {
            User_Id = Guid.NewGuid();
        }

        public bool LoadCookies(HttpRequestBase request)
        {
            if (request.Cookies["SessionID"] == null)
                return false;
            var x = request.Cookies["SessionID"];
            Id = new Guid(request.Cookies["SessionID"].Value);
            PMSession.Update();
            PMSession session = PMSession.Items.FirstOrDefault(item => item.Id == Id);
            if (session == null)
                return false;

            this.Id = session.Id;
            this.FinishDate = session.FinishDate;
            this.User_Id = session.User_Id;
            return true;
        }

        public void SaveCookies(string email, bool rememberMe, HttpResponseBase response)
        {
            if (rememberMe)
                FinishDate = DateTime.Today.AddYears(1);
            User.Update();
            var user = User.Items.FirstOrDefault(item => item.Email == email);
            if (user == null)
                throw new Exception("Current user was not found");
            else User_Id = user.Id;
            PMSession.Update();

            response.Cookies["SessionID"].Value = Id.ToString();            
        }

        
    }
}
