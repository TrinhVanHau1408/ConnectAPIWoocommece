   M S B A M L   `   `   `  ����>  :api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:apiapi  5   	       api.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:api     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ 	lvProduct��5      $��0��6&    �� View 5      �� 5      .P�5      �� $��ID��6%    �� DisplayMemberBinding 61   �� *id+�� 5      $
��Name��6%    �� Width$
 Auto��6Y    63   �� *name+6Y   �� 5      $��Quantity��6%   $
 Auto��6g    67   �� *stock_quantity+6g   �� 5      $��Stock status��6&   $
 Auto��6j    6<   �� *stock_status+6j   �� 5      $��Price��6%   $
 Auto��6c    64   �� *regular_price+6c   �� 5      $
��Type��6%   $
 Auto��6Y    63   �� *type+6Y   5      5      �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      �� -   5        $��Reload��5      $��5q�6@   �� 5      -   5        $	��Add��5      $��5q�6;   �� 5       -   5        $��Update��5       $��5q�6A   �� 5!      -   5        $��Delete��5!      $��5q�6B   5"      5#      5$      