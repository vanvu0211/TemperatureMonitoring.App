using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Windows;
using System.Net.NetworkInformation;
using Timer = System.Timers.Timer;
using System.Windows.Input;
using Microsoft.Win32;
using DocumentFormat.OpenXml.Spreadsheet;
using BTL2_DLCN.MQTT;using Newtonsoft.Json;

namespace BTL2_DLCN
{
    public class ViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Khoi tao
        SerialPort SerialPort;

        private readonly IExcelExporter _excelExporter;

        private readonly MqttClient _mqttClient;
        private MqttTag _mqttTagSensor1;
        private MqttTag _mqttTagSensor2;
        public string? ReportDataSensor1 { get; set; }
        public ObservableCollection<ReportData> reportDatas = new();
        public ObservableCollection<ReportData> reportData2s = new();
        public bool StartLogData { get; set; }  
        public bool IsLogData { get; set; }
        public int counter { get; set; }

        private readonly Timer Timer5s;

        
        public bool EnableTimer5s { get;set; }
        public bool EnableTimer10s { get;set; }
        public bool EnableTimer30s { get;set; }
        public bool EnableTimer1m { get;set; }
        public bool EnableTimerCustom { get;set; }
        public string? TimerCustom { get;set; }
        public int TimerCustom2Int { get;set; }

        #region realtimechart
        private readonly List<DateTimePoint> _values = new();
        private readonly DateTimeAxis _customAxis;
        public ObservableCollection<ISeries> Series3 { get; set; }
        public Axis[] XAxes { get; set; }
        public object Sync { get; } = new object();
        public bool IsReading { get; set; } = true;

        private readonly List<DateTimePoint> _values2 = new();
        private readonly DateTimeAxis _customAxis2;
        public ObservableCollection<ISeries> Series4 { get; set; }
        public Axis[] XAxes2 { get; set; }
        public object Sync2 { get; } = new object();
        public bool IsReading2 { get; set; } = true;
        #endregion realtimechart

        public List<string> PortNames { get; set; }
        public List<int> BaudRates { get; set; }
        public string? PortName { get; set; }
        public string? Status { get; set; }
        public string? StatusColor { get; set; }
        public string? BaudRate { get; set; }
        public bool IsConnect { get; set; }
        public bool IsDisconnect { get; set; }

        public string InputData = String.Empty;

        public double HightThreshHoldSensor1 { get; set; }
        public double HightThreshHoldSensor2 { get; set; }
        public double LowThreshHoldSensor1 { get; set; }
        public double LowThreshHoldSensor2 { get; set; }

        public bool AlarmHightThreshHoldSensor1 { get; set; }
        public string? ColorAlarmHightThreshHoldSensor1 { get; set; }
        public bool AlarmHightThreshHoldSensor2 { get; set; }
        public string? ColorAlarmHightThreshHoldSensor2 { get; set; }
        public bool AlarmLowThreshHoldSensor1 { get; set; }
        public string? ColorAlarmLowThreshHoldSensor1 { get; set; }
        public bool AlarmLowThreshHoldSensor2 { get; set; }
        public string? ColorAlarmLowThreshHoldSensor2 { get; set; }

        public bool EnableAlarm1 { get; set; }
        public bool EnableAlarm2 { get; set; }


        public string? StausColor5sTimer { get;set; }
        public string? StausColor10sTimer { get;set; }
        public string? StausColor30sTimer { get;set; }
        public string? StausColor1mTimer { get;set; }
        public string? StausColorCustomTimer { get;set; }

        private double data { get; set; }
        private double data2 { get; set; }
        private string? _value1;
        public string? Value1
        {
            get => _value1;
            set
            {
                _value1 = value;
                data = Convert.ToDouble(_value1);
                OnPropertyChanged(nameof(Value1));
            }
        }
        private string? _value2;
        public string? Value2
        {
            get => _value2;
            set
            {
                _value2 = value;
                data2 = Convert.ToDouble(_value2);
                OnPropertyChanged(nameof(Value2));
            }
        }

        public ICommand ConnectCommand { get; set; }
        public ICommand DisconnectCommand { get; set; }
        public ICommand LogDataCommand { get; set; }

