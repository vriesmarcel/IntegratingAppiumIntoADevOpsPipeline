using CarvedRock.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarvedRock.Models;

namespace CarvedRock
{
    public partial class NewItemForm : Form
    {
        public NewItemForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dataStore = new MockDataStore();
            var newItem = new Item() { Text = txtItemText.Text, Description = txtItemDetail.Text };

            dataStore.AddItemAsync(newItem).Wait();
            Close();
        }
    }
}
