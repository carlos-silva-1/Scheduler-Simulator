   M S B A M L   `   `   `  ����G  CSchedulerWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:SchedulerWPF.ViewsSchedulerWPF.Views  5   	   A%clr-namespace:SchedulerWPF.ViewModelsSchedulerWPF.ViewModels  5   	   "    SchedulerWPF.Views.InputView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:SchedulerWPF.Views     5   	   <
viewmodels%clr-namespace:SchedulerWPF.ViewModels     5   	     � WindowStyle$
  None=�5	   	   $	��500��5
   	   $	��800��6   ���#000c1f$��18(�5       � 	Resources 5         FocusVisual �� Property �� Value �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 RoundedButtonStyle �� 
TargetType �� 
TargetType	 ��Name
 �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 �     &	 �     &	 �     &	     &	 =    &	 ^    &	 �    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7  7  7 	 �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$   $	 1 2R�6/   #��� ��6E   $
 true��6�   $��1��6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ #008b8b��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ #000c0c��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6+   .�5       ��  �@�FocusVisualStyle8   61   �� 5!       � ��8  6+   �� 5"       � ��8  6,   �� 5#       � ��# � ��6+   �� 5$       � ��$ 1q�60   �� 5%       � ��$ Center=�6;   �� 5&       � ��$ Center=�69   �� 5'       � ��$ 1q�6(   �� 5(       � �� 5)      �� 5*      " ��6&   .R�5+      �� $	 border��6!   $
 10��61   #��z��6C   #��z��6m   #��z��6�   $
 true��6�   ._�5,      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  5-       5.      P� 5/       �@��IsDefaulted$
 true��6=   . �50   "   �� $ border��6@   # � ��6T    � ��51      P� 52       �@J�IsMouseOver$
 true��6=   . �53   "   �� $ border��6?   8   � ��6S   �� 54   "   $ border��6@   8   � ��6T   55      P� 56       � ��$
 true��6;   . �57   "   �� $ border��6?   8   � ��6S   �� 58   "   $ border��6@   8   � ��6T   59      P� 5:       � {�$ false��6;   . �5;   "   �� $ border��6?   8   � ��6S   �� 5<   "   $ border��6@   8   � ��6T   �� 5=   "   $ contentPresenter��6K   8 	  � ��6i   5>      5@      5B      5C      5D      .��5F      �  � RowDefinitions 5H   
   �� 5I      $	��150 �6   �� 5J      $��* �6    � ColumnDefinitions 5M   
   �� 5N      $	��250 �6   �� 5O      $��* �6   .+�5R   
   �� .+�5S      �� $��Scheduling Algorithm��6   $��	25 30 0 0q�64    �� Padding$ 15 0 0 0q�5T      
�������6+   �� 5U      $��30��6   $	��200��6#   $
��20 5q�6/   ��5V      �� *SchedulingAlgorithms+ �� SelectedItem 5W      �� *SelectedSchedulingAlgorithm+5X      �� 5Z   
   $��0��6   $��1��6"   $��20 0 0 0q�62   .+�5[      �� $
��Left=�67   $��20 0 0 0q�5\      
���������5[      �� *InputInfoText+5\   *   �� 5]      $	��300��6K   $��30��6W   $
��Left=�5^      ��5]      �� *NumberOfProcessesInput+ �� Mode$ TwoWay=�y�5^   1   �� *" NumberOfProcessesInputVisibility+�� 5_      -   5         ��Name$ 
StartInput��5_      $��Start Input��6)   $	��300��6?   $��30��5`      $
��Left=�6!   #��� 
 5a      ���#003f3f
�������$��0q�5b   (   $��0 5 0 0q�6<   y�5`   <   �� *StartInputBtnVisibility+5b   <   �� 5c      -   5        $ FinishInput��5c      $��Finish Input��6*   $	��300��6A   $��30��5d      $��0 5 0 0q�6!   $
��Left=�62   ���#003f3f
�������$��0q�5e   =   #��� 
 5f      5g      &   System.Windows.Controls.DataGrid  5i   
   -   5          Name$ InputDataGrid��5l      $��1��5i      $��0��6    $��2��60   $��0 0 0 0q�5j         CanUserAddRows$ False��5k         AutoGenerateColumns$ False��6*   
���������5j   $   �� *Memory+   Columns 5m      0  *System.Windows.Controls.DataGridTextColumn  5n      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   ��  �� Path$ ID�  5o      $ Arrival Time��6%    6;   �� $ ArrivalTime�  5p      $ Service Time��6%    6;   �� $ TotalServiceTime�  5q      $ IO Requests Types��6%    6@   �� *IORequestTypes+  5r      $ IO Requests Times��6%    6@   �� *IORequestTimes+5t      5v      5w      