   M S B A M L   `   `   `  ����O  KFolderWatcher.PL.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:FolderWatcher.PL.WPF.ViewModelFolderWatcher.PL.WPF.ViewModel  5   	   G(clr-namespace:FolderWatcher.PL.WPF.ModelFolderWatcher.PL.WPF.Model  5   	   *    $FolderWatcher.PL.WPF.View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;vm,clr-namespace:FolderWatcher.PL.WPF.ViewModel     5   	   :model(clr-namespace:FolderWatcher.PL.WPF.Model     5   	   -   5        $
��Main��5   	     � Title$  Window - Watcher��5	   	    � WindowStartupLocation$ CenterScreen=�5
   	      GrayBlackGradientBrush#���   5   	   $	��900��5   	   $
��1000��5   	    � DataContext 5      8  2FolderWatcher.PL.WPF.ViewModel.MainWindowViewModel  5   
    � 	Resources 5       �� 
TargetType �� Property �� Value��%V  '��      'Q� �    �� 5   
   " ��6   �5      �� 5       � ��$ 5q�6+   �� 5       � ��$	 2.5q�6,   �� 5       � ��
 �   �60   �� 5       � ��$	 2.5q�64   �� 5       � ��
 � d �6/   5      �� 5   
   " Q�6   �5      �� 5        �@Q�
IsExpanded . 6/   5"      5#      .��5$      �  � ColumnDefinitions 5%   
   �� 5&      $��6* �6   �� 5'      $��6* �6   �� 5(       � RowDefinitions 5*   
   �� 5+      $
��auto �6   �� 5,      �� 5-      $
��auto �6   .+�5/   
   �� $��0��6   $��0��50      $��5q�51      $��Center=�52      $��Console��53        LightGreenGradientBrush#��[ 54      �� 55   
   $��1��6   $��0��56      $��5q�57        ControlDarkGradientBrush#��[ 59      ��58      �� *Logs+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=���5:      �� 5;      .R�5<      �  5=      �� 5>      �� 5?      .+�5A      �� $��0��6    
���""��$��Message��5C       �� 5D      $��1��6    
���2�2���5F       �� 5G      5H      5J      �� 5K   
   $��1��6   $��0��5L      $��5q�5M      {�.   �� TextWrapping$
 Wrap=�5O      ��5P      �� *CurrentPath+$	 TwoWay=�$
 PropertyChanged=�R� 5Q   
   -   5        $��DirectoriesTree��5S      $��1��5Q      $��1��5R      $��5q�5T       %� 
ScrollUnit$ Pixel=�5U      #��[ 5V      ��5W      �� *SelfDirectory+$	 TwoWay=�$
 PropertyChanged=���5X      �� 5Y       �� DataType/   )FolderWatcher.PL.WPF.Model.DirectoryModel"  6+    �� ItemsSource 5Z   +   �� *Directories+$	 TwoWay=�$
 PropertyChanged=�.R�5[      � ��6   ��  5\      �� 5]      �� 5^      �� 5_      �� 5`      �� 5a      .+�5c      �� $��0��6%    �� Orientation$ 
Horizontal=�5d   %   .+�5e      �� ��6$   �� *	ImagePath+$	 TwoWay=�$
 PropertyChanged=�� 5f      $��40��5g   *    � IsIndeterminate . y�5f   *   �� *
IsExpanded+  Vis_Converter �� 	Converter# [ 5h   *   �� 5i      $��	Next Node��6%   $��0q�5j   %   ��5k   %   ��  �� ElementName$
 Main�� �� Path$ DataContext.LungeCommand���5l   %   �� 5m      �� 5n      $��1��6%   $ 
Horizontal=�5o   %   .+�5p      �� 
��� d �$��Path:��5q   $   �� 5r      
���/�����5s   $   �� *
FullName+$	 TwoWay=�$
 PropertyChanged=�5t      �� 5u      $��2��6%   $ 
Horizontal=�5v   %   .+�5w      �� 
��� d �$��
Directory:��5x   $   �� 5y      
���/�����5z   $   �� *Name+$	 TwoWay=�$
 PropertyChanged=�5{      �� 5|      $��3��6%   $ 
Horizontal=�5}   %   .+�5~      �� 
��� d ���5   $   �� *	TotalSize+$	 TwoWay=�$
 PropertyChanged=��� 5�      
���/���$��bytes��5�   $   5�      R� 5�      $��4��6#   
������ ��5�   #   �� *Files+$	 TwoWay=�$
 PropertyChanged=���5�   #   �� ��5�      �� 5�   "   *   $FolderWatcher.PL.WPF.Model.FileModel"  6;   .R�5�   &   � #��[  6+    5�   *   �� 5�   .   �� 5�   .   .+�5�   *   �� $��0��60   $��Center=�5�   0   $��Center=�5�   0   ��5�   0   �� *	ImagePath+$	 TwoWay=�$
 PropertyChanged=�� 5�   *   $��1��6/    5�   .   �� 5�   2   �� 5�   2   �� 5�   2   �� 5�   2   �� 5�   2   .+�5�   .   �� $��0��69   $ 
Horizontal=�5�   9   .+�5�   2   �� 
��� d �$��Path:��5�   8   �� 5�   2   
���/�����5�   8   �� *
FullName+$	 TwoWay=�$
 PropertyChanged=�5�   /   �� 5�   .   $��1��69   $ 
Horizontal=�5�   9   .+�5�   2   �� 
��� d �$��File:��5�   8   �� 5�   2   
���/�����5�   8   �� *Name+$	 TwoWay=�$
 PropertyChanged=�5�   /   �� 5�   .   $��2��69   $ 
Horizontal=�5�   9   .+�5�   2   �� 
��� d �$��
Extension:��5�   8   �� 5�   2   
����  ���5�   8   �� *	Extension+$	 TwoWay=�$
 PropertyChanged=�5�   /   �� 5�   .   $��3��69   $ 
Horizontal=�5�   9   .+�5�   2   �� 
��� d ���5�   8   �� *Size+$	 TwoWay=�$
 PropertyChanged=��� 5�   2   
���/���$��bytes��5�   8   5�   /   5�   +   5�   '   5�   #   5�      5�      5�      5�      � 5�   
   $��2��6   $��0��5�      $��2��5�       5�      �� 5�      $
��auto �6    �� 5�      $
��auto �6    .+�5�      �� $��0��6   $
��Load��5�      {�5�      �� *
LoadFlag+$	 TwoWay=�$
 PropertyChanged=���5�      �� *LoadCommand+�� 5�      $��1��6   $��Watch��5�      {�5�      �� *WatcherFlag+$	 TwoWay=�$
 PropertyChanged=���5�      �� *StartWatchCommand+��5�      �� $ DirectoriesTree��$ SelectedItem�5�      �� 5�   
   $��2��6   $��3��5�        DarkGreenGradientBrush#��[ 5�      .@�5�      �� $��0��6   $��Clear Console��5�      ��5�      �� *ClearConsoleCommand+�� 5�      $��1��6   $��
Clear Tree��5�      ��5�      �� *ClearStopCommand+5�      5�      5�      