using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public List<Item1> list1 = new List<Item1>();
        public static string folder_path = @"C:\SavedFiles";
        public static string file_extention = ".pdf";

        public Form1()
        {
            InitializeComponent();
            HandleItems h = new HandleItems(checkedListBox1);
            Directory.CreateDirectory(folder_path);
            var x = h.Deserialize(list1);
            if (x != null)
            {
                list1 = x;
            }
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HandleItems h = new HandleItems(checkedListBox1);
            Item1 item = new Item1(textBox1.Text, dateTimePicker1.Value.Date);
            if (textBox1.Text != "")
            {
                int x = 0;
                foreach (Item1 value in list1)
                    if (item.Name_ == value.Name_)
                    {
                        MessageBox.Show("There is already an item with the same name", "Error");
                        x = 1;
                    }
                if (x == 0)
                {

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        var fileName = string.Empty;
                        var savePath = string.Empty;
                        fileName = openFileDialog1.SafeFileName;
                        savePath = (folder_path + @"\" + textBox1.Text + ".pdf");
                        var file = openFileDialog1.FileName;
                        System.IO.File.Copy(file, savePath);
                        MessageBox.Show("File selected: " + fileName, "File has been added", MessageBoxButtons.OK);

                        h.AddItem(item, list1);
                        textBox1.Clear();

                    }
                    else
                    {
                        MessageBox.Show("No file selected!", "Warning!", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Name can't be empty", "Error");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            HandleItems h = new HandleItems(checkedListBox1);
            foreach (var item in checkedListBox1.CheckedItems.OfType<Item1>().ToList())
            {
                h.RemoveItem(item, list1);
                System.IO.File.Delete(folder_path + @"\" + item.Name_ + file_extention);
            }
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            HandleItems.Serialize(list1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start(folder_path);
        }
    }
}
