   M S B A M L   `   `   `  ����B  >WPF-POS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WPF_POSWPF_POS  5   	       WPF_POS.frmCustomer   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WPF_POS     5   	     � WindowState$  	Maximized=�5   	   
����ح� � Title$ frmCustomer��5
   	   $	��650��6   $	��800��6*   .��5      � .+�5   
   �� 
������� �� CornerRadius$ 25��5      $	��700��5      $	��550��5      $��Center=�5      $��Center=�5      ._�5      �  � RowDefinitions 5      �� 5      $��25 �6$   �� 5      $
��0.2* �6$   �� 5      .+�5      �� $��1��6   
�������6)   �� 5      $��1��6   $��Customer Entry��5      $��45(�5      $
��Bold"�5       $��Center=�5!      $��Center=�5"      � 5&      $��2��6    � ColumnDefinitions 5'      �� 5(      �� 5)      $
��0.4* �6+   .+�5-      � $��0��6   $��20q�6+    5.      �� 5/      �� 50      �� 51      �� 52      .+�55      �� $��0��6%   $��Center=�56   %   .+�57      �� $��Id��58   !   $��25(�59   !   $��0,0,0,6q�5:   !   �� 5<      -   5        $��txtId��5=   !    �� 
IsReadOnly . $	��300��5@   !   $��40��5A   !   
�������$��25(�5C   !   5E      �� 5G      $��1��6%   $��Center=�5H   %   .+�5I      �� $
��Name��5J   !   $��25(�5K   !   $��0,0,0,6q�5L   !   �� 5N      -   5        $��txtName��5O   !   $	��300��5Q   !   $��40��5R   !   
�������$��25(�5T   !   5V      �� 5X      $��2��6%   $��Center=�5Y   %   .+�5Z      �� $��Phone��5[   !   $��25(�5\   !   $��0,0,0,6q�5]   !   �� 5_      -   5        $��txtPhone��5`   !   $	��300��5b   !   $��40��5c   !   
�������$��25(�5e   !   5g      �� 5i      $��3��6%   $��Center=�5j   %   .+�5k      �� $��Address��5l   !   $��25(�5m   !   $��0,0,0,6q�5n   !   �� 5p      -   5        $��
txtAddress��5q   !   $	��300��5s   !   $��40��5t   !   
�������$��25(�5v   !   5x      5y      �� 5|      $��2��6!   $��Center=�5}   !   $��Center=�5~   !   .+�5      �� -   5        $��btnAdd��5�      $	��Add��5�      $	��130��5�      $��50��5�      $��23(�5�      $
��Bold"�5�      $��	0,10,0,10q�5�      
���2�2�5�      �� 5�      -   5        $��	btnUpdate��5�      $��Update��5�      $	��130��5�      $��50��5�      $��23(�5�      $
��Bold"�5�      $��	0,10,0,10q�5�      
���z���5�      �� 5�      -   5        $��	btnDelete��5�      $��Delete��5�      $	��130��5�      $��50��5�      $��23(�5�      $
��Bold"�5�      $��	0,10,0,10q�5�      
���z���5�      �� 5�      -   5        $��	btnBrowse��5�      $��Browse��5�      $	��130��5�      $��50��5�      $��23(�5�      $
��Bold"�5�      $��	0,10,0,10q�5�      
���z���5�      5�      5�      5�      5�      5�      5�      