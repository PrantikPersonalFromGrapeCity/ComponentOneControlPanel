using GrapeCity.Tools.ComponentOneControlPanel.Controls;
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

namespace GrapeCity.Tools.ComponentOneControlPanel.UIContainers
{
    /// <summary>
    /// Interaction logic for EditionsContainer.xaml
    /// </summary>
    public partial class EditionsContainer : UserControl
    {
        public EditionsContainer()
        {
            InitializeComponent();
            var editions=GlobalVariables.ProductsJson.SelectTokens("Root[*].Products[*].Editions[*]")
                            ?.ToList();
            if(editions?.Count()>0)
            {
                foreach(var edition in editions)
                {
                    var edContrl = new EditionControl(edition as JObject);
                    edContrl.Width = 260;
                    edContrl.Height = 150;
                    edContrl.Margin = new Thickness(5,5,5,5);
                    editionPanel.Children.Add(edContrl);
                }
            }
        }
    }
}
