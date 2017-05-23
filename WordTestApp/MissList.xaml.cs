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

using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

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


        private void button_Click(object sender, RoutedEventArgs e)
        {
            Reload();
        }

        void Reload()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add();
            dt.Columns.Add();
            foreach(int n in mList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = wList[n][0];
                dr[1] = wList[n][1];
                dt.Rows.Add(dr);
            }
            this.dataGrid.ItemsSource = dt.DefaultView;
        }
    }
}
