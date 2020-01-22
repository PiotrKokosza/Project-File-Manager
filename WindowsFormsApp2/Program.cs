using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    public class Item1
    {
        [JsonProperty(PropertyName = "Name")]
        private string name;
        [JsonProperty(PropertyName = "Date")]
        private DateTime date;
        public string Name_
        {
            get => name;
        }
        public DateTime Date_
        {
            get => date;
        }
        public Item1()
        {

        }
        public Item1(string Name, DateTime Date)
        {
            name = Name;
            date = Date;
        }
        public override string ToString()
        {
            string text = (name + " - " + date.ToString("dd/MM/yyyy"));
            return text.ToString();
        }
    }
    public class HandleItems
    {
        private CheckedListBox c;
        public HandleItems(CheckedListBox C)
        {
            c = C;
        }
        public void AddItem(Item1 item, List<Item1> list)
        {
            list.Add(item);
            c.Items.Add(item);
        }
        public void RemoveItem(Item1 item, List<Item1> list)
        {
            list.Remove(item);
            c.Items.Remove(item);
        }
        public static void Serialize(List<Item1> list)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(Form1.folder_path + @"\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, list);
            }
        }
        public List<Item1> Deserialize(List<Item1> list)
        {
            JsonSerializer serializer = new JsonSerializer();
            if (File.Exists(Form1.folder_path + @"\json.txt") == false)
            {
                using (FileStream sw = File.Create(Form1.folder_path + @"\json.txt"))
                {
                    sw.Close();
                }
            }
            using (StreamReader sw = new StreamReader(Form1.folder_path + @"\json.txt"))
            using (JsonReader reader = new JsonTextReader(sw))
            {
                list = serializer.Deserialize<List<Item1>>(reader);
            }
            if (list != null)
            {
                foreach (var item in list.OfType<Item1>())
                {
                    c.Items.Add(item);
                }
            }
            return list;
        }
    }
}
