using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

using Microsoft.Cognitive.LUIS;

namespace LUISSampleForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string button1Text = button1.Text;

            try
            {
                button1.Text = "処理中・・・";
                button1.Enabled = false;


                string appId = textBox1.Text;
                string appKey = textBox2.Text;

                HttpClient client = new HttpClient();
                var query = HttpUtility.ParseQueryString(string.Empty);

                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", appKey);

                query["timezoneOffset"] = "0";
                query["verbose"] = "true";
                query["spellCheck"] = "false";
                query["staging"] = "false";

                byte[] b = Encoding.UTF8.GetBytes($"\"{textBox3.Text}\"");

                var uri = $"https://eastasia.api.cognitive.microsoft.com/luis/v2.0/apps/{appId}?{query}";

                using (ByteArrayContent content = new ByteArrayContent(b))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync(uri, content);

                    var strResponseContent = await response.Content.ReadAsStringAsync();

                    textBox4.Clear();
                    textBox4.Text = strResponseContent;
                }
            }
            finally
            {
                button1.Text = button1Text;
                button1.Enabled = true;
            }
        }
    }
}