        #endregion Khoi tao
        public ViewModel()
        {
            SerialPort = new SerialPort();
            _excelExporter = new ExcelExporter();
            _mqttClient = new MqttClient();
            SerialPort.DataReceived += SerialPort_DataReceived;
            PortNames = SerialPort.GetPortNames().ToList();
            BaudRates = new List<int> { 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 56000 };
            Status = "Diconnected!";
            StatusColor = "Red";
            BaudRate = "9600";
            IsConnect = false;
            IsDisconnect = true;
            ConnectCommand = new RelayCommand(Connect);
            DisconnectCommand = new RelayCommand(Disconnect);
            LogDataCommand = new RelayCommand(LogData);
            _mqttTagSensor1 = new MqttTag();
            _mqttTagSensor2 = new MqttTag();
            Timer5s = new Timer(1000);
            Timer5s.Elapsed += Timer5s_Elapsed;

            StausColor5sTimer = "Black";
            StausColor10sTimer = "Black";
            StausColor30sTimer = "Black";
            StausColor1mTimer = "Black";
            StausColorCustomTimer = "Black";

            StartLogData = false;
            IsLogData = false;
            //Chart
            var sectionsOuter = 130;
            var sectionsWidth = 20;

            Needle = new NeedleVisual();
            Needle2 = new NeedleVisual();

            Series = GaugeGenerator.BuildAngularGaugeSections(
                new GaugeItem(100, s => SetStyle(sectionsOuter, sectionsWidth, s)),

                new GaugeItem(60, s => SetStyle(sectionsOuter, sectionsWidth, s)),
                new GaugeItem(30, s => SetStyle(sectionsOuter, sectionsWidth, s)),
                new GaugeItem(10, s => SetStyle(sectionsOuter, sectionsWidth, s)));

            Series2 = GaugeGenerator.BuildAngularGaugeSections(
                new GaugeItem(100, s => SetStyle(sectionsOuter, sectionsWidth, s)),

                new GaugeItem(60, s => SetStyle(sectionsOuter, sectionsWidth, s)),
                new GaugeItem(30, s => SetStyle(sectionsOuter, sectionsWidth, s)),
                new GaugeItem(10, s => SetStyle(sectionsOuter, sectionsWidth, s)));

            VisualElements = new VisualElement<SkiaSharpDrawingContext>[]
            {
            new AngularTicksVisual
            {
                LabelsSize = 16,
                LabelsOuterOffset = 15,
                OuterOffset = 65,
                TicksLength = 20
            },
            Needle
            };

            VisualElements2 = new VisualElement<SkiaSharpDrawingContext>[]
           {
            new AngularTicksVisual
            {
                LabelsSize = 16,
                LabelsOuterOffset = 15,
                OuterOffset = 65,
                TicksLength = 20
            },
            Needle2
           };

            //Realtimechart
            Series3 = new ObservableCollection<ISeries>
        {
            new LineSeries<DateTimePoint>
            {
                Values = _values,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        };

            _customAxis = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };

            XAxes = new Axis[] { _customAxis };

            _ = ReadData();

            Series4 = new ObservableCollection<ISeries>
        {
            new LineSeries<DateTimePoint>
            {
                Values = _values2,
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        };

            _customAxis2 = new DateTimeAxis(TimeSpan.FromSeconds(1), Formatter)
            {
                CustomSeparators = GetSeparators(),
                AnimationsSpeed = TimeSpan.FromMilliseconds(0),
                SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
            };

            XAxes2 = new Axis[] { _customAxis2 };

            _ = ReadData2();
        }

        

        private void Timer5s_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {                 
            counter++;
            reportDatas.Add(new ReportData()
            {
                Name = "Sensor 1",
                Time = DateTime.Now,
                Value = Value1
            });
            reportData2s.Add(new ReportData()
            {
                Name = "Sensor 2",
                Time = DateTime.Now,
                Value = Value2
            });
        }

        public IEnumerable<ISeries> Series { get; set; }
        public IEnumerable<ISeries> Series2 { get; set; }

        public IEnumerable<VisualElement<SkiaSharpDrawingContext>> VisualElements { get; set; }
        public IEnumerable<VisualElement<SkiaSharpDrawingContext>> VisualElements2 { get; set; }

        public NeedleVisual Needle { get; set; }
        public NeedleVisual Needle2 { get; set; }


        private static void SetStyle(
        double sectionsOuter, double sectionsWidth, PieSeries<ObservableValue> series)
        {
            series.OuterRadiusOffset = sectionsOuter;
            series.MaxRadialColumnWidth = sectionsWidth;
        }

        private async Task ReadData()
        {
            // to keep this sample simple, we run the next infinite loop 
            // in a real application you should stop the loop/task when the view is disposed 

            while (IsReading)
            {
                await Task.Delay(100);

                // Because we are updating the chart from a different thread 
                // we need to use a lock to access the chart data. 
                // this is not necessary if your changes are made in the UI thread. 
                lock (Sync)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    _values.Add(new DateTimePoint(DateTime.Now, data));
#pragma warning restore CS8604 // Possible null reference argument.
                    if (_values.Count > 250) _values.RemoveAt(0);

                    // we need to update the separators every time we add a new point 
                    _customAxis.CustomSeparators = GetSeparators();
                }
            }
        }
        private async Task ReadData2()
        {
            // to keep this sample simple, we run the next infinite loop 
            // in a real application you should stop the loop/task when the view is disposed 

            while (IsReading2)
            {
                await Task.Delay(100);

                // Because we are updating the chart from a different thread 
                // we need to use a lock to access the chart data. 
                // this is not necessary if your changes are made in the UI thread. 
                lock (Sync2)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    _values2.Add(new DateTimePoint(DateTime.Now, data2));
#pragma warning restore CS8604 // Possible null reference argument.
                    if (_values2.Count > 250) _values2.RemoveAt(0);

                    // we need to update the separators every time we add a new point 
                    _customAxis2.CustomSeparators = GetSeparators();
                }
            }
        }

        private double[] GetSeparators()
        {
            var now = DateTime.Now;

            return new double[]
            {

            now.AddSeconds(-25).Ticks,
            now.AddSeconds(-20).Ticks,
            now.AddSeconds(-15).Ticks,
            now.AddSeconds(-10).Ticks,
            now.AddSeconds(-5).Ticks,
            now.Ticks
            };
        }



        private static string Formatter(DateTime date)
        {
            var secsAgo = (DateTime.Now - date).TotalSeconds;

            return secsAgo < 1 ? "now" : $"{secsAgo:N0}s ago";

        }
        private async void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                InputData = SerialPort.ReadLine();     // đọc lên dùng ReadLine để kiểm soát được string array sẽ tạo ở dưới

            }
            catch
            {
                return;
            }
            try
            {
                string[] InputArr = InputData.Split('|');       // tách khung dữ liệu gửi lên với dạng data0|data1|data2 lấy data 1,2
                Value1 = InputArr[0];
                Value2 = InputArr[1];

                _mqttTagSensor1.name = "Sensor1";
                _mqttTagSensor1.value = Value1;
                _mqttTagSensor1.timestamp = DateTime.Now;
;
                _mqttTagSensor2.name = "Sensor2";
                _mqttTagSensor2.value = Value2;
                _mqttTagSensor2.timestamp = DateTime.Now;

                if (_mqttClient.IsConnected)
                {
                    await _mqttClient.Publish("TEST2/Sensor1", JsonConvert.SerializeObject(_mqttTagSensor1), true);

                    await _mqttClient.Publish("TEST2/Sensor2", JsonConvert.SerializeObject(_mqttTagSensor2), true);

                }

                if (EnableAlarm1)
                {
                    AlarmHightThreshHoldSensor1 = true;
                    AlarmLowThreshHoldSensor1 = true;
                    if (double.Parse(Value1) > HightThreshHoldSensor1)
                    {
                        ColorAlarmHightThreshHoldSensor1 = "Red";
                    }
                    else
                if (double.Parse(Value1) < HightThreshHoldSensor1)
                    {
                        ColorAlarmHightThreshHoldSensor1 = "#00FF00";
                    }

                    if (double.Parse(Value1) > LowThreshHoldSensor1)
                    {
                        ColorAlarmLowThreshHoldSensor1 = "#00FF00";
                    }
                    else
                   if (double.Parse(Value1) < LowThreshHoldSensor1)
                    {
                        ColorAlarmLowThreshHoldSensor1 = "Red";
                    }
                }
                else
                {
                    AlarmHightThreshHoldSensor1 = false;
                    AlarmLowThreshHoldSensor1 = false;
                }

                if (EnableAlarm2)
                {
                    AlarmHightThreshHoldSensor2 = true;
                    AlarmLowThreshHoldSensor2 = true;

                    if (double.Parse(Value2) > HightThreshHoldSensor2)
                    {
                        ColorAlarmHightThreshHoldSensor2 = "Red";
                    }
                    else
                if (double.Parse(Value2) < HightThreshHoldSensor2)
                    {
                        ColorAlarmHightThreshHoldSensor2 = "#00FF00";
                    }

                    if (double.Parse(Value2) > LowThreshHoldSensor2)
                    {
                        ColorAlarmLowThreshHoldSensor2 = "#00FF00";
                    }
                    else
                   if (double.Parse(Value2) < LowThreshHoldSensor2)
                    {
                        ColorAlarmLowThreshHoldSensor2 = "Red";
                    }
                }
                else
                {
                    AlarmHightThreshHoldSensor2 = false;
                    AlarmLowThreshHoldSensor2 = false;
                }


                if (EnableTimer5s && StartLogData)
                {
                    IsLogData = false;
                    Timer5s.Enabled = true;                
                    StausColor5sTimer = "Green";
                        if (counter == 5)
                        {
                            EnableTimer5s = false;
                            Timer5s.Enabled = false;
                        StausColor5sTimer = "Black";
                        MessageBox.Show("Finished!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        await CreateFileExcel();
                        IsLogData = true;
                        counter = 0;
                        reportDatas = new();
                        reportData2s = new();
                        StartLogData = false;
                    }
                    
                }
                if (EnableTimer10s && StartLogData)
                {
                    Timer5s.Enabled = true;
                    StausColor10sTimer = "Green";
                    IsLogData = false;
                    if (counter == 10)
                    {
                        EnableTimer10s = false;
                        Timer5s.Enabled = false;
                        StausColor10sTimer = "Black";
                        MessageBox.Show("Finished!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        await CreateFileExcel();
                        reportDatas = new();
                        reportData2s = new();
                        StartLogData = false;
                        IsLogData = true;
                        counter = 0;
                    }
                }
                if (EnableTimer30s && StartLogData)
                {
                    Timer5s.Enabled = true;
                    IsLogData = false;
                    StausColor30sTimer = "Green";
                    if (counter == 30)
                    {
                        EnableTimer30s = false;
                        Timer5s.Enabled = false;
                        StausColor30sTimer = "Black";
                        MessageBox.Show("Finished!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        IsLogData = true;
                        await CreateFileExcel();
                        reportDatas = new();
                        reportData2s = new();
                        StartLogData = false;
                        counter = 0;
                    }
                }
                if (EnableTimer1m && StartLogData)
                {
                    Timer5s.Enabled = true;
                    StausColor1mTimer = "Green";
                    IsLogData = false;

                    if (counter == 60)
                    {
                        EnableTimer1m = false;
                        Timer5s.Enabled = false;
                        StausColor1mTimer = "Black";
                        MessageBox.Show("Finished!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        IsLogData = true;
                        await CreateFileExcel();
                        reportDatas = new();
                        reportData2s = new();
                        StartLogData = false;
                        counter = 0;
                    }
                }

                if (EnableTimerCustom && StartLogData)
                {
                    Timer5s.Enabled = true;
                    StausColorCustomTimer = "Green";
                    IsLogData = false;

                    try
                    {
                        TimerCustom2Int = Convert.ToInt32(TimerCustom);

                        if (counter == TimerCustom2Int)
                        {
                            EnableTimerCustom = false;
                            Timer5s.Enabled = false;
                            StausColorCustomTimer = "Black";
                            MessageBox.Show("Finished!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            IsLogData = true;
                            await CreateFileExcel();
                            reportDatas = new();
                            reportData2s = new();
                            StartLogData = false;
                            TimerCustom = "";
                            counter = 0;
                        }
                    }
                    catch ( Exception ex)
                    {
                        StausColorCustomTimer = "Black";
                        MessageBox.Show(ex.Message);
                        EnableTimerCustom = false;
                        Timer5s.Enabled = false;     
                        IsLogData = true;
                        TimerCustom = "";
                    }

                   
                }

                Needle.Value = double.Parse(Value1);
                Needle2.Value = double.Parse(Value2);


            }
            catch
            {
                return;
            }
        }

        private async void Connect()
        {
            if (!String.IsNullOrEmpty(PortName) && !String.IsNullOrEmpty(BaudRate))
            {
                try
                {
                    SerialPort.PortName = PortName;
#pragma warning disable CS8604 // Possible null reference argument.
                    SerialPort.BaudRate = int.Parse(BaudRate);
#pragma warning restore CS8604 // Possible null reference argument.
                    SerialPort.Open();
                    Status = "Connected!";
                    StatusColor = "Green";
                    IsConnect = true;
                    IsDisconnect = false;
                    IsLogData = true;
                    await _mqttClient.ConnectAsync();
                }
                catch (Exception ex)
                {
                    Status = "Diconnected!";
                    StatusColor = "Red";
                    IsConnect = false;
                    IsDisconnect = true;
                    IsLogData = false;
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else MessageBox.Show("Chưa chọn Port!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private async void Disconnect()
        {
            try
            {
                SerialPort.Close();
                Status = "Diconnected!";
                StatusColor = "Red";
                IsConnect = false;
                IsDisconnect = true;
                IsLogData = false;
                data = 0;
                data2 = 0;
                EnableAlarm1 = false;
                EnableAlarm1 = false;
                AlarmHightThreshHoldSensor1 = false;
                AlarmHightThreshHoldSensor2 = false;
                AlarmLowThreshHoldSensor1 = false;
                AlarmLowThreshHoldSensor2 = false;
                await _mqttClient.DisconnectAsync();
            }
            catch { }
        }

        private void ExportToExcel(string? filePath)
        {
            if (filePath is not null)
            {
                _excelExporter.ExportReport(filePath, reportDatas.Concat(reportData2s));
            }
        }

        private async Task CreateFileExcel()
        {
            Task CreateFileExcel = new (()=>
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.DefaultExt = "xlsx";
                if (saveFile.ShowDialog() == true)
                {
                    ExportToExcel(saveFile.FileName);
                }
            });
            CreateFileExcel.Start();
            await CreateFileExcel;
        }

        private void LogData()
        {
            StartLogData= true;
        }

    }
}
