   M S B A M L   `   `   `  ����G  CCardtekProje, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CardtekProjeCardtekProje  5   	   | xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting 5   	       CardtekProje.Window5   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:CardtekProje     5   	   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	     � Title$  Istatistikler��5	   
   $��645.85��6    $	��854��60    � 
ResizeMode$ 	CanResize=�6<    � Icon$ 	icon2.ico��6T   .��5
      � 
����h{�.+�5   
   o� 
����İ�$	��Top=�6+   $��28��6@   $	��Top=�6L   $��0,0,-2,0q�6d   .@�5      m� -   5        $��_Musteri Tablo��5      $��14(�6/   $��Black"�6=   $��Comic Sans MS)�6P   $��Italic$�6k   6~   m� 5      -   5        $��_Kampanya Tablo��5      $��14(�60   $��Comic Sans MS)�6>   $��Italic$�6Y   $
��Bold"�6l   6~   m� 5      $`��Z                                                                                          ��6   $y�Hidden=�6{   m� 5      -   5        $��
_SMS Tablo��5      $��14(�6,   $
��Bold"�6:   $��Comic Sans MS)�6L   $��Italic$�6g   6z   m� 5      -   5        $��_İslem Tablo��5      $��14(�6-   $��Right=�6;   $��RightToLeft=�6W   $
��Bold"�6t   $��Comic Sans MS)�6�   $��Italic$�6�   6�   5      >  8System.Windows.Controls.DataVisualization.Charting.Chart  5   
   -   5        $��ColumnChart1��5   "   $	��516��5       $��243,0,10,42q�5         Title$ Grafik��66   $��Bottom=�5         Series. 5      E  ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  H  BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   DependentValuePath$ Value��6+      IndependentValuePath$	 Key��6G      IsSelectionEnabled .     ItemsSource 6b   �� 5   *   5      �� 5   
   -   5        	 ��Name$	 olcut_combobox��5      $
��Left=�6L   $��
10,112,0,0q�6g   $	��Top=�6{   $	��159��6�   $��30��6�   �� 5   
   -   5        
 ��Name$
 	textBlock��5      $
��Left=�6'   $��	10,57,0,0q�6B   $
��Wrap=�6U   $,��&Karşılaştırma Ölüçütü seçin:��6i   $	��Top=�6�   $	��159��6�   $��Comic Sans MS)�6�   $��16(�6�   $
��Bold"�6�   $��Italic$�6�   �� 5   
   -   5         ��Name$ Goster��5      $��
20,521,0,0q�6!   $
��Left=�65   $	��Top=�6P   $��58��6h   $��72��6t   .��5      �� $��47��6   $��69��6%   .+�5      �� $��Pictures/Goster.png��6   $��30��65    J� RenderTransformOrigin$ -0.033,0.433&�6A   $��0,0,5,0q�6f   �� 5      $��21��6   $��
    Goster��6)   $
��Left=�6;   $��69��6V   5      5      �� 5    
   -	   5        $	 mcctablo��5       $
��Left=�6J   $��
10,182,0,0q�6e   $	��Top=�6y   $	��159��6�   $��30��6�   �� 5!   
   -
   5        $	 kisiler_combobox��5!      $
��Left=�6Y   $��
10,244,0,0q�6t   $	��Top=�6�   $	��159��6�   $��30��6�   �� 5"   
   -   5         ��Name$ Tarih_Aralik��5"      $��Tarih Aralık:��6W   $
��Left=�6o   $��
10,297,0,0q�6�   $	��Top=�6�   $	��125��6�   $��22��6�   $
��Bold"�6�   $��Italic$�6�   $��Comic Sans MS)�6�   $��16(�6  (  "System.Windows.Controls.DatePicker  5#   
   -   5          Name$ TarihBaslangic��5#      $
��Left=�6Z   $��
10,334,0,0q�6u   $	��Top=�6�     5$   
   -   5        $ 
TarihBitis��5$      $
��Left=�6R   $��126,334,0,0q�6m   $	��Top=�6�   �� 5%   
   -   5        $
 
textBlock1��5%      $
��Left=�6(   $��117,338,0,0q�6C   $
��Wrap=�6X   $��-��6l   $	��Top=�6u   $��20��6�   $��9��6�   $ 
0.056,0.95&�6�   $
��Bold"�6�   �� 5&   
   -   5        $ Tutar_Aralik��5&      $��Tutar Aralık:��6W   $
��Left=�6o   $��
10,390,0,0q�6�   $	��Top=�6�   $
��Bold"�6�   $��Italic$�6�   $��Comic Sans MS)�6�   $��16(�6�   $	��125��6  $��20��6  �� 5'   
   -   5         ��Name$ TutarMin��5'      $
��Left=�6$   $��23��6\   $��
10,415,0,0q�6h    �� TextWrapping$
 Wrap=�6|   $	��Top=�6�   $	��102��6�   �� 5(   
   -   5        $ TutarMax��5(      $
��Left=�6$   $��22��6\   $��126,416,0,0q�6h   $
 Wrap=�6}   $	��Top=�6�   $	��102��6�   �� 5)   
   -   5        $
 textBlock1_Copy��5)      $
��Left=�6-   $��117,416,0,0q�6H   $
��Wrap=�6]   $��-��6q   $	��Top=�6z   $��20��6�   $��9��6�   $ 
0.056,0.95&�6�   $
��Bold"�6�   �� 5*   
   -   5         ��Name$ Olcut_kontrol��5*      $��Pictures/Tik_Isareti.png��6'   $
��Left=�6I   $��30��6d   $��198,112,0,0q�6p   $	��Top=�6�   $��30��6�   $ 1,0.705&�6�   �� 5+   
   -   5        $ mcc_kontrol��5+      $��Pictures/Tik_Isareti.png��6%   $
��Left=�6G   $��30��6b   $��198,182,0,0q�6n   $	��Top=�6�   $��30��6�   $ 1,0.705&�6�   �� 5,   
   -   5        $ kisi_kontrol��5,      $��Pictures/Tik_Isareti.png��6&   $
��Left=�6H   $��30��6c   $��198,244,0,0q�6o   $	��Top=�6�   $��30��6�   $ 1,0.705&�6�   �� 5-   
   -   5        $ Tarih_kontrol��5-      $��Pictures/Tik_Isareti.png��6'   $
��Left=�6I   $��30��6d   $��198,363,0,0q�6p   $	��Top=�6�   $��30��6�   $ 1,0.705&�6�   �� 5.   
   -   5        $ tutar_kontrol��5.      $��Pictures/Tik_Isareti.png��6'   $
��Left=�6I   $��30��6d   $��198,443,0,0q�6p   $	��Top=�6�   $��30��6�   $ 1,0.705&�6�   �� 5/   
   -   5        $
 tarih_wrong��5/      $
��Left=�6)   $��
10,358,0,0q�6D   $
��Wrap=�6X   $��Yanlış Girdi !��6l   $	��Top=�6�   $	��120��6�   
���<��$y�Visible=�6�   $��Italic$�6�   $
��Bold"�6�   $��Comic Sans MS)�6�   �� 50   
   -   5        $
 tutar_wrong��50      $
