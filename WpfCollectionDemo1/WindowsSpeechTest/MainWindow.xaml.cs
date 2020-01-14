using Baidu.Aip.Speech;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Utility;

namespace WindowsSpeechTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }
        List<string> list;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] fg = { "东方", "西方", "南方", "北方", "你好", "张欧飞", "helloworld" };
                list = new List<string>();
                list.AddRange(fg);
                list.Add("天气怎么样");
                list.Add("你好吗");
                list.Add("打开音乐");
                list.Add("打开视频");

                sr = new SRecognition(list.ToArray());
                sr.ReginstEvent += Sr_ReginstEvent;
            }
            catch (Exception ee)
            {
                MessageBox.Show("MainWindow_Loaded:" + ee.StackTrace);
            }

        }

        private void Sr_ReginstEvent(string obj)
        {
            regist.Text = obj;
            //设置录音为静音
            if (list.Contains(obj))
            {
                SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.SpeakAsync(obj);
            }

            //设置录音为正常

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;
            switch (button.Content)
            {
                case "简单发音同步播放":
                    SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                    speechSynthesizer.Speak("Hello World!!!");
                    speechSynthesizer.Speak("你好 ，单词");
                    break;
                case "简单发音异步播放":
                    speechSynthesizer = new SpeechSynthesizer();
                    speechSynthesizer.SpeakAsync("简单发音异步播放Hello World!!!");
                    speechSynthesizer.SpeakAsync("简单发音异步播放你好 ，单词");
                    break;
                case "复杂发音":
                    speechSynthesizer = new SpeechSynthesizer();
                    speechSynthesizer.Volume = 100;
                    speechSynthesizer.Rate = 0;
                    //朗读语言:英语"en-US"，简体中文"zh-CN"，台湾中文"zh-TW"
                    string language = "zh-CN";
                    speechSynthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 0, new CultureInfo(language));
                    speechSynthesizer.SpeakAsync(button.Content.ToString() + "简单发音异步播放你好language ，单词");
                    break;

                default:
                    break;
            }

        }

        string path = "D:\\rec.wav";
        private SRecognition sr;
        private void UniformGrid_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;
            switch (button.Content)
            {
                case "微软语音识别开始":
                    sr.BeginRec(regist);
                    button.IsEnabled = false;
                    break;
                case "清空":
                    regist.Text = "";
                    break;

                case "百度开始录音":

                    //开始录音  15s后关闭录音，开始识别
                    startRecord = true;
                    record.StartRecord(path);
                    button.IsEnabled = false;

                    //15s录音 开始识别
                    DispatcherTimer dispatcherTimer = new DispatcherTimer();
                    dispatcherTimer.Interval = TimeSpan.FromSeconds(5);
                    dispatcherTimer.Tag = button;
                    dispatcherTimer.Tick += DispatcherTimer_Tick;
                    dispatcherTimer.Start();

                    break;
                case "百度语音识别开始":
                    record.StopRecord();
                    RegeistRecord();
                    break;
                case "百度清空":
                    break;
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer dispatcherTimer = sender as DispatcherTimer;
            (dispatcherTimer.Tag as Button).IsEnabled = true;
            dispatcherTimer.Stop();

            record.StopRecord();
            RegeistRecord();

        }

        RecordController record = new RecordController();
        bool startRecord = false;

        int num = 0;
        private void RegeistRecord()
        {

            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);
            fileStream.Close();
            //开始语音识别


            Dispatcher.Invoke(() => { baiduRegist.Text = "开始识别"; });

            //"err_msg": "param rate invalid.",
            //          "err_no": 3311,
            //"sn": "839609843531578964890"
            String APP_ID = "18266839";
            String API_KEY = "SguUG4TsGqYSCMNok4Uiz3PT";
            String SECRET_KEY = "Sf4wGgklCuQL60rNDKzMIOwQBdq6C3dE";
            var client = new Asr(APP_ID, API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间
            client.Timeout = 120000; // 若语音较长，建议设置更大的超时时间. ms
            var result = client.Recognize(buffer, "PCM", 16000);


            string temp = result.ToString().Replace("\r\n", "");

            DataBaiduResult dataBaiduResult = JsonHelper.JsonDeserialize<DataBaiduResult>(temp);
            //JToken resultStr = null;
            //result.TryGetValue("result", out resultStr);
            //Dispatcher.Invoke(() => { registGoogle.Text = "识别" + num++; });
            Dispatcher.Invoke(() => { registGoogle.Text = dataBaiduResult.result[0]; });

            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.SpeakAsync(dataBaiduResult.result[0]);
        }

    }

    /// <summary>
    /// 微软语音识别，简单的demo
    /// </summary>
    public class SRecognition
    {
        public SpeechRecognitionEngine recognizer = null;//语音识别引擎  
        public DictationGrammar dictationGrammar = null; //自然语法  
        public TextBlock cDisplay;

        public SRecognition(string[] fg) //创建关键词语列表  
        {
            CultureInfo myCIintl = new CultureInfo("zh-CN");
            foreach (RecognizerInfo config in SpeechRecognitionEngine.InstalledRecognizers())//获取所有语音引擎  
            {
                if (config.Culture.Equals(myCIintl) && config.Id == "MS-2052-80-DESK")
                {
                    recognizer = new SpeechRecognitionEngine(config);
                    break;
                }//选择识别引擎
            }
            if (recognizer != null)
            {
                InitializeSpeechRecognitionEngine(fg);//初始化语音识别引擎  
                dictationGrammar = new DictationGrammar();
            }
            else
            {
                MessageBox.Show("创建语音识别失败");
            }
        }
        private void InitializeSpeechRecognitionEngine(string[] fg)
        {
            recognizer.SetInputToDefaultAudioDevice();//选择默认的音频输入设备  
            Grammar customGrammar = CreateCustomGrammar(fg);
            //根据关键字数组建立语法  
            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammar(customGrammar);
            //加载语法  
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
            //recognizer.SpeechHypothesized += new EventHandler <SpeechHypothesizedEventArgs>(recognizer_SpeechHypothesized);  
        }

        public event Action<string> ReginstEvent;
        public void BeginRec(TextBlock tbResult)//关联窗口控件  
        {
            TurnSpeechRecognitionOn();
            TurnDictationOn();
            // cDisplay = tbResult;
        }
        public void over()//停止语音识别引擎  
        {
            TurnSpeechRecognitionOff();
        }
        public virtual Grammar CreateCustomGrammar(string[] fg) //创造自定义语法  
        {
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(new Choices(fg));
            return new Grammar(grammarBuilder);
        }
        private void TurnSpeechRecognitionOn()//启动语音识别函数  
        {
            if (recognizer != null)
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                //识别模式为连续识别  
            }
            else
            {
                MessageBox.Show("创建语音识别失败");
            }
        }
        private void TurnSpeechRecognitionOff()//关闭语音识别函数  
        {
            if (recognizer != null)
            {
                recognizer.RecognizeAsyncStop();
                TurnDictationOff();
            }
            else
            {
                MessageBox.Show("创建语音识别失败");
            }
        }
        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //识别出结果完成的动作，通常把识别结果传给某一个控件  
            string text = e.Result.Text;
            ReginstEvent(text);
            //cDisplay.Text += text;
        }
        private void TurnDictationOn()
        {
            if (recognizer != null)
            {
                recognizer.LoadGrammar(dictationGrammar);
                //加载自然语法  
            }
            else
            {
                MessageBox.Show("创建语音识别失败");
            }
        }
        private void TurnDictationOff()
        {
            if (dictationGrammar != null)
            {
                recognizer.UnloadGrammar(dictationGrammar);
                //卸载自然语法  
            }
            else
            {
                MessageBox.Show("创建语音识别失败");
            }
        }
    }



    /// <summary>
    /// 录音功能控制类
    /// </summary>
    public class RecordController
    {
        public WaveIn mWavIn;
        public WaveFileWriter mWavWriter;

        /// <summary>
        /// 开始录音
        /// </summary>
        /// <param name="filePath"></param>
        public void StartRecord(string filePath)
        {
            WaveFormat waveFormat = new WaveFormat(16000, 16, 1);
            mWavIn = new WaveIn();
            mWavIn.WaveFormat = waveFormat;
            mWavIn.DataAvailable += MWavIn_DataAvailable;
            mWavIn.RecordingStopped += MWavIn_RecordingStopped;
            mWavWriter = new WaveFileWriter(filePath, mWavIn.WaveFormat);
            mWavIn.StartRecording();
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void StopRecord()
        {
            mWavIn?.StopRecording();
            mWavIn?.Dispose();
            mWavIn = null;
            mWavWriter?.Close();
            mWavWriter = null;
        }

        private void MWavIn_RecordingStopped(object sender, StoppedEventArgs e)
        {
            mWavIn?.Dispose();
            mWavIn = null;
            mWavWriter?.Close();
            mWavWriter = null;
        }

        private void MWavIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            mWavWriter.Write(e.Buffer, 0, e.BytesRecorded);
            int secondsRecorded = (int)mWavWriter.Length / mWavWriter.WaveFormat.AverageBytesPerSecond;
        }
    }

}
