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
            //更新
            Reloading();
        }

        void Reloading()
        {
            List<MissWord> ml = new List<MissWord>();
            var mList2 = mList.OrderBy(n => n);
            int nowJudge = -1;
            foreach(int n in mList2)
            {
                if (nowJudge == n) ml[ml.Count - 1].WrongLevel++;
                else
                {
                    ml.Add(new MissWord(wList[n][0], wList[n][1]));
                    nowJudge = n;
                }
            }
            this.dataGrid.ItemsSource = ml;
        }
    }

    class MissWord
    {
        public string Question { get; }
        public string Answer { get; }
        public int WrongLevel { get; set; }
        public MissWord(string q,string a)
        {
            Question = q;
            Answer = a;
            WrongLevel = 1;
        }
    }
}
