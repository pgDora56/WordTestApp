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
using System.Windows.Shapes;

namespace WordTestApp
{
    /// <summary>
    /// MissList.xaml の相互作用ロジック
    /// </summary>
    public partial class MissList : Window
    {
        List<string[]> wList = new List<string[]>();
        List<int> mList = new List<int>();

        public MissList(List<string[]> wordsList, List<int> missList)
        {
            wList = wordsList;
            mList = missList;
            InitializeComponent();
        }
        
      /*  public List<string> Setting(string[][] w,List<int> mList)
        {

        }*/
        
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
