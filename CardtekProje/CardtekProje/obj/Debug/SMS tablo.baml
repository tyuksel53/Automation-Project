   M S B A M L   `   `   `  ����G  CCardtekProje, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CardtekProjeCardtekProje  5   	       CardtekProje.Window3   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:CardtekProje     5   	     � Title$  	SMS Tablo��5   	   $��488.5��6   $	��737��6*    � 
ResizeMode$ CanMinimize=�66    � Icon$ 	icon2.ico��6P   .��5	      � 
����h{�.+�5   
   o� $	��731��6   
����İ�$	��Top=�67   $��28��6L   $	��Top=�6X   $
��Left=�6p   .@�5      m� -   5        $��_Musteri Tablo��5      $��14(�6/   $��Black"�6=   $��Comic Sans MS)�6P   $��Italic$�6k   6~   m� 5      -   5        $��_Kampanya Tablo��5      $��14(�60   $��Comic Sans MS)�6>   $��Italic$�6Y   $
��Bold"�6l   6~   m� 5      -   5        $��_İslem Tablo��5      $��14(�6-   $
��Bold"�6;   $��Comic Sans MS)�6M   $��Italic$�6h   6{   m� 5      $^��X                                                                                        ��6   $y�Hidden=�6y   m� 5      -   5        $��_İstatistikler��5      $��14(�6/   $
��Bold"�6=   $��Comic Sans MS)�6O   $��Italic$�6j   $��Right=�6�   $��Center=�6�   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5         SelectionMode$ Extended=�6@   $
��Left=�6Y   $��	10,95,0,0q�6t   $	��Top=�6�   $	��331��6�   $	��407��6�   �� 5   
   -   5         ��Name$ Goster��5      $��629,189,0,0q�6!   
����İ�$
��Left=�6R   $	��Top=�6m   $��58��6�   $��72��6�   .��5      �� $��47��6   $��69��6%   .+�5      �� $��Pictures/Goster.png��6   $��30��65    J� RenderTransformOrigin$ -0.033,0.433&�6A   $��0,0,5,0q�6f   �� 5      $��16��6   $��      Getir��6)   $
��Left=�6<   $��69��6W   5      5      �� 5   
   -   5        $ Taksitle��5      $��629,277,0,0q�6#   
����İ�$
��Left=�6k   $	��Top=�6�   $��56��6�   $��72��6�   .��5      �� $��47��6   $��69��6%   .+�5      �� $��Pictures/Taksitle.png��6   $��30��67   $ -0.033,0.433&�6C   $��0,0,5,0q�6h   �� 5      $��21��6   $��   Taksitle��6)   5      5       �� 5!   
   -   5        $ Temizle��5!      $��579,370,0,0q�6"   
����İ�$
��Left=�6S   $	��Top=�6n   $��56��6�   $	��122��6�   .��5"      �� $��47��6   $��69��6%   .+�5#      �� $��Pictures/Temizle.png��6   $��30��66   $ -0.033,0.433&�6B   �� 5$      $��16��6   $
��Left=�6)   $	��105��6D   $��-17,0,-19,0q�6P   �� 6g   $� ��6k   �� 6v   $�Secilenleri Temizle��6z   6�   5%      5&      �� 5'   
   -	   5         ��Name$ 	textBlock��5'      $��Comic Sans Ms)�6'   $��14(�6B   $��Italic$�6P   $
��Bold"�6c   $
��Left=�6u   $��
444,33,0,0q�6�   $
��Wrap=�6�   $	��Top=�6�   $	��112��6�   $	��277��6�   $��Seçilen IDler:��6�   �� 5(   
   -
   5        $ Geri_Al��5(      $��446,370,0,0q�6"   
����İ�$
��Left=�6h   $	��Top=�6�   $��56��6�   $��72��6�   .��5)      �� $��47��6   $��69��6%   .+�5*      �� $��Pictures/GeriAl.png��6   $��30��65   $ -0.033,0.433&�6A   $��0,0,5,0q�6f   �� 5+      $��21��6   $��    Geri Al��6)   5,      5-      5/      50      