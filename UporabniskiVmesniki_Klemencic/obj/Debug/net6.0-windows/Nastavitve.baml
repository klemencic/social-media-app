   M S B A M L   `   `   `  ����X  TUporabniskiVmesniki_Klemencic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:UporabniskiVmesniki_KlemencicUporabniskiVmesniki_Klemencic  5   	   .    (UporabniskiVmesniki_Klemencic.Nastavitve   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:UporabniskiVmesniki_Klemencic     5   	     � Title$  
Nastavitve��5   	   $	��350��6   $	��300��6)    � 
ResizeMode$ NoResize=�5	      .��5      � .+�5   
   ��    
LabelStyle#��[  6   $��Avtomatsko shranjevanje��64   $	��150��6V   $��	10,35,0,0q�6b   $	��Top=�6u   $
��Left=�6�   6�   d� 5   
   -   5         d�Name$ toggleButton��5   B     ToggleButtonStyle#��[ 6   $	��Off��6s   $	��100��6�   $��19��6�   $	��Top=�6�   $
��Left=�6�   $��	10,63,0,0q�6�   �� 5   
   -   5         ��Name$ button��5   6     ButtonStyle#��[ 6   $��Shrani��6F   $	��Top=�6X   $
��Left=�6p   $��	10,87,0,0q�6�   6�   �� 5   
   -   5        $ button2��5   6   #��[ 6   $��Naloži��6G   $	��Top=�6Y   $
��Left=�6q   $��
10,119,0,0q�6�   6�   �� 5   
   #��[  6   $$��Interval shranjevanja (minute)��64   $	��175��6]   $��
12,154,0,0q�6i   $	��Top=�6}   $
��Left=�6�   $��17��6�   6�   �� 5   
   -   5         ��Name$ slider��5   6     SliderStyle#��[ 6   $	��100��6F   $��19��6R   $	��Top=�6^   $
��Left=�6v   $��
11,175,0,0q�6�    � Minimum$ 1Q�6�    � Maximum$ 5Q�6�    � Value$ 2Q�6�    �� TickFrequency$ 1Q�6�   	 �� IsSnapToTickEnabled$
	 True��6�   6�   �� 5   
   -   5        
 ��Name$
 	valueText��5      $	��Top=�6W   $
��Left=�6o   $��121,175,0,0q�6�   ��6'   ��  �� ElementName$ slider�� �� Path$ Value�6�   5      5       