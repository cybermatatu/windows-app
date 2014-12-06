﻿using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using wallabag.Common;

namespace wallabag.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        public Models.Item Model { get; set; }
        
        public string Title
        {
            get { return Model.Title; }
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

        private string generateCSS()
        {
            double fontSize = ApplicationSettings.GetSetting<double>("fontSize", 20.0);
            double lineHeight = ApplicationSettings.GetSetting<double>("lineHeight", 1.5);

            string css = "body {" +
                "font-size: " + fontSize + "px;" +
                "line-height: " + lineHeight.ToString().Replace(",",".") +
                "}";
            return "<style>" + css + "</style>";
        }

        public ItemViewModel() { Model = new Models.Item(); }
        public ItemViewModel(Models.Item M) { Model = M; }
    }
}
