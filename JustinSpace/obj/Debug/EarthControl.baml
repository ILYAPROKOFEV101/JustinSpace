   M S B A M L   `   `   `  ����  JustinSpace    JustinSpace.EarthControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      $	��500��5      $	��500��6     � 	Resources  5         EarthRotationStoryboard g� RepeatBehavior �� 
TargetName �� TargetProperty W� From	 W� To g� Duration��%�   &	        �� 5   
   $ Forever��65   .��5      W� $ EarthRotateTransform��6   $ Angle�5	      $ 0X�5
      $	 360X�6'   $ 0:2:30D�5      5      5      .��5      � .+�5   
   �� $	��500��6   $	��500��6   $��Center=�6*   $��Center=�6G   z�5      �� 5      -   5        EarthRotateTransform�� �� CenterX$	 250Q�5   @    �� CenterY$	 250Q�6N   .+�5      A� $	��500��6   $	��500��6"   
������ ��5      �� 5      	 �� ImageSource$5	 /pack://application:,,,/Assets/earth_texture.png��6!   
 j� Stretch$
 UniformToFill=�6_   5      A� 5      $	��500��6   $	��500��6"   $��10��6/    J� Opacity$	 0.4Q�6D    J� IsHitTestVisible .  ��5      
� 5       
� Center$ 0.5,0.5&�6*    
� RadiusX$	 0.5Q�6;    
� RadiusY$	 0.5Q�6I   .��5       �  � Color$ #00FFFF��6'    � Offset$	 0.6Q�67   � 5!      $ Transparent��6'   $	 1.0Q�6;   5"      5$      A� 5'      $	��520��6   $	��520��6"   $��8��6/   $
 0.25Q�6C    .  ��5(      
� 5)      $ 0.5,0.5&�6*   $	 0.5Q�6;   $	 0.5Q�6I   .��5*      � $ #80DEEA��6'   $	 0.0Q�67   � 5+      $ Transparent��6'   $	 1.0Q�6;   5,      5.      A� 51      $	��500��6   $	��500��6"   $��10��6/   $	 0.1Q�6D    .  ��52      
� 53      $ 0.5,0.5&�6*   $	 0.5Q�6;   $	 0.5Q�6I   .��54      � $ #00FFFF��6'   $	 0.8Q�67   � 55      $ Transparent��6'   $	 1.0Q�6;   56      58      59      5:      5;      