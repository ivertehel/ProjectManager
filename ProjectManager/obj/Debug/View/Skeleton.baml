   M S B A M L   `   `   `  ����A  =PMView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PMViewPMView  5   	       PMView.Skeleton   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:PMView     5   	     � Title$  Project Manager��5   	   $	��650��5	   	   $	��850��6    � WindowStartupLocation$ CenterScreen=�6"    � 	Resources 5
         ProfileButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers	 P� Property
 P� Value��%&  &	        �� 5   
   " ��6+   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 ���� 6,   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5      #��z��5      ._�5      �� $ PART_Content��6/   #��z��5   +   #��z��5   +   #��z��5   +   6c   5      5      5       5      P� 5      	 �@J�IsMouseOver
 . . �5      ��  � ��
 ��·�63   �� 5       �@J�Opacity$	 0.8Q�60   �� 5!       � ��
 ��·�64   5"      5$      5%      .��5&      � $��Stretch=�6   ��5'   
   �� 5(       �� ImageSource$  /Assets/MainBackground.png��6   6D    � RowDefinitions 5*   
   �� 5+      $
��auto �6   �� 5,      $
��auto �6   �� 5-      $��* �6   .+�5/   
   � $��0��6   $	��Top=�6   $��0��64   $��15, 15,15,0q�6D   ��50      �� 51       �� Color$ White��6"    �� Opacity$	 0.6Q�60   6@   .+�53      �� $��Project Manager��6   $��30(�6.   $��Center=�6<   54      � 55   
   $��1��6   $	��Top=�6   $��0��64   $��
15, 0,15,0q�6D   .+�56      � -   5        $��MenuBarGrid��56      $��Stretch=�6&   $��Stretch=�6D   6b   57      � 58   
   -   5        $��ProjectsGrid��58      $��2��6#   $��Stretch=�60   $��Stretch=�6N   $��15, 0,15,15q�6j   ��59      �� 5:      $ White��6"   $	 0.6Q�60   .+�5<      � -   5        $��BodyGrid��5<      $��Stretch=�6#   $��Stretch=�6A   6_   5=      � 5>   
   -   5        $��ClientsGrid��5>      $��2��6"   $��Stretch=�6/   .+�5?      � -   5        $��
ClientGrid��5?      $y�Hidden=�6%   $��Stretch=�69   $��Stretch=�6W   6u   5@      5A      5B      