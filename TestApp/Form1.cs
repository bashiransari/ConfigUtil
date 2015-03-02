using BAS.ConfigUtil;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [ConfigProp("2.7,3.4")]
        public List<double> ListInt2 { get; set; }

          [ConfigProp("2,3")]
        public List<int> ListInt { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadConfiguration("Test");
        }
    }
}
