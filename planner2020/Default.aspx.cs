using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace planner2020
{
    public partial class Default : System.Web.UI.Page
    {
        static int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //GridView1.Visible = false;

            if (!IsPostBack)
            {
                Years();
                Months();
            }

        }

        private void Months()
        {
            DataSet dsMonths = new DataSet();
            dsMonths.ReadXml(Server.MapPath("~/Data/Months.xml"));

            DropDownList3.DataTextField = "Name";
            DropDownList3.DataValueField = "Number";

            DropDownList3.DataSource = dsMonths;
            DropDownList3.DataBind();
        }

        private void Years()
        {
            DataSet dsYears = new DataSet();
            dsYears.ReadXml(Server.MapPath("~/Data/Years.xml"));

            DropDownList2.DataTextField = "Number";
            DropDownList2.DataValueField = "Number";

            DropDownList2.DataSource = dsYears;
            DropDownList2.DataBind();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            excepLabel.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\source\repos\planner2020\planner2020\App_Data\ajanda.mdf;Integrated Security=True");
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO [Table] (Title,Description,StartDate,EndDate) VALUES( @Title,@Description,@StartDate,@EndDate)", conn);
                
                cmd.Parameters.AddWithValue("@Title", baslikTB.Text);
                cmd.Parameters.AddWithValue("@Description", aciklamaTB.Text);
                cmd.Parameters.AddWithValue("@StartDate", Calendar1.SelectedDate);
                DateTime d2 = DateTime.Parse(bitisTB.Text);
                cmd.Parameters.AddWithValue("@EndDate", d2);
               
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Default.aspx");
            }
            catch (System.Data.SqlTypes.SqlTypeException)
            {
                excepLabel.Text = "Lütfen önce tarihi seçin.";
            }
            catch(System.FormatException)
            {
                excepLabel.Text = "Geçerli bir bitiş tarihi ekleyin.";
            }


        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
           
            count++;
            if (count % 2 == 1)
            {
                etkinlikBtn.Text = "Etkinlikleri göster";
                GridView1.Visible = false;
            }
            else
            {
                etkinlikBtn.Text = "Etkinlikleri gizle";
                GridView1.Visible = true;
            }


        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt16(DropDownList2.SelectedValue);
            int month = Convert.ToInt16(DropDownList3.SelectedValue);

            Calendar1.VisibleDate = new DateTime(year, month, 1);
            Calendar1.SelectedDate = new DateTime(year, month, 1);
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int year = Convert.ToInt16(DropDownList2.SelectedValue);
            int month = Convert.ToInt16(DropDownList3.SelectedValue);
            Calendar1.VisibleDate = new DateTime(year, month, 1);
            Calendar1.SelectedDate = new DateTime(year, month, 1);
        }

    }
}