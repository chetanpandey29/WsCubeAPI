using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS_CubeAPI.App_Start
{
    public partial class Startup
    {
        static Startup()
        {
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            //app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
