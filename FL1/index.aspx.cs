using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace FL1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //LaddaDjur(); 
            //visaXML();
            XML();
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

        private void XML()
        {
            string xmlfil = Server.MapPath("instrument.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlfil);

            XmlNode info = doc.SelectSingleNode("/musikinstrument/information");
            Label1.Text = info.InnerText;

            //XmlNodeList musikinstrument = doc.SelectNodes("/musikinstrument/instrument");

            //foreach(XmlNode nod in musikinstrument)
            //{
            //    Label1.Text += nod["namn"].InnerText +" ";
            //}

            //// Hämta noder utifrån attribut
            //XmlNodeList musikinstrument = doc.SelectNodes("/musikinstrument/instrument[@id='3']");

            //foreach (XmlNode nod in musikinstrument)
            //{
            //    Label1.Text += nod["namn"].InnerText + " ";
            //}

            //Hämta noder utifrån namn
            XmlNodeList musikinstrument = doc.SelectNodes("/musikinstrument/instrument[namn='kontrabas']");

            foreach (XmlNode nod in musikinstrument)
            {
                Label1.Text += nod["namn"].InnerText + " ";
            }

        }

        private void visaXML()
        {
            string xmlfil = Server.MapPath("instrument.xml");
            XmlTextReader reader = new XmlTextReader(xmlfil);
            StringBuilder str = new StringBuilder();

            reader.ReadStartElement("musikinstrument");

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        str.Append("Element: ");
                        str.Append(reader.Name);
                        str.Append("<br />");

                        if  (reader.AttributeCount >0)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                str.Append("Atrributnamn: ");
                                str.Append(reader.Name);
                                str.Append(": ");
                                str.Append(reader.Value);
                                str.Append("<br />");
                            }
                        }
                        break;

                    case XmlNodeType.Text:
                        str.Append("Texten: ");
                        str.Append(reader.Value);
                        str.Append("<br />");
                        break;
                }

            }
            Label1.Text = str.ToString();

        }
    }
}