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
using System.Data;

namespace FL1
{
    public partial class index : System.Web.UI.Page
    {
        private const int AntalFilmerPerSida = 9;
        private const int AntalSidorPager = 10;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            divError.Visible = false;
            divInfo.Visible = false;
            if (!IsPostBack)
            {
               
                FyllSidor(1);
            }
            
           
        }

        protected void pager_Click(object sender, EventArgs e)
        {
           
            string ValdSida = (sender as LinkButton).CommandArgument;
            int AktuellSida = Math.Abs(int.Parse(ValdSida));
            
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
           
            
                       
            FyllSidor(AktuellSida);
            

        }

       
        private  void FyllSidor(int aktuellSida)
        {
            Int64 AntalPoster = RäknaAntalFilmer();
            if (AntalPoster > 0)
            {
                
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
                int j = 0;
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
                if (MaxAntalSidor > AntalSidor)
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
                if (MaxAntalSidor != AntalSidor)
                {

                    ListItem item = new ListItem();
                    item.Text = "&raquo;";

                    item.Value = "+" + MaxAntalSidor;
                    item.Selected = false;
                    sidor.Add(item);
                }

               LaddaFilmer(aktuellSida, start);
               

                Repeater2.DataSource = sidor;
                Repeater2.DataBind();
            }
            else
            {
                divInfo.Visible = true;
                lblInfo.Text = "Hittade inga filmer";
            }
        }

        
        private Int64 RäknaAntalFilmer()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["pagila"].ConnectionString;
            Int64 antal;
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "select count(*) from film";// where film_id=1234";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        object AntalPoster = cmd.ExecuteScalar();
                        antal = (Int64)(AntalPoster);

                    }
                                       
                }
                return antal;
            }
            catch (Exception e)
            {
                divError.Visible = true;
                lblError.Text = e.Message;
                return 0;
            }

        }
        private void LaddaFilmer(int aktuellsida, int start)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["pagila"].ConnectionString;
            
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "select title,description from film order by title offset :Start limit :AntalFilmer";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("AntalFilmer", AntalFilmerPerSida);
                        cmd.Parameters.AddWithValue("Start", start);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            Repeater1.DataSource = reader;
                            Repeater1.DataBind();
                            
                        }
                    }

                }
               
            }
            catch (Exception e)
            {
                divError.Visible = true;
                lblError.Text = e.Message;
               
                
            }

            
        }
       
    }
}