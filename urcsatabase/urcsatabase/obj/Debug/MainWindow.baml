   M S B A M L   `   `   `  ����F  Burcsatabase, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:urcsatabaseurcsatabase  5   	       urcsatabase.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:urcsatabase     5   	     � Title$  Flight��5   	   $	��615��6   $	��540��6%    � 
ResizeMode$ CanMinimize=�61    � Icon$. (pack://application:,,,/images/player.png��5	   	   .��5
      �� -   5        $��MyCanvas��5
      |�. 
���   �.+�5   
   �� -   5        $��player��5      $��50��6"   $��60��6.   
��� ��� �� Left$	 246��6G   
 �� Top$	 498��6Y   �� 5   
   -   5        $��	scoreText��5      $��Score 0��6!   $��18(�63   $
��Bold"�6A   
�������6S   �� 5   
   -   5        $��
damageText��5      $��Life: 0��6"   $��18(�64   $
��Bold"�6B   
�������$	 430��6g   �� 5   
   -   5        $��bossdamageText��5      $��Boss's Life: 0��6&   $��18(�6?   $
��Bold"�6M   
�������$y�Hidden=�6r   $ 34��6�   �� 5   
   -   5         ��Name$ 
playButton��5      $
��PLAY��6%   $��18(�64   $
��Bold"�6B   $	��102��6T   $	��189��6a   $	 188��6m   $	 269��6   
����� �6�   �� 5   
   -   5        $ restartButton��5      $��RESTART��6(   $��18(�6:   $
��Bold"�6H   $	��102��6Z   $	��189��6g   $	 188��6s   $	 269��6�   
����� �$y�Hidden=�6�   �� 5   
   -   5         ��Name$ endText��5      $�� ��6!   $��18(�6,   $
��Bold"�6:   
�������$	 188��6_   $	��289��6q   $	 160��6}   $	��104��6�   �� 5   
   -	   5        $��highscoreText��5      $��HIGH SCORE: ��6%   $��18(�6<   $
��Bold"�6J   
�������$	 206��6o   �� 5   
   -
   5        $ musicButton��5      $��	Music: ON��6&   $��18(�6:   $
��Bold"�6H   $	��102��6Z   $	��189��6g   $	 188��6s   $	 131��6�   
����� �6�   5      5      