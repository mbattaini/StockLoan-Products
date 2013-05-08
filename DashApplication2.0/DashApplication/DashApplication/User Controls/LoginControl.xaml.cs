using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using C1.Silverlight;

using DashApplication.ServicePosition;
using DashApplication.CustomClasses;

namespace DashApplication
{    
    public partial class LoginControl : UserControl
    {
        private enum ViewStates
        {
            SignIn,
            Login,
            LoggedIn,
            Load,
            Logout
        }

        private PositionClient psClient;
        
        Storyboard storyBoard;
        DoubleAnimation dblAnimation;
        RotateTransform rtransform;

        Storyboard sbGrow;

        Storyboard storyBoardRec;
        DoubleAnimation dblAnimationRec;
        TranslateTransform sTransform;


        
        public LoginControl()
        {
            InitializeComponent();

            psClient = new PositionClient();
            psClient.UserValidationGetCompleted += psClient_UserValidationGetCompleted;
            psClient.WebSecurityProfileDataSetGetCompleted += psClient_WebSecurityProfileDataSetGetCompleted;
        }

        void psClient_WebSecurityProfileDataSetGetCompleted(object sender, WebSecurityProfileDataSetGetCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                UserInformation.UserProfile = Functions.ConvertToDataTable(e.Result, "User");

                CustomEvents.UpdateUserIdInformation(UserInformation.UserId, UserInformation.Password);
                ChangeViewState(ViewStates.LoggedIn);
            }

            
            EndLoad();
        }

        private void BeginLoad()
        {                      
            LoadingLabel.Content = "Logging In " + UserIdTextBox.Text;

            RotateImage(false);
        }

        private void EndLoad()
        {                                 
            RotateImage(true);           
        }

        private void RotateImage(bool isStop)
        {
            if (!isStop)
            {
                storyBoard = new Storyboard();
                dblAnimation = new DoubleAnimation();
                rtransform = new RotateTransform();
                rtransform.Angle = 0;

                dblAnimation.From = 0;
                dblAnimation.To = 360;
                dblAnimation.RepeatBehavior = RepeatBehavior.Forever;

                LoadImage.RenderTransform = rtransform;

                Storyboard.SetTarget(dblAnimation, rtransform);
                Storyboard.SetTargetProperty(dblAnimation, new PropertyPath(RotateTransform.AngleProperty));
                
                storyBoard.Children.Add(dblAnimation);
                storyBoard.Begin();
            }
           else
            {
                if (storyBoard.Children.Count > 0)
                {
                    storyBoard.Stop();
                }
            }
        }

        void psClient_UserValidationGetCompleted(object sender, UserValidationGetCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                EndLoad();

                if (e.Result)
                {                    
                    UserInformation.UserId = UserIdTextBox.Text;
                    UserInformation.Password = UserPasswordBox.Password;
                    UserIdLabel.Content = "Welcome user, " + UserInformation.UserId + "!";                    
                    
                    psClient.WebSecurityProfileDataSetGetAsync(UserInformation.UserId);                  
                }
                else
                {
                    ChangeViewState(ViewStates.Login);                    
                    UserPasswordBox.Password = "";                   
                    C1MessageBox.Show("Incorrect userid / password");
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            psClient.UserValidationGetAsync(UserIdTextBox.Text, UserPasswordBox.Password);

            ChangeViewState(ViewStates.Load);
            
            BeginLoad();            
        } 
      
        private void ChangeViewState(ViewStates vm)
        {
           
            switch (vm)
            {
                case ViewStates.LoggedIn:
                    CustomAnimations.FadeOut(LayoutRoot);
                    SignOnLayout.Visibility = Visibility.Collapsed;
                    LogInLayout.Visibility = Visibility.Collapsed;
                    LoggedInLayout.Visibility = Visibility.Visible;
                    LoadLayout.Visibility = Visibility.Collapsed;
                    CustomAnimations.FadeIn(LayoutRoot);
                    break;

                case ViewStates.Login:
                    CustomAnimations.FadeOut(LayoutRoot);
                    SignOnLayout.Visibility = Visibility.Collapsed;
                    LogInLayout.Visibility = Visibility.Visible;
                    LoggedInLayout.Visibility = Visibility.Collapsed;
                    LoadLayout.Visibility = Visibility.Collapsed;
                    CustomAnimations.FadeIn(LayoutRoot);
                    break;

                case ViewStates.Logout:
                    CustomAnimations.FadeOut(LayoutRoot);
                    SignOnLayout.Visibility = Visibility.Visible;
                    LogInLayout.Visibility = Visibility.Collapsed;
                    LoggedInLayout.Visibility = Visibility.Collapsed;
                    LoadLayout.Visibility = Visibility.Collapsed;
                    CustomAnimations.FadeIn(LayoutRoot);
                    break;

                case ViewStates.SignIn:
                    CustomAnimations.FadeOut(LayoutRoot);
                    SignOnLayout.Visibility = Visibility.Visible;
                    LogInLayout.Visibility = Visibility.Collapsed;
                    LoggedInLayout.Visibility = Visibility.Collapsed;
                    LoadLayout.Visibility = Visibility.Collapsed;
                    CustomAnimations.FadeIn(LayoutRoot);
                    break;
                
                case ViewStates.Load:
                    SignOnLayout.Visibility = Visibility.Collapsed;
                    LogInLayout.Visibility = Visibility.Collapsed;
                    LoggedInLayout.Visibility = Visibility.Collapsed;
                    LoadLayout.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeViewState(ViewStates.Login);            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            UserInformation.UserId = "";
            UserInformation.Password = "";
            UserInformation.UserProfile = null;
            
            CustomEvents.UpdateUserIdInformation("", "");            
            ChangeViewState(ViewStates.Logout);            
        }

        private void MainTabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             
        }

        private void C1TabControl_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {            
            ChangeViewState(ViewStates.Login);     
        }      
    }
}
