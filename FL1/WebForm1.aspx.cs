using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FL1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            //foreach(ListItem item in q1.Items)
            //{
            //    if (item.Selected)
            //        Response.Write(item.Value +" ");
            //}

            //Om man inte vet hur många frågor som finns
            foreach (Control c in form1.Controls)
            {
                if (c.GetType() == typeof(CheckBoxList))
                {
                    Response.Write(c.ID);
                    CheckBoxList cbl = new CheckBoxList();
                    cbl = (CheckBoxList)c;
               
                    foreach (ListItem chkitem in cbl.Items)
                    {
                        if (chkitem.Selected)
                            Response.Write(chkitem.Text +" "+ chkitem.Value + " ");
                    }
                }
            }
        }
    }
}