using System.Diagnostics;

namespace EstoqueLab.UI.Helpers
{
    public class AppSettings
    {
        public IConfiguration Configuration { get; }
        private static String IP { get; set; }
        public AppSettings(IConfiguration configuration)
        {
            Configuration = configuration;
            if (Debugger.IsAttached)
            {
                var conf = Configuration.GetSection("API_Access:UrlApiBaseDev").Value;
                HostApi = conf;
            }
            else
                HostApi = Configuration.GetSection("API_Access:UrlApiBase").Value;

        }

        public static String HostApi { get; set; }
    }


    public class Methods
    {
        public static String Categoria = AppSettings.HostApi + "/api/Categoria";
        public static String Produto = AppSettings.HostApi + "/api/Produto";

    }
}
