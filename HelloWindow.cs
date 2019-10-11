using System;
using System.Windows.Forms;

namespace MarkAnalyzerAutocadPlagin
{
    public partial class HelloWindow : Form
    {
        public HelloWindow()
        {
            InitializeComponent();
        }

        private void HelloBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
