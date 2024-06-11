using System;
namespace Core.CrossCuttingConcerns.Logging.SeriLog.Messages
{
    public static class SerilogMessages
    {
        public static string NullOptionsMessage => "You have sent a blank value! Something went wrong. Please try again.";
        public static string LoginMessage = "Giriş yaptınız.";
        public static string RegisterMessage = "Kayıt oldunuz.";
    }
}

  