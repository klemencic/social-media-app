   M S B A M L   `   `   `  ����X  TUporabniskiVmesniki_Klemencic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:UporabniskiVmesniki_KlemencicUporabniskiVmesniki_Klemencic  5   P   M+clr-namespace:UporabniskiVmesniki_KlemencicUporabniskiVmesniki_Klemencic  5   	   .    (UporabniskiVmesniki_Klemencic.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   UUporabniskiVmesniki_Klemencic+clr-namespace:UporabniskiVmesniki_Klemencic     6P   =local+clr-namespace:UporabniskiVmesniki_Klemencic     5   	     � Title$  
MainWindow��5   	    � SizeToContent$ WidthAndHeight=�5	      .��5      � -   5         �Name$
 Main��5      $��Center=�6   $	��644��66   $	��Top=�6C   $
��1162��6[    J� Opacity$ 0.975Q�6h    � ColumnDefinitions 5   
   �� 5      $	��59* �6   �� 5      $��1103* �6   .+�5   
   ��    TabControlStyle#��[  6   $��2��6U   $��	0,-33,0,0q�6i   ��6>   Z� .@�5      �� $��10,142,-10,-142q�6     TabItemStyle#��[ 60   ��5       �� 5!       �� Orientation$ 
Horizontal=�6!   .+�5"      �� $��
 logo2.png��6    $��20��64   $��20��6?   �� 5#      $	��ZID��6$   $��Center=�6/   $��5,0,0,0q�6J   5$      .��5&      � ���	#FFE5E5E5$	��550��6.   $	��900��6;   .+�5(      �� -   5         ��Name$ welcomeText��5(       $��Welcome��65   $��24(�6D   $
��Bold"�6R   
���   �$
��Left=�6w   $	��Top=�6�   $��
-3,-34,0,0q�6�   $	��132��6�   z�5)      T� 5*      -   5        translateTransform��5*   1   5,      �� 5-      $	��100��6   $	��100��6)   $��24,89,906,423q�65   &   System.Windows.Controls.DataGrid  5/      -   5        $��postListView��5/   F     DataGridStlye#��[ 6      AutoGenerateColumns$ False��6Z   $��10,10,10,14q�6�      
IsReadOnly$
 True��6�   ��6v   �� *objave+	   Columns	 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn
   Header$
 	Uporabnik��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6D   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6N   �� *
DisplayIme+  53      $
 Vsebina��61   $ * 6B    6L   �� *	Vsebina+  54      $
 Datum��61   $ * 6@    6J   �� *Datum+  55      $

 Tema��61   $ * 6?    6I   �� *Tema+4  .System.Windows.Controls.DataGridTemplateColumn  56      $
 Slika��65   $ * 6D      CellTemplate 57   "   �� 58   &   .R�59   *   �� $	��100��6I   $	��100��6U   ��60   �� *Slika+6U   5:   '   5<        5=      $
 Oznaceni prijatelji��61   $	 120 6N    6Z   �� *TagedFriends+5?      5@      5A      �� 5B      #��[ 6   $��-90,176,90,-177q�6>   ��5C      �� 5D      $ 
Horizontal=�6!   .+�5E      �� $&�� pack://application:,,,/logo2.png��6    $��20��6J   $��20��6U   �� 5F      $��O MENI��6$   $��Center=�62   $��5,0,0,0q�6M   5G      .��5K      � ���	#FFE5E5E5$	��550��6.   $	��900��6;   .+�5M      1   +UporabniskiVmesniki_Klemencic.OsebniPodatki  -   5          Name$ 
normalview��5M   *   1   +UporabniskiVmesniki_Klemencic.OsebniPodatki   Loaded$ OsebniPodatki_Loaded_2��6k   $	��NaN��6>   $	��NaN��6K   $��
3,8,238,66q�6W   6�   2	   ,UporabniskiVmesniki_Klemencic.OsebniPodatki2	  5N      -   5         	 Name$ alternateview��5N   ?   2
   ,UporabniskiVmesniki_Klemencic.OsebniPodatki2 
  Loaded$ OsebniPodatki_Loaded_1��6�   $y�Hidden=�6+   $	��NaN��6V   $	��NaN��6c   $��
3,8,238,66q�6o   6�   5O      5U      �� 5V      #��[ 6   $��-190,209,190,-209q�6<   ��5W      �� 5X      $ 
Horizontal=�6!   .+�5Y      �� $&�� pack://application:,,,/logo2.png��6    $��20��6J   $��20��6U   �� 5Z      $��2q�6$   $��
PRIJATELJI��6/   5[      .��5]      � ���	#FFE5E5E5$	��550��6.   $	��900��6;   .+�5^        -   5        $��prijateljiList��5^   j   #��[ 6   $ False��6�   $
 True��6�   $��7,11,533,14q�6�   ��6F   �� *
prijatelji+	 5_        5`      $	
 Ime��61   $ * 6V    6>   �� *Ime+6V     5a      $
 Priimek��61   $ * 6^    6B   �� *	Priimek+6^     5b      $
 Status��61   $ * 6]    6A   �� *Status+6]   5d      �� 5f      -   5          ButtonStyle#��[ 5f      $��379,14,421,506q�6`   .��6x   Dodaj prijatelje6�   �� 5g      -	   5        #��[ 5g      $��381,84,419,436q�6Z   .��6r   Odstrani prijatelje6�   �� 5h      -
   5        #��[ 5h      $��380,49,420,471q�6W   .��6o   Uredi prijatelje6�   5i      5m      5q      �� 5s   
   �� 5w   
   $��13,11,1051,534q�6   $(��"pack://application:,,,/logotip.png��6(   $��2��6T   o� 5x   
     	MenuStyle#��[ 6   $��10,227,1054,343q�62   $��2��6K   .@�5z      m�   MenuItemStyle#��[ 6   $��24��6>   $��98��6J   $��Datoteka��6V   .@�5{      m� -   5        #��[ 5{      $��Izhod��6B   6a   m� 5|      -   5        #��[ 5|      $��Uvozi��6B   6Q   m� 5}      -   5        #��[ 5}      $��Izvozi��6B   6R   m� 5~      -   5        #��[ 5~      $��
Nastavitve��6B   6V   5�      m� 5�      $��24��6   $��98��6#   #��[ 6.   $��Objave��6U   .@�5�      m� -   5        #��[ 5�      $��Dodaj��6B   6q   m� 5�      -   5        #��[ 5�      $��Odstrani��6B   6q   m� 5�      -   5        #��[ 5�      $��Uredi��6B   6j   m� 5�      -   5        #��[ 5�      $��Osveži��6B   6l   m� 5�      -   5        #��[ 5�      $��Vsebina��6B   6r   5�      m� 5�      $��24��6   $��98��6#   #��[ 6/   $��
Postavitev��6V   .@�5�      m� -   5        #��[ 5�      $��
Originalna��6B   6t   m� 5�      -   5        #��[ 5�      $��Alternativna��6B   6z   5�      5�      5�      5�      