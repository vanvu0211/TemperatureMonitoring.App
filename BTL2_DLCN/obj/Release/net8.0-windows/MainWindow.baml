   M S B A M L   `   `   `  ���� D  @BTL2_DLCN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:BTL2_DLCN	BTL2_DLCN  O,clr-namespace:BTL2_DLCN.Resources.ComponentsBTL2_DLCN.Resources.Components  g cLiveChartsCore.SkiaSharpView.WPF, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc92a3e13d40c573}Xclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF LiveChartsCore.SkiaSharpView.WPF     BTL2_DLCN.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:BTL2_DLCN     C
components,clr-namespace:BTL2_DLCN.Resources.Components     hlvcXclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  _	 [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 	   � WindowStartupLocation$  CenterScreen=� � Title$ 	BLT2_DLCN��$	��900��$
��1650��.���  � ColumnDefinitions �� $
��auto ��� $��* ��� $
��auto �.+�� $��0�� � RowDefinitions �� �� .+�# 	 MaterialDesignThemes.Wpf.Card  $��0��$��10,10,10,10q�$��10q�$��2��.���  �� $
��auto ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+���  �� Padding$ 10q�$��0��$��2��$��Center=�$��Center=�$��
Connecting��   	CardTitle#��[  �� $��0��$��1��$��Port:��  
FieldLabel#��[ $��10q�$��Center=��� $��1��$��1��) 	 #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Port��  ComboBox#��[ $��10q����� *	PortNames+ �� Text �� *
PortName+���� .R�$� �� $��0��$��2��$��	BaudRate:��#��[ $��10q�$��Center=��� $��1��$��2��$ BaudRate��#��[ $��10q����� *	BaudRates+ �� *
BaudRate+���� .R�$� �� $��0��$��3��$��Status:��#��[ $��10q�$��Center=��� $��1��$��3��$
��Bold"�#��[ $��10q�$��Center=����� *Status+���� *StatusColor+�� $��0��$��2��$��4��$��10q�$	��200��  ButtonPrimary#��[ + 	 %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��$��Connect��{��� *IsDisconnect+���� *ConnectCommand+�� $��0��$��2��$��5��$��10q�$	��200��#��[ $ 10��$��
Disconnect��{��� *	IsConnect+���� *DisconnectCommand+  $��1��$��10,10,10,10q�$��10q�$��2��.���  �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $ 10q�$��0��$��2��$��Center=�$��Center=�$��Report��#��[  � $��1��$��10q�$��Center=�$��1�� �� $
��auto ��� $
��auto ��� $��* � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+��� $��0��$��0��$��Sample Time:��#��[ $��Center=��� $��0��$��4��$��Custom Time:��#��[ $��Center=�	� $��0��$��1��$��10q� d� 	IsChecked �� *EnableTimer5s+	 �� Mode$	 TwoWay=�	� $��1��$��1��$��10q� �� *EnableTimer10s+$	 TwoWay=�	� $��2��$��1��$��10q� �� *EnableTimer30s+$	 TwoWay=�	� $��3��$��1��$��10q� �� *EnableTimer1m+$	 TwoWay=�	� $��44��$��1��$��10q� �� *EnableTimerCustom+$	 TwoWay=��� $��2��$��0��$��	5 Seconds��#��[ $��Center=����� *StausColor5sTimer+�� $��2��$��1��$��
10 Seconds��#��[ $��Center=����� *StausColor10sTimer+�� $��2��$��2��$��
30 Seconds��#��[ $��Center=����� *StausColor30sTimer+�� $��2��$��3��$��	1 Minutes��#��[ $��Center=����� *StausColor1mTimer+�� 
 �� Orientation$
 
Horizontal=�$��4��$��2��.+��� $��2��$��40��$��16(�$
��Bold"�$��Center=����� *StausColorCustomTimer+���� *TimerCustom+ �� UpdateSourceTrigger$ PropertyChanged=��� $��1��$��4��$�� Seconds��#��[ $��Center=����� *StausColorCustomTimer+�� $��0��$��2��$��4��$��10q�$	��200��#��[ $ 10��$��Log Data������ *LogDataCommand+{��� *	IsLogData+� $��1��$��2��$��	0,0,438,0q� �� $��* ��� $��* �.+�  $��0��$��10q�$��10q�.���  �� $
��auto ��� $��* �.+��� $��0��$��10q�
������� �� CornerRadius$ 10��._��� $ 10q�$��0��$��Sensor 1��#��[  � $��1�� �� $��* ��� $
��auto �.+���  �� Padding$ 10q�$��10q�$ 10��$��2��$��3q�
���   �._�� $��1�� �� $
��auto ��� $��* ��� $
��auto � �� $�� auto ��� $�� auto ��� $�� auto ��� $�� auto ��� $�� auto ��� $�� auto ��� $�� auto �.+��� $��0��$��0��$��Hight ThreshHold��#��[  $��18(�$��10q�$��Center=�.   (BTL2_DLCN.Resources.Components.Indicator  $��35��$��35��$��0��$��10q�$��1��   State �� *AlarmHightThreshHoldSensor1+   OnColor �� *" ColorAlarmHightThreshHoldSensor1+�� $��Center=�$
 
Horizontal=�$��2��$��1��.+��� $��0��$��1��$��Value:��#��[ $��10q�$��Center=��� $��1��$��40��$��16(�$	�� 10q�$��Center=����� *HightThreshHoldSensor1+$ PropertyChanged=�� $��2��$��3��.+��� $��5q��� $
��Left=�$��0��$��3��$��Low ThreshHold��#��[  $��18(�$��10q�$��Center=�  $��10q�$��35��$��35��$��3��$��1�� �� *AlarmLowThreshHoldSensor1+ �� * ColorAlarmLowThreshHoldSensor1+�� $��Center=�$
 
Horizontal=�$��2��$��4��.+��� $��0��$��1��$��Value:��#��[ $��10q�$��Center=��� $��1��$��40��$��16(�$	�� 10q�$��Center=����� *LowThreshHoldSensor1+$ PropertyChanged=�� $��5��$��3��.+��� $��5q��� $
 
Horizontal=�$��6��.+��� $
��Left=�$��0��$��5��$��Enable Alarm:��#��[  $��18(�$��
10 0 10 10q�$��Center=��� $
��0 10q� �� *EnableAlarm1+$	 TwoWay=��� 
���   �$��3q�$��10q�$ 10��$��0��._�5  /LiveChartsCore.SkiaSharpView.WPF.CartesianChart  $��1��,  &LiveChartsCore.SkiaSharpView.WPF.Chart   SyncContext �� *Sync+   Series �� *	Series3+   XAxes �� *XAxes+  $��1��$��10q�$��10q�.���  �� $
��auto ��� $��* �.+��� $��0��$��10q�
�������$ 10��._��� $ 10q�$��0��$��Sensor 2��#��[  � $��1�� �� $��* ��� $
��auto �.+��� $ 10q�$��10q�$ 10��$��2��$��3q�
���   �._�� $��1�� �� $
��auto ��� $��* ��� $
��auto � �� $�� auto ��� $�� auto ��� $�� auto ��� $�� auto ��� $�� auto ��� $�� auto ��� $�� auto �.+��� $��0��$��0��$��Hight ThreshHold��#��[  $��18(�$��10q�$��Center=�  $��35��$��35��$��0��$��10q�$��1�� �� *AlarmHightThreshHoldSensor2+ �� *" ColorAlarmHightThreshHoldSensor2+�� $��Center=�$
 
Horizontal=�$��2��$��1��.+��� $��0��$��1��$��Value:��#��[ $��10q�$��Center=��� $��1��$��40��$��16(�$	�� 10q�$��Center=����� *HightThreshHoldSensor2+$ PropertyChanged=�� $��2��$��3��.+��� $��5q��� $
��Left=�$��0��$��3��$��Low ThreshHold��#��[  $��18(�$��10q�$��Center=�  $��10q�$��35��$��35��$��3��$��1�� �� *AlarmLowThreshHoldSensor2+ �� * ColorAlarmLowThreshHoldSensor2+�� $��Center=�$
 
Horizontal=�$��2��$��4��.+��� $��0��$��1��$��Value:��#��[ $��10q�$��Center=��� $��1��$��40��$��16(�$	�� 10q�$��Center=����� *LowThreshHoldSensor2+$ PropertyChanged=�� $��5��$��3��.+��� $��5q��� $
 
Horizontal=�$��6��.+��� $
��Left=�$��0��$��5��$��Enable Alarm:��#��[  $��18(�$��
10 0 10 10q�$��Center=��� $
��0 10q� �� *EnableAlarm2+$	 TwoWay=��� 
���   �$��3q�$��10q�$ 10��$��0��._�  $��1�� �� *Sync2+ �� *	Series4+ �� *XAxes2+� $��2�� �� $��* ��� $��* �.+�  $��0��$��10q�$��10q�.��� $��1�� �� $��* ��� $
��auto �.+�/  )LiveChartsCore.SkiaSharpView.WPF.PieChart  $��1��$��0��   InitialRotation$
 -225Q�   MaxAngle$	 270Q�   MinValue$ 0Q�   MaxValue 200   Series �� *Series+   VisualElements �� *VisualElements+�� $��Center=�$
 
Horizontal=�$��2��.+��� $ 
10 10 0 10q�$��2��$��2��$��Center=�$��Center=�$��Temperature Sensor 1:��#��[  �� $��80��$ 10q�$��2��$��2��$��Center=�$��Center=�#��[  ���� *Value1+�� $ 
0 10 10 10q�$��2��$��2��$��Center=�$��Center=�$	��°C��#��[    $��1��$��10q�$��10q�.��� $��1�� �� $��* ��� $
��auto �.+�  $��1��$��0��$
 -225Q�$	 270Q�$ 0Q� 200 �� *	Series2+ �� *VisualElements+�� $��Center=�$
 
Horizontal=�$��2��.+��� $ 
10 10 0 10q�$��2��$��2��$��Center=�$��Center=�$��Temperature Sensor 2:��#��[  �� $��80��$ 10q�$��2��$��2��$��Center=�$��Center=�#��[  ���� *Value2+�� $ 
0 10 10 10q�$��2��$��2��$��Center=�$��Center=�$	��°C��#��[  