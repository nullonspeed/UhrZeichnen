using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
using System.Windows.Threading;

namespace Uhr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer SamplingTimer { get; set; }

        public double Radius { get; set; }

        public double HourDegree { get; set; }

        public double HourMinuteDegree { get; set; }

        public double SecondAndMinuteDegree { get; set; }

        public int HourLines { get; set; } = 12;

        public int MinuteLines { get; set; } = 60;

        public double MinInnerRadius { get; set; }

        public double HourInnerRadius { get; set; }

        public DateTime currentTime { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            SamplingTimer = new DispatcherTimer { IsEnabled = true, Interval = new TimeSpan(0, 0, 0, 0, 100) };
            SamplingTimer.Tick += SamplingTimer_Tick;
            HourDegree = 360 / 12;
            HourMinuteDegree = HourDegree / 60;
            SecondAndMinuteDegree = 360 / 60;
            currentTime = DateTime.Now;
        }

        private void SamplingTimer_Tick(object? sender, EventArgs e)
        {
            DrawWatch();
        }

        private void DrawWatch()
        {
            if(currentTime != DateTime.Now)
            {
                currentTime = DateTime.Now;
                Radius = can_Watch.ActualWidth > can_Watch.ActualHeight ? can_Watch.ActualHeight / 2 * 0.85 : can_Watch.ActualWidth / 2 * 0.85;
                MinInnerRadius = can_Watch.ActualWidth > can_Watch.ActualHeight ? can_Watch.ActualHeight / 2 * 0.82 : can_Watch.ActualWidth / 2 * 0.82;
                HourInnerRadius = can_Watch.ActualWidth > can_Watch.ActualHeight ? can_Watch.ActualHeight / 2 * 0.80 : can_Watch.ActualWidth / 2 * 0.80;
                can_Watch.Children.Clear();

                DrawScale();

                DrawPointer();
            }
           
           
        }

        private void DrawPointer()
        {
            Line HourPointer = new Line
            {
                StrokeThickness = 3,
                X1 = can_Watch.ActualWidth / 2,
                X2 = can_Watch.ActualWidth / 2,
                Y1 = (can_Watch.ActualHeight / 2 - HourInnerRadius * 0.6),
                Y2 = can_Watch.ActualHeight / 2,
                Stroke = new SolidColorBrush(Colors.White)


            };
            double angle = currentTime.Hour * HourDegree + currentTime.Minute * HourMinuteDegree;
            RotateTransform rotateScale = new RotateTransform(angle, can_Watch.ActualWidth / 2, can_Watch.ActualHeight / 2);
            HourPointer.RenderTransform = rotateScale;
            can_Watch.Children.Add(HourPointer);


            Line MinutePointer = new Line
            {
                StrokeThickness = 2,
                X1 = can_Watch.ActualWidth / 2,
                X2 = can_Watch.ActualWidth / 2,
                Y1 = (can_Watch.ActualHeight / 2 - HourInnerRadius * 0.8),
                Y2 = can_Watch.ActualHeight / 2,
                Stroke = new SolidColorBrush(Colors.Yellow)


            };
            angle = currentTime.Minute * SecondAndMinuteDegree;
           rotateScale = new RotateTransform(angle, can_Watch.ActualWidth / 2, can_Watch.ActualHeight / 2);
            MinutePointer.RenderTransform = rotateScale;
            can_Watch.Children.Add(MinutePointer);


            Line Second = new Line
                {
                    StrokeThickness = 1,
                    X1 = can_Watch.ActualWidth / 2 ,
                    X2 = can_Watch.ActualWidth / 2 ,
                    Y1 = (can_Watch.ActualHeight / 2 - HourInnerRadius*0.9),
                    Y2 = can_Watch.ActualHeight / 2 ,
                    Stroke = new SolidColorBrush(Colors.Red)


                };
                 angle = currentTime.Second * SecondAndMinuteDegree;
                rotateScale = new RotateTransform(angle, can_Watch.ActualWidth / 2, can_Watch.ActualHeight / 2);
                Second.RenderTransform = rotateScale;
                can_Watch.Children.Add(Second);



            
        }

        private void DrawScale()
        {

            for (int tempMinute = 0; tempMinute < MinuteLines; tempMinute++)
            {
                Line MinuteLine = new Line
                {
                    StrokeThickness = 1,
                    X1 = can_Watch.ActualWidth / 2 - Radius,
                    X2 = can_Watch.ActualWidth / 2 - MinInnerRadius,
                    Y1 = can_Watch.ActualHeight / 2,
                    Y2 = can_Watch.ActualHeight / 2,
                    Stroke = new SolidColorBrush(Colors.White)


                };
                double angle = tempMinute * SecondAndMinuteDegree;
                RotateTransform rotateScale = new RotateTransform(angle, can_Watch.ActualWidth / 2, can_Watch.ActualHeight / 2);
                MinuteLine.RenderTransform = rotateScale;
                can_Watch.Children.Add(MinuteLine);
            }

            for (int temphour = 0; temphour < HourLines; temphour++)
            {
                Line HourLine = new Line
                {
                    StrokeThickness = 2,
                    X1 = can_Watch.ActualWidth / 2 - Radius,
                    X2 = can_Watch.ActualWidth / 2 - HourInnerRadius,
                    Y1 = can_Watch.ActualHeight / 2,
                    Y2 = can_Watch.ActualHeight / 2,
                    Stroke = new SolidColorBrush(Colors.White)


                };
                double angle = temphour * HourDegree;
                RotateTransform rotateScale = new RotateTransform(angle, can_Watch.ActualWidth / 2, can_Watch.ActualHeight / 2);
                HourLine.RenderTransform = rotateScale;
                can_Watch.Children.Add(HourLine);
                TextBlock NumbersLabel;


                if (temphour==0)
                {
                    NumbersLabel = new TextBlock
                    {
                        Text = 12+"",
                        Foreground = new SolidColorBrush(Colors.White),
                        Height = 20,
                        Width = 30,
                        TextAlignment = TextAlignment.Center

                    };
                }
                else
                {
                    NumbersLabel = new TextBlock
                    {
                        Text = temphour+"",
                        Foreground = new SolidColorBrush(Colors.White),
                        Height = 20,
                        Width = 30,
                        TextAlignment = TextAlignment.Center

                    };
                }
               


                TransformGroup transformGroup = new TransformGroup();
                transformGroup.Children.Add(new TranslateTransform(-NumbersLabel.Width / 2, -NumbersLabel.Height / 2));
                transformGroup.Children.Add(new RotateTransform { Angle = -angle + 90 });
                transformGroup.Children.Add(new TranslateTransform { X = Radius*1.1, Y = 0 });
                transformGroup.Children.Add(new RotateTransform {Angle= angle -90 });
                transformGroup.Children.Add(new TranslateTransform {X = can_Watch.ActualWidth/2, Y= can_Watch.ActualHeight/2});


                NumbersLabel.RenderTransform = transformGroup;
                can_Watch.Children.Add(NumbersLabel);
            }

        }

        private void can_Watch_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Radius = can_Watch.ActualWidth > can_Watch.ActualHeight ? can_Watch.ActualHeight * 0.9 : can_Watch.ActualWidth * 0.9;
            DrawWatch();
        }

        
    }
}
