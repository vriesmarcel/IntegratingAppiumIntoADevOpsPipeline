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

namespace CarvedRock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FillListView();

        //    listView1.View = View.List;
            listView1.GridLines = true;
            listView1.View = View.Details;
        }

        private void FillListView()
        {
            MockDataStore store = new MockDataStore();
            var items = store.GetItemsAsync().Result;
            listView1.Items.Clear();
            foreach (var item in items)
            {
                var displayItems = new string[] { item.Text, item.Description };
                var lvItem = new ListViewItem(displayItems);
                listView1.Items.Add(lvItem);
            }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Details frmDetail = new Details();
            if (listView1.SelectedItems.Count > 0)
            {
                frmDetail.ItemText = listView1.SelectedItems[0].SubItems[0].Text;
                frmDetail.itemDetail = listView1.SelectedItems[0].SubItems[1].Text;
                frmDetail.ShowDialog(this);
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewItem();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddNewItem();
        }

        private void AddNewItem()
        {
            var frmAddNewItem = new NewItemForm();
            frmAddNewItem.ShowDialog(this);
           
            FillListView();
        }
    }
}
