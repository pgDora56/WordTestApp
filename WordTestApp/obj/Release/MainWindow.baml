   M S B A M L   `   `   `  ���� F  BWordTestApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WordTestAppWordTestApp      WordTestApp.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:WordTestApp       � Title$  WordsTester��$	��400��$	��640��.��� .+��� $%��単語てすたー Made by Dora��$
��Left=�$��	10,10,0,0q�$	��Top=�$��FTT-TsukuGo R)��� -    ��Name$ 	TopButton��$��データを開く��$
��Left=�$	��Top=�$��83��$��	17,32,0,0q�$��FTT-TsukuGo R)�$��30���� -    ��Name$ fileUrl��$�� ��$
��Left=�$��
110,32,0,0q�$	��Top=�$��FTT-TsukuGo R)��� -   $ StartButton��$��	開　始��$
��Left=�$	��Top=�$��83��$��	17,72,0,0q�$��FTT-TsukuGo R)�$��30��{�.  �� -   $ questionArea��$�� ��$
��Left=�$��
17,110,0,0q�$	��593��$	��100��$	��Top=�$��FTT-TsukuGo R)�$��24(��� -    ��Name$ 
answerArea��$
��Left=�$��110,232,0,100q� �� TextWrapping$
 Wrap=�$�� ��$	��250��$��Adobe Devanagari)�$��20(��� $��Answer:��$��20(�$
��Left=�$��
30,232,0,0q�$	��Top=�$��FTT-TsukuGo R)��� -   $ answerButton��$��解答��$��18(�$
��Left=�$	��Top=�$��52��$��370,232,0,0q�$��FTT-TsukuGo R)�$��30��{�.   �� 	IsDefault .  J� RenderTransformOrigin$ 3.173,0.676&��� $��結果:��$��20(�$
��Left=�$��
46,288,0,0q�$	��Top=�$��FTT-TsukuGo R)��� -   $ 	AnswerMsg��$��20(�$
��Left=�$��110,288,0,0q�$��60��$	��500��$	��Top=�$��FTT-TsukuGo R)�$
 Wrap=� �� 
IsReadOnly . �� -   $ 	nowStatus��$�� ��$
��Left=�$��
110,72,0,0q�$	��Top=�$��FTT-TsukuGo R)