��Left=�6)   $��
10,441,0,0q�6D   $
��Wrap=�6X   $��Yanlış Girdi !��6l   $	��Top=�6�   $	��120��6�   
���<��$y�Visible=�6�   $��Italic$�6�   $
��Bold"�6�   $��Comic Sans MS)�6�   �� 51   
   -   5        $
 
odenenpara��51      $��710,63,13,0q�6(   $
��Wrap=�6=   $	��Top=�6Q   $��Comic Sans MS)�6i   $��16(�6�   $
��Bold"�6�   $��Italic$�6�   $��67��6�   �� 6�   $�Toplam Odenen Para��6�   �� 6�   $�:��6�   6�   �� 52   
   -   5        $
 Bilgi��52      $
��Left=�6#   $��
255,65,0,0q�6>   $
��Wrap=�6R   $��	TextBlock��6f   $	��Top=�6w   $��Comic Sans MS)�6�   $
��Bold"�6�   $��Italic$�6�   $��16(�6�   $��40��6�   $	��165��6�   �� 53   
   -   5        $	
 Kar��53      $��130,480,0,0q�6!   $
��Wrap=�66   $	��Top=�6J   $��Comic Sans MS)�6b   $��16(�6}   $
��Bold"�6�   $��Italic$�6�   $��75��6�   $
��Left=�6�   $	��105��6�   �� 6�   $�Elde Edilen Toplam ��6�   �� 6  $�K��6	  �� 6  $�ar��6  �� 6$  $�:��6(  �� 63  $�186 ₺��67  �� 6F  �� 6R  6Y  54      55      