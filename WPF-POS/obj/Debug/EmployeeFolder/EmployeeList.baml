   M S B A M L   `   `   `  ����B  >WPF-POS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WPF_POS.EmployeeFolderWPF_POS.EmployeeFolder  5   	   )    #WPF_POS.EmployeeFolder.EmployeeList   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:WPF_POS.EmployeeFolder     5   	     � WindowState$  	Maximized=�5   	    � Title$ EmployeeList��5	   	   $	��450��6   $	��800��6+   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dgvEmployee��5      $��25(�5         AutoGenerateColumns .     CanUserAddRows .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.1* 61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *Id+  5      $
 Name��6%   $
 0.3* 63    6@   �� *Name+  5      $ Username��6%   $
 0.3* 67    6D   �� *
Username+  5      $ Password��6%   $
 0.3* 67    6D   �� *
Password+  5      $ Phone��6%   $
 0.3* 64    6A   �� *Phone+  5      $ Salary��6%   $
 0.3* 65    6B   �� *Salary+  5      $ Address��6%   $
 0.3* 66    6C   �� *	Address+5      5      5      