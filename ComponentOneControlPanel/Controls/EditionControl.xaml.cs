using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrapeCity.Tools.ComponentOneControlPanel.Controls
{
    /// <summary>
    /// Interaction logic for EditionControl.xaml
    /// </summary>
    public partial class EditionControl : UserControl
    {
        public EditionControl(JObject edition)
        {
            InitializeComponent();
            nmBlk.Text = edition["Name"].ToString().ToUpper();
            descText.Text= edition["Description"].ToString().ToUpper();
        }
    }
}
