using System;

namespace Demo.API
{
    public static class AppData
    {
        public static string Host 
        {
            get { return "http://tapki.azurewebsites.net/api/"; }
        }

        public static string YaSuccessUri 
        {
            get { return "http://tapki.azurewebsites.net/yandex/success"; }
        }

        public static string YaFailUri 
        {
            get { return "http://tapki.azurewebsites.net/yandex/fail"; }
        }
    }
}

