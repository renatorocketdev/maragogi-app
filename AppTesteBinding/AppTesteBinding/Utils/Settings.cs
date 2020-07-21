using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AppTesteBinding.Utils
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
    public static class Settings
{
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string InglesKey = "settings_key";
        private static readonly bool InglesDefault = false;

        private const string UsuarioKey = "username_key";
        private static readonly string UsuarioDefault = string.Empty;

        private const string EmailKey = "email_key";
        private static readonly string EmailDefault = string.Empty;

        private const string TelefoneKey = "telefone_key";
        private static readonly string TelefoneDefault = string.Empty;

        private const string SenhaKey = "password_key";
        private static readonly string SenhaDefault = string.Empty;

        private const string LembrarKey = "lembrar_key";
        private static readonly bool LembrarDefault = false;

        private const string Logadokey = "logado_key";
        private static readonly bool LogadoDefault = false;

        private const string FacebookKey = "facebook_key";
        private static readonly bool facebookDefault = false;
        #endregion


        public static bool Ingles
        {
            get { return AppSettings.GetValueOrDefault(InglesKey, InglesDefault); }
            set { AppSettings.AddOrUpdateValue(InglesKey, value); }
        }

        public static bool Logado
        {
            get { return AppSettings.GetValueOrDefault(Logadokey, LogadoDefault); }
            set { AppSettings.AddOrUpdateValue(Logadokey, value); }
        }

        public static string Usuario
        {
            get { return AppSettings.GetValueOrDefault(UsuarioKey, UsuarioDefault); }
            set { AppSettings.AddOrUpdateValue(UsuarioKey, value); }
        }

        public static string Email
        {
            get { return AppSettings.GetValueOrDefault(EmailKey, EmailDefault); }
            set { AppSettings.AddOrUpdateValue(EmailKey, value); }
        }

        public static string Telefone
        {
            get { return AppSettings.GetValueOrDefault(TelefoneKey, TelefoneDefault); }
            set { AppSettings.AddOrUpdateValue(TelefoneKey, value); }
        }

        public static string Senha
        {
            get { return AppSettings.GetValueOrDefault(SenhaKey, SenhaDefault); }
            set { AppSettings.AddOrUpdateValue(SenhaKey, value); }
        }
        public static bool Lembrar
        {
            get { return AppSettings.GetValueOrDefault(LembrarKey, LembrarDefault); }
            set { AppSettings.AddOrUpdateValue(LembrarKey, value); }
        }

        public static bool Facebook
        {
            get { return AppSettings.GetValueOrDefault(FacebookKey, facebookDefault); }
            set { AppSettings.AddOrUpdateValue(FacebookKey, value); }
        }
    }
}