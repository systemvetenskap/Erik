using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FL1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LaddaDjur(); 
        }
        private void LaddaDjur()
        {
            djur d = new djur();
            d.Namn = "Får";
            d.Text = "Det här är ett ulligt djur";

            djur d2 = new djur();
            d2.Namn = "Hund";
            d2.Text = "hundar äter små barn";

            djur d3 = new djur();
            d3.Namn = "Fågel";
            d3.Text = "fåglar flyger";

            List<djur> mångadjur = new List<djur>();
            mångadjur.Add(d);
            mångadjur.Add(d2);
            mångadjur.Add(d3);

            GridView1.DataSource = mångadjur;
            GridView1.DataBind();
        }
    }
}