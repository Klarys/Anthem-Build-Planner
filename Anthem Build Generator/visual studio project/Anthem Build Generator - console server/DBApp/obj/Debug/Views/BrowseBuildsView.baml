   M S B A M L   `   `   `  ����@  <DBApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DBApp.ViewsDBApp.Views  5      "    DBApp.Views.BrowseBuildsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:DBApp.Views     5      
�������$��2q�5   &   
���   �
���   �.��5
      �   � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ Builds��5      $��1��6#   $��1��63      
IsReadOnly .    AutoGenerateColumns .  $��Center=�6p   $	��200��6�   $	��200��6�   $	��600��6�      SelectionMode$ Single=�6�    �� SelectedItem 6�   �� *SelectedBuild+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Build's Name: ��6%   	   MinWidth$		 300Q�6[   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6=   ��  �� Path$
 Name�6[     5      $ Class: ��6%   $		 150Q�6U   
 66   �� $ Class�6U     5      $ Rating: ��6%   $		 150Q�6W   
 67   �� $ Rating�6W   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��1��6;   $��	0 20 0 20q�6K   
�������.+�5       �� $ Vertical=�6   $	��450��60   .+�5!      �� $��5 5 0 0q�6   $��20(�6-   Name:6B   �� 5"      -   5         ��Name$ SelectedBuildName��5"      $��5 3 0 0q�67   $��Center=�6H   6g   �� 5#      $��5 20 0 0q�6   $��20(�6.   Additional Notes:6O   �� 5$      -   5        $" SelectedBuildAdditionalNotes��5$      $��5 3 0 0q�6B   $��Center=�6S   6r   �� 5%      -   5         ��Name$ ShowBuildDetails��5%      $��0 20 0 0q�63   .��6E   More details...6V   5&      �� 5'      $��Center=�6   $	��150��61   $	��150��6=   ��6J   �� *SelectedBuildClassIcon+6v   5(      �� 5+   
   -   5        $ ReturnToMenu��5+      $��3��6(   $��1��65   $��30��6F   .��6U   Return to main menu6j   5,      5-      