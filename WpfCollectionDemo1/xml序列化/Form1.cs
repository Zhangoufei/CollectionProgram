using Com.Zhang.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace xml序列化
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string fileName = openFileDialog.FileName;

                ConfigeManagerMent configeManagerMent = CommonUntility.SerializeFileToObject<ConfigeManagerMent>(fileName);

                var order = from configeManager in configeManagerMent.configura  orderby configeManager.startUp.subpportted.name descending select configeManager;
                ConfigeManagerMent confi = new ConfigeManagerMent();
                confi.configura=new List<Configman>();
                confi.configura.AddRange(order);
                CommonUntility.SerializeObjectToFile<ConfigeManagerMent>(confi, "D://TIYE/xml.xml");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConfigeManagerMent configeManagerMent = new ConfigeManagerMent();

            Configman configman = new Configman();

            startUp startUp = new startUp();

            subpportted subpportted = new subpportted();
            subpportted.name = "1";

            startUp.subpportted = subpportted;

            configman.startUp = startUp;
            configeManagerMent.configura = new List<Configman>();
            configeManagerMent.configura.Add(configman);
            configeManagerMent.configura.Add(configman);
            configeManagerMent.configura.Add(configman);
            configeManagerMent.configura.Add(configman);

            CommonUntility.SerializeObjectToFile<ConfigeManagerMent>(configeManagerMent, AppDomain.CurrentDomain.BaseDirectory+"//"+"ddd.xml");
        }
    }


    [Serializable]
    public class ConfigeManagerMent
    {
        public List<Configman> configura { set; get; }
    }
    [Serializable]
    public class Configman
    {
        public startUp startUp { set; get; }
    }


    [Serializable]
    public class startUp
    {
        public subpportted subpportted;
    }
    [Serializable]
    public class subpportted
    {
        [XmlAttribute]
        public string version { set; get; }
        [XmlAttribute]
        public string sku { set; get; }
        [XmlAttribute]
        public string name { set; get; }

    }

}
