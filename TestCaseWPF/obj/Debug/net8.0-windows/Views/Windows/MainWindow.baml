   M S B A M L   `   `   `  ����F  BTestCaseWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TestCaseWPFTestCaseWPF  5          TestCaseWPF.MainWindow   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:TestCaseWPF     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        $	��750��5	      $	��550��5
      $��14(�5      $��Normal&�5        � Title  5      �� *MainWindowTitle+.��5      � $	��720��5   	   $	��500��5   	   $��0,0,0,0q�5   	    � ColumnDefinitions 5   
   �� 5      $	��170��6   �� 5      $	��170��6   �� 5      $	��170��6   �� 5      $	��170��6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $	��250��6   �� 5      $
��auto �6   .+�5   
   �� $��2��5      $��
10,2,10,18q�5       $��Center=�5!      $��Center=�5"      $/��)Добавить изображение
��5$      ��5#      �� *OpenDialogCommand+5$      �� 5%   
   $��10,10,10,10q�5&      $��Center=�5'      $��Center=�5(       �� DisplayMemberPath$ Filter��5)      ��5*      ��  �� Path$ Filters� �� SelectedItem 5+      �� $ SelectedFilter��� 5,   
   $��2��5-      $��1��5.      $��
10,2,10,18q�5/      $��Center=�50      $��Center=�51      $.��(Соханить изображение
��53      ��52      �� *SaveDialogCommand+53      �� 54   
   $��1��55      $��10,10,10,10q�56      $��Center=�57      $��Center=�58      $&�� Применить фильтр
��5:      ��59      �� *ApplyFilter+5:      �� 5;   
   $��1��5<      $��4��5=      $��50,12,61,12q�5>      $��Uniform=�5@      ��5?      �� *DisplayedImage+5@      �� 5A   
   $��2��5B      $��10,10,10,10q�5C      $��Center=�5D      $��Center=�5E      $.��(Исходное изображение
��5G      ��5F      �� *RollBackImage+5G      �� 5H   
   $��3��5I      $��10,10,10,10q�5J      $��Center=�5K      $��Center=�5L      $.��(Показать гистограмму
��5N      ��5M      �� *InvokeHistogramWindow+5N      �� 5O   
   $��1��5P      $��28��5Q      $��50��5R      $��10,170,142,160q�5S      $��Center=�5T      $��Center=�5U      $��<��5W      ��5V      �� *PreviousImage+5W      �� 5X   
   $��1��5Y      $��3��5Z      $��28��5[      $��50��5\      $��138,170,6,160q�5]      $��Center=�5^      $��Center=�5_      $��>��5a      ��5`      �� *	NextImage+5a      5b      5c      