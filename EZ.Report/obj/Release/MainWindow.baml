   M S B A M L   `   `   `  ���� E  AEZ.Report, Version=0.12.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:EZ.Report	EZ.Report      EZ.Report.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:EZ.Report     -     �Name$  MainWindowSettings��$	��306��$	��277�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=����	#DDFFFFFF � 	Resources    Exit �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� Orientation
 ��Name �� Triggers P� Property P� Value �� 
TargetName  Minimize ��Name  MarginComboBox u� Left
 u� Top u� Right u� Bottom��%~  &	        &	 '    &	 :    �� " ��.���  � �� �� " ��.R�� .+��� $ Foreg��._��� $	 
Horizontal=�.+��� $	
 Img��$��	Close.png���� $��Center=�$��Center=����	#00000000 P�  �@J�IsMouseOver . . ��� $	 Img�� � ��$ 	Close.png���� $ Foreg�� � �� �	#98F04747�� .���  � �� �� .R�� .+��� $
 Head��._��� $��Stretch=�$��Stretch=����	#00000000�� $ 	NameLable��$��Center=�$��Center=����	#00000000�� $��	Min-W.png�� P�  �@J�IsMouseOver . . ��� $
 Head�� � �� �	#FF777777u� $ 16Q�$ 0Q�$ 0Q�$ 0Q�.��� -    �Name$ MainGrid�����	#02000000.+��� -   ���	#FF888888$
��Left=�$��22��$	��Top=�$	��277���� -    ��Name$ LeftBorderLine�����	#FF888888$
��Left=�$	��306��$	��Top=�$��1��$��0,0,0,0q��� -   $ RightBorderLine�����	#FF888888$
��Left=�$	��306��$	��Top=�$��1��$��	276,0,0,0q��� -   $ BottomBorderLine�����	#FF888888$
��Left=�$��9��$	��Top=�$	��276��$��	0,297,0,0q��� -   #��[  $
��Left=�$��22��$��	255,0,0,0q�$	��Top=�$��22��$��0q����	#00000000���	#00000000 � Cursor$
 Hand��$��Center=�$��1q��� -   #��[ $
��Left=�$��22��$��	233,0,0,0q�$	��Top=�$��22��$��0q����	#00000000���	#00000000$
 Hand���� -	   $��	EZ.Report��$
��Left=�$��10,2,0,0q�$	��Top=�$��18��$	��100�����	#FF3E3E3F$��Tahoma)�$
��Left=�$��Center=�$��0,0,0,-3q�$��SemiBold"�&   System.Windows.Media.TextOptions   TextHintingMode$ Fixed=��� -
    ��Name$ AddBTN��$��Добавить��$
��Left=�$��	26,47,0,0q�$	��Top=�$��65�����	#FFDDDDDD���	#FF3E3E3F$
��Bold"�$��Tahoma)�$��25��$
 Hand���� -    ��Name$ CounterLeftTB��$
��Left=�$��
116,47,0,0q�$��99��$	��Top=�$��15��$��25��$��Tahoma)�$
��Bold"����	#FF3E3E3F$
 Hand�� �� TextAlignment$ Center=� �� Padding$ 0,5,0,0q�{�.  �� $
��Left=�$��
131,47,0,0q�$	�� | ��$	��Top=�$��14��$��25��$��Tahoma)�$
��Bold"����	#FF3E3E3F$
 Hand��$ Center=�$ 0,5,0,0q�{�.  �� -   $ CounterRightTB��$
��Left=�$��
146,47,0,0q�$��99��$	��Top=�$��15��$��25��$��Tahoma)�$
��Bold"����	#FF3E3E3F$
 Hand��$ Center=�$ 0,5,0,0q�{�.  �� -   $ 	DeleteBTN��$��Удалить��$
��Left=�$��
186,47,0,0q�$	��Top=�$��65�����	#FFDDDDDD���	#FF3E3E3F$
��Bold"�$��Tahoma)�$��25��$
 Hand���� -   $ LeftBTN��$��<��$
��Left=�$��	26,97,0,0q�$	��Top=�$��25�����	#FFDDDDDD���	#FF3E3E3F$
��Bold"�$��Tahoma)�$��25��$��1,1,1,2q�$
 Hand���� -    ��Name$ NameTB��$
��Left=�$��25��$��	76,97,0,0q� �� TextWrapping$
 Wrap=�$��Название��$	��Top=�$	��125��$��Center=�$��Center=�$��Tahoma)�$
��Bold"����	#00000000���	#00000000���	#FF3E3E3F�� -   $ RightBTN��$��>��$
��Left=�$��
226,97,0,0q�$	��Top=�$��25�����	#FFDDDDDD���	#FF3E3E3F$
��Bold"�$��Tahoma)�$��25��$��1,1,1,2q�$
 Hand���� -   $ PathTB��$
��Left=�$��
26,147,0,0q�$��Путь��$	��Top=�$	��225��$��25��$��Tahoma)�$
��Bold"����	#FF3E3E3F$
 Hand��$ Center=�$ 0,5,0,0q��� -   $ KeyTB��$
��Left=�$��
26,197,0,0q�$'��!Комбинация клавиш��$	��Top=�$	��225��$��25��$��Tahoma)�$
��Bold"����	#FF3E3E3F$
 Hand��$ Center=�$ 0,5,0,0q��� -   $ SettingsBTN��$��Настройки��$
��Left=�$��
26,247,0,0q�$	��Top=�$	��100�����	#FFDDDDDD���	#FF3E3E3F$
��Bold"�$��Tahoma)�$��25��$
 Hand���� -   $ StateBTN��$��Состояние��$
��Left=�$��151,247,0,0q�$	��Top=�$	��100�����	#FFDDDDDD���	#FF3E3E3F$
��Bold"�$��Tahoma)�$��25��$
 Hand���� -   $ AuthorTB��$
��Left=�$��167,297,0,0q��� -    ��Name$
 Link��$"��https://youtu.be/jQRb4DZnhn8?����	#000066CC � Cursor$ Arrow��.�.��� $"��For EMS by OttoDietrich#4987��$	��Top=�$��Tahoma)�$
��Bold"����	#FF3E3E3F$ Center=�$��7(�$��9��$	��110��