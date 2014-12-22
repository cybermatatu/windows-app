﻿using GalaSoft.MvvmLight;
using System;
using System.Text.RegularExpressions;
using wallabag.Common;
using Windows.UI;

namespace wallabag.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        public Models.Item Model { get; set; }
        
        public string Title
        {
            get
            {
                // RegExp to remove multiple whitespaces (including newline etc.) in title.
                Regex r = new Regex("\\s+");
                return r.Replace(Model.Title, " ");
            }
            set
            {
                if (value != Model.Title)
                {
                    Model.Title = value;
                    RaisePropertyChanged(() => Title);
                }
            }
        }

        public string Content
        {
            get {
                var content =
                    "<html><head><link rel=\"stylesheet\" href=\"ms-appx-web:///Assets/css/wallabag.css\" type=\"text/css\" media=\"screen\" />" + generateCSS() + "</head>" +
                        Model.Content +
                    "</html>";
                return content; 
            }
            set
            {
                if (value != Model.Content)
                {
                    Model.Content = value;
                    RaisePropertyChanged(() => Content);
                }
            }
        }

        public string ContentWithTitle
        {
            get
            {
                var content =
                    "<html><head><link rel=\"stylesheet\" href=\"ms-appx-web:///Assets/css/wallabag.css\" type=\"text/css\" media=\"screen\" />" + generateCSS() + "</head>" +
                        "<h1 class=\"wallabag-header\">" + Title + "</h1>" +
                        Model.Content +
                    "</html>";
                return content;
            }
        }
       
        public Uri Url
        {
            get { return Model.Url; }
            set
            {
                if (value != Model.Url)
                {
                    Model.Url = value;
                    RaisePropertyChanged(() => Url);
                }
            }
        }

        private string CSSproperty(string name, object value)
        {
            if (value.GetType() != typeof(Color))
            {
                return string.Format("{0}: {1};", name, value.ToString());
            }
            else
            {
                var color = (Color)value;
                var tmpColor = string.Format("rgba({0}, {1}, {2}, {3})", color.R, color.G, color.B, color.A);
                return string.Format("{0}: {1};", name, tmpColor);
            }
        }
        private string generateCSS()
        {
            double fontSize = ApplicationSettings.GetSetting<double>("fontSize", 16);
            double lineHeight = ApplicationSettings.GetSetting<double>("lineHeight", 1.5);

            var tmpSettingsVM = new SettingsViewModel();
            string css = "body {" +
                CSSproperty("font-size", fontSize + "px") +
                CSSproperty("line-height", lineHeight.ToString().Replace(",", ".")) +
                CSSproperty("color", tmpSettingsVM.textColor.Color) +
                CSSproperty("background", tmpSettingsVM.Background.Color) +
                "}";
            return "<style>" + css + "</style>";
        }

        public ItemViewModel() { Model = new Models.Item(); }
        public ItemViewModel(Models.Item M) { Model = M; }
    }
}
