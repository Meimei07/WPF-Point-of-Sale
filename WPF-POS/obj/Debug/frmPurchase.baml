   M S B A M L   `   `   `  ����B  >WPF-POS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WPF_POSWPF_POS  5   	       WPF_POS.frmPurchase   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WPF_POS     5   	     � WindowState$  	Maximized=�5   	    � Title$ frmPurchase��5	   	   $	��450��6   $	��800��6*   .��5      � $��
15,15,15,5q�6    � RowDefinitions 5   
   �� 5      $��0.15* �6   �� 5      �� 5      $��0.65* �6   �� 5      $
��0.2* �6   .+�5   
   � $��0��6   $��	30 0 50 0q�6    � ColumnDefinitions 5      �� 5      $
��0.1* �6#   �� 5      $
��0.1* �6#   �� 5      $
��0.2* �6#   �� 5      $
��0.2* �6#   �� 5      $��0.25* �6#   �� 5      $��0.25* �6#   .+�5      �� $��0��6   $��ID : ��5       $��18(�5!      $��Center=�5"      $��Center=�5#      �� 5%      -   5        $��txtInvoiceId��5&      $��1��5%       �� 
IsReadOnly . $��18(�5)      $��Center=�5*      �� 5.      $��2��6   $��History ID : ��5/      $��18(�50      $��Center=�51      $��Center=�52      �� 54      -   5        $��cmbHistoryId��55      $��3��54      $��18(�58      $��Center=�59      �� 5=      $��4��6   $��Employee Name : ��5>      $��18(�5?      $��Center=�5@      $��Center=�5A      �� 5C      -   5        $��cmbEmployeeName��5D      $��5��5C      $��18(�5E      $��Center=�5F      5H      � 5K   
   $��1��6    5L      �� 5M      �� 5N      $��0.25* �6    .+�5R      � $��0��6    5S      �� 5T      �� 5U      �� 5V      �� 5W       5Z      �� 5[      $
��0.5* �6'   �� 5\      $��15 �6'   �� 5]      $��0.25* �6'   �� 5^      $��0.35* �6'   .+�5b      �� $��0��6   $��0��6)   $��Product Name : ��5c      $��18(�5d      $��Center=�5e      $��Right=�5f      �� 5h      -   5        $��cmbProductName��5i      $��0��5h      $��2��6(   $��18(�5l      $��Center=�5m      �� 5q      $��1��6   $��0��6)   $��Quantity : ��5r      $��18(�5s      $��Center=�5t      $��Right=�5u      �� 5w      -   5        $��txtQuantity��5x      $��1��5w      $��2��6'   $��18(�5y      $��Center=�5z      �� 5|      -   5        $��tbUnit��5}      $��1��5|      $��3��6)   $��20(�5�      $��10,0,0,0q�5�      $��Center=�5�      $
��left=�5�      ��5      �� *Unit+5�      �� 5�      $��2��6   $��0��6)   $��Cost Price : ��5�      $��18(�5�      $��Center=�5�      $��Right=�5�      �� 5�      -   5        $��txtCostPrice��5�      $��2��5�      $��2��6'    . $��18(�5�      $��Center=�5�      �� 5�      $��3��6   $��0��6)   $��Selling Price : ��5�      $��18(�5�      $��Center=�5�      $��Right=�5�      �� 5�      -   5        $��txtSellingPrice��5�      $��3��5�      $��2��6'    . $��18(�5�      $��Center=�5�      5�      �� 5�      $��1��6    �� Orientation$ 
Horizontal=�5�      $��Center=�5�      $��Center=�5�      .+�5�      �� -	   5        $��btnNew��5�      $	��New��5�      $��20(�5�      $��90��5�      $��40��5�      $��	10,0,10,0q�5�      �� 5�      -
   5        $��btnAdd��5�      $	��Add��5�      $��20(�5�      $��90��5�      $��40��5�      $��	10,0,10,0q�5�      �� 5�      -   5        $��	btnRemove��5�      $��Remove��5�      $��20(�5�      $��90��5�      $��40��5�      $��	10,0,10,0q�5�      5�      5�      &   System.Windows.Controls.DataGrid  5�   
   -   5        $��dgvPurchase��5�      $��2��5�      $��18(�5�         CanUserAddRows .     AutoGenerateColumns .     Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Id��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.07* 61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *Id+  5�      $	 Product Name��6%   $
 0.35* 6;    6I   �� *ProductName+  5�      $	 Quantity��6%   $

 0.2* 67    6D   �� *
Quantity+  5�      $
	 Unit��6%   $

 0.2* 63    6@   �� *Unit+  5�      $	 
Cost Price��6%   $

 0.3* 69    6F   �� *	CostPrice+  5�      $	 Selling Price��6%   $

 0.3* 6<    6I   �� *SellingPrice+5�      �� 5�   
   $��3��6   $ 
Horizontal=�5�      $��Center=�5�      $��Center=�5�      .+�5�      �� -   5        $��btnNewPurchase��5�      $	��New��5�      $��20(�5�      $	��100��5�      $��50��5�      $��	20,0,20,0q�5�      �� 5�      -   5        $��btnSavePurchase��5�      $
��SAVE��5�      $��20(�5�      $	��100��5�      $��50��5�      $��	20,0,20,0q�5�      5�      5�      5�      