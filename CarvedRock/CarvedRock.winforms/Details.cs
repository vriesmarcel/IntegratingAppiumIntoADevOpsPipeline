using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarvedRock
{
    public partial class Details : Form
    {
        public Details()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            lblItemText.Text = ItemText;
            lblItemDetail.Text = itemDetail;
        }

        public string ItemText { get; set; }
        public string itemDetail { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
