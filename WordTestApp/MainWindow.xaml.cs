using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Microsoft.Win32;

namespace WordTestApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AnswerMsg.Text = "";
        }

        string[][] wordsData = new string[100][];
        public int[] questionNumbers,missQNumbers;
        public int qNumber,correct,allQuestionCount;
        bool isNormalMode = true;
        public string fileUrl2 = "";

        List<int> missList = new List<int>();
        List<int> deleteList = new List<int>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fileUrl2 == "")
            {
                //ファイル読み込み
                string fileUrl = FileSelect();
                if (fileUrl != "")
                {
                    wordsData = ReadCsv(fileUrl);
                    if (wordsData != null)
                    {
                        this.fileUrl.Content = "準備完了：" + fileUrl;
                        fileUrl2 = fileUrl;
                        this.TopButton.Content = "復習開始";
                        this.TopButton.IsEnabled = false;
                        this.StartButton.IsEnabled = true;
                    }
                }
            }
            else
            {
                //復習モード
                if(missList.Count!=0)
                {
                    StartReStudyMode();
                }
            }
        }

        private void StartTest(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            qNumber = 0;
            correct = 0;
            //シャッフルする配列
            int[] ary = new int[100];
            for (int i = 0; i < 100; i++)
            {
                ary[i] = i;
            }

            //シャッフルする
            questionNumbers = ary.OrderBy(i => Guid.NewGuid()).ToArray();

            this.answerArea.Text = "";
            this.AnswerMsg.Text = "";
            this.nowStatus.Content = "1問目　現在0問中0問正解";
            this.questionArea.Content = wordsData[questionNumbers[0]][0];
            this.answerButton.IsEnabled = true;
            this.StartButton.IsEnabled = false;
            isNormalMode = true;
            LogWrite("#################### " + dt.ToString() + " " + fileUrl2 + " ####################");
        }

        void StartReStudyMode()
        {
            allQuestionCount = missList.Count;

            //シャッフルする配列
            int[] ary = new int[allQuestionCount];
            for (int i = 0; i < allQuestionCount; i++)
            {
                ary[i] = i;
            }
            //シャッフルする
            missQNumbers = ary.OrderBy(i => Guid.NewGuid()).ToArray();

            qNumber = 0;
            this.answerArea.Text = "";
            this.AnswerMsg.Text = "";
            this.nowStatus.Content = "復習モード 1問目/" + allQuestionCount +"問中";
            this.questionArea.Content = wordsData[missList[missQNumbers[0]]][0];
            this.answerButton.IsEnabled = true;
            this.StartButton.IsEnabled = false;
            isNormalMode = false;
        }

        private void AnswerCheck(object sender, RoutedEventArgs e)
        {
            if (isNormalMode)
            {
                //通常モード
                string msg;
                if (this.answerArea.Text == wordsData[questionNumbers[qNumber]][1])
                {
                    msg = "○ 正解。";
                    correct++;
                }
                else
                {
                    msg = "☓ 誤答。";
                    missList.Add(questionNumbers[qNumber]);
                }
                msg += "「" + wordsData[questionNumbers[qNumber]][0] + "」は" + wordsData[questionNumbers[qNumber]][1] + "(回答：" + this.answerArea.Text + ")";
                this.AnswerMsg.Text = msg;
                LogWrite(qNumber + "(" + questionNumbers[qNumber] + "):" + msg);
                if (qNumber < 99)
                {
                    qNumber++;
                    int q = qNumber + 1;
                    this.nowStatus.Content = q + "問目　現在" + qNumber + "問中" + correct + "問正解";
                    this.questionArea.Content = wordsData[questionNumbers[qNumber]][0];
                    this.answerArea.Text = "";
                }
                else
                {
                    this.questionArea.Content = "終了！";
                    this.nowStatus.Content = "最終結果：100問中" + correct + "問正解 / ミスゲージ：" + missList.Count;
                    LogWrite("最終結果：100問中" + correct + "問正解\n");
                    this.answerButton.IsEnabled = false;
                    this.StartButton.IsEnabled = true;
                    ReStudyModeCheck();
                }
            }
            else
            {
                //復習モード
                string msg;
                if (this.answerArea.Text == wordsData[missList[missQNumbers[qNumber]]][1])
                {
                    msg = "○ 正解。";
                    deleteList.Add(missList[missQNumbers[qNumber]]);
                }
                else
                {
                    msg = "☓ 誤答。";
                    missList.Add(missList[missQNumbers[qNumber]]);
                }
                msg += "「" + wordsData[missList[missQNumbers[qNumber]]][0] + "」は" + wordsData[missList[missQNumbers[qNumber]]][1] + "(回答：" + this.answerArea.Text + ")";
                this.AnswerMsg.Text = msg;
                //LogWrite(qNumber + "(" + missQNumbers[qNumber] + "):" + msg);
                if (qNumber < allQuestionCount - 1)
                {
                    qNumber++;
                    int q = qNumber + 1;
                    this.nowStatus.Content = "復習モード "+ q +"問目/" + allQuestionCount + "問中";
                    this.questionArea.Content = wordsData[missList[missQNumbers[qNumber]]][0];
                    this.answerArea.Text = "";
                    //this.answerArea.Text = wordsData[missList[missQNumbers[qNumber]]][1]; //デバッグ用
                }
                else
                {
                    this.questionArea.Content = "終了！" + deleteList.Count + "ミスの削除に成功！";
                    deleteList.Sort((a, b) => b - a);
                    foreach (int i in deleteList)
                    {
                        missList.Remove(i);
                    }
                    deleteList.Clear();
                    this.nowStatus.Content = "ミスゲージ：" + missList.Count;
                    this.answerButton.IsEnabled = false;
                    this.StartButton.IsEnabled = true;
                    ReStudyModeCheck();
                }
            }
        }

        static void LogWrite(string text)
        {
            Encoding utf8Enc = Encoding.GetEncoding("utf-8");
            StreamWriter writer = new StreamWriter(@"log.txt", true, utf8Enc);
            writer.WriteLine(text);
            writer.Close();
        }

        private void OpenMissList(object sender, RoutedEventArgs e)
        {
            if (missList.Count == 0)
            {
                MessageBox.Show("ミスリストはありません");
            }
            else
            {
                MissList ml = new MissList(wordsData,missList);
                ml.Show();
            }
        }

        static string FileSelect()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "単語データを読み込む";
            ofd.Filter = "CSVファイル（カンマ区切り）(*.csv)|*.csv";
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            else
            {
                return "";
            }
        }

        static string[][] ReadCsv(string fileUrl)
        {
            int count = 0;
            string[][] wordsData = new string[100][];
            for(int i = 0;i<100;i++)
            {
                wordsData[i] = new string[2];
            }

            try
            {
                // csvファイルを開く
                using (var sr = new System.IO.StreamReader(@fileUrl))
                {
                    // ストリームの末尾まで繰り返す
                    while (!sr.EndOfStream || count<100)
                    {
                        // ファイルから一行読み込む
                        var line = sr.ReadLine();
                        // 読み込んだ一行をカンマ毎に分けて配列に格納する
                        var values = line.Split(',');
                        wordsData[count] = values;
                        // 出力する
                        //foreach (var value in values)
                        //{
                        //    System.Console.Write("{0} ", value);
                        //}
                        //System.Console.WriteLine();
                        count++;
                    }
                }
                bool errorFlag = false;
                if (wordsData.Length != 100)
                {
                    errorFlag = true;
                }
                else
                {
                    for (int j = 0; j < 100; j++)
                    {
                        if(wordsData[j].Length != 2)
                        {
                            errorFlag = true;
                            break;
                        }
                    }
                }

                if (errorFlag)
                {
                    MessageBox.Show("CSVファイルが無効です。");
                    return null;
                }
                else
                {
                    return wordsData;
                }
            }
            catch (System.Exception e)
            {
                // ファイルを開くのに失敗したとき
                MessageBox.Show(e.Message);
                return null;
            }
        }

        void ReStudyModeCheck()
        {
            if(missList.Count==0)
            {
                this.TopButton.IsEnabled = false;
            }
            else
            {
                this.TopButton.IsEnabled = true;
            }
        }
/*
        private void AnswerAreaKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                AnswerCheck(this.answerButton);
            }
        }
        */
    }
}
