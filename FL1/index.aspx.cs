using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Configuration;
using Npgsql;

namespace FL1
{
    public partial class index : System.Web.UI.Page
    {
        private const int AntalFilmerPerSida = 9;
        private const int AntalSidorPager = 10;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            divError.Visible = false;
            if (!IsPostBack)
            {
                Int64 AntalTräffar = LaddaFilmer(1,1);
                FyllSidor(1);
            }
            
           
        }

        protected void pager_Click(object sender, EventArgs e)
        {
           
            string ValdSida = (sender as LinkButton).CommandArgument;
            int start, AktuellSida = Math.Abs(int.Parse(ValdSida));
            
            if (ValdSida[0]=='+')
            {
                AktuellSida++;
                
            }
            else if (ValdSida[0] == '-')
            {
                AktuellSida -= AntalSidorPager-1;
               

                if (AktuellSida < 0)
                {
                    AktuellSida = 1;
                    
                }
            }
            else
            {
                AktuellSida = int.Parse(ValdSida);
            }
           

            start = (AktuellSida * AntalFilmerPerSida) - (AntalFilmerPerSida-1);

            Int64 AntalTräffar = LaddaFilmer(AktuellSida, start);
            FyllSidor(AktuellSida);
            

        }

        //private void FyllSidor3(int aktuellSida)
        //{
        //    int AntalPoster = 22;
        //    int AntalSidor = AntalPoster / AntalFilmerPerSida;
            
        //    List<ListItem> sidor = new List<ListItem>();
        //    for (int i = 1; i <= AntalSidor; i++)
        //    {
        //        ListItem item = new ListItem();
        //        item.Text = i.ToString();
        //        item.Value = i.ToString();
        //        item.Selected = i == aktuellSida;

        //        sidor.Add(item);
        //    }
           
        //    Repeater2.DataSource = sidor;
        //    Repeater2.DataBind();
        //}







        private  void FyllSidor(int aktuellSida)
        {
            Int64 AntalPoster = LaddaFilmer(1, 1);
            int start, MaxAntalSidor;
              

            int AntalSidor = (int)Math.Ceiling((decimal)AntalPoster / (decimal)AntalFilmerPerSida);
           
            List<ListItem> sidor = new List<ListItem>();

            /* Kontrollera sidspann
             * 
             * Hämtar antalet sidor som ska visas på den valda sidan.
             * Om AktuellSida = 2, kommer sidspannet att vara 11-20
             * Då sätts variabeln start till 11 och MaxAntalSidor till 20
             * 
             */
            int j=0;
            do
            {
                j++;
                MaxAntalSidor = j * AntalSidorPager;
                start = MaxAntalSidor - (AntalSidorPager - 1);
                
            }
            while (aktuellSida > MaxAntalSidor);




            //Om det är fler sidträffar än man vill visa i pagern
            if (AntalSidor > AntalSidorPager)
            {
                //Om man har bläddrat fram i pagern vill man även visa en tillbakapil
                if (start != 1)
                {
                    ListItem item = new ListItem();
                    item.Text = "&laquo;";
                    item.Value = "-" + (start - 1).ToString();
                    item.Selected = false;
                    sidor.Add(item);
                }
            }

            // Om antalet sidor inte fyller pagern måste sista sidan begränsas
            if (MaxAntalSidor>AntalSidor)
            {
                MaxAntalSidor = AntalSidor;

            }
          
               
               
           //Fyller pagern från start till slut
            for (int i = start; i <= MaxAntalSidor; i++)
            {
                ListItem item = new ListItem();
                item.Text = i.ToString();
                item.Value = i.ToString();
                item.Selected = i == aktuellSida;
               
                sidor.Add(item);
            }

            //Om det finns fler sidor än som visas ska en framåtpil laddas
            //Det betyder att sista sidan inte får en framåtpil
            if (MaxAntalSidor!=AntalSidor)
            {
               
                ListItem item = new ListItem();
                item.Text = "&raquo;";

                item.Value = "+" + MaxAntalSidor;
                item.Selected = false;
                sidor.Add(item);
            }

            Repeater2.DataSource = sidor;
            Repeater2.DataBind();
        }

        //private void LaddaFilmer(int aktuellsida)
        //{
        //    string connectionString = WebConfigurationManager.ConnectionStrings["pagila"].ConnectionString;
        //    NpgsqlConnection conn = new NpgsqlConnection(connectionString);
        //    try
        //    {
        //        string sql = "select title,description from film order by title offset :AktuellSida limit :AntalFilmer";
               
                   
        //        NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("AntalFilmer", AntalFilmerPerSida);
        //        cmd.Parameters.AddWithValue("AktuellSida", aktuellsida);
        //        conn.Open();
        //        NpgsqlDataReader reader = cmd.ExecuteReader();
        //        Repeater1.DataSource = reader;
        //        Repeater1.DataBind();

        //        FyllSidor(aktuellsida);
        //    }
        //    catch (Exception e)
        //    {
        //        Response.Write(e.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        private Int64 LaddaFilmer(int aktuellsida, int start)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["pagila"].ConnectionString;
            Int64 antal;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "select count(*) from film";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        object AntalPoster = cmd.ExecuteScalar();
                        antal = (Int64)(AntalPoster);

                    }

                    sql = "select title,description from film order by title offset :Start limit :AntalFilmer";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("AntalFilmer", AntalFilmerPerSida);
                        cmd.Parameters.AddWithValue("Start", start);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            Repeater1.DataSource = reader;
                            Repeater1.DataBind();
                            //FyllSidor(aktuellsida);
                        }
                    }

                }
                return antal;
            }
            catch (Exception e)
            {
                divError.Visible = true;
                lblError.Text= e.Message;
                return 0;
            }
            
            //NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            //try
            //{
            //    string sql = "select count(*) from film";
            //    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            //    conn.Open();
            //    object AntalPoster = cmd.ExecuteScalar();
            //    Int64 antal = (Int64)(AntalPoster);

               
            //    sql="select title,description from film order by title offset :Start limit :AntalFilmer";

            //    cmd = new NpgsqlCommand(sql, conn);
            //    cmd.Parameters.AddWithValue("AntalFilmer", AntalFilmerPerSida);
            //    cmd.Parameters.AddWithValue("Start", start);
            //    NpgsqlDataReader reader = cmd.ExecuteReader();
            //    Repeater1.DataSource = reader;
            //    Repeater1.DataBind();
                
            //    FyllSidor(aktuellsida);
            //    return antal;
            //}
            //catch (Exception e)
            //{
            //    Response.Write(e.Message);
            //    return 0;
            //}
            //finally
            //{
            //    conn.Close();
            //}
            
           
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

            mångadjur.Add(d);
            mångadjur.Add(d2);
            mångadjur.Add(d3);

            
            Repeater1.DataSource = mångadjur;
            Repeater1.DataBind();

           
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
            //XmlNodeList musikinstrument = doc.SelectNodes("/musikinstrument/instrument[namn='kontrabas']");

       

            XmlNodeList musikinstrument = doc.SelectNodes("/musikinstrument/instrument/namn[@nr='1']");
            //XmlAttribute nr = musikinstrument.Attributes["id"];

            foreach (XmlNode nod in musikinstrument)
            {
                //Label1.Text = nod.Attributes["nr"].Value;
                Label1.Text += nod.ParentNode["namn"].InnerText;
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