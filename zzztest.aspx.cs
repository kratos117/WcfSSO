using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace WcfSSO
{
    public partial class zzztest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("begin<br/><br/>");

            _testPaginatorEmployee();
            _testGetGroups();            

            Response.Write("<br/><br/>done");
            return;
        }

        private void _testPaginatorEmployee() {
            SrvEmployeeInfo c = new SrvEmployeeInfo();
            TEmployeeInfo data = new TEmployeeInfo();
            c.QueryEmployees(data,"","",1,22);
        }

        private void _testGetGroups() {
            SrvEmployeeInfo c = new SrvEmployeeInfo();
            c.GetGroups();
        }
    }
}