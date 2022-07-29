using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Stream stream = FileUpload1.FileContent;
        StringBuilder sb = new StringBuilder();
        using (var reader = new StreamReader(stream))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine($"{line}");
                sb.AppendLine(line);
            }
        }
        TextArea1.InnerText = sb.ToString();
        TextBox1.Text = sb.ToString();
    }
}