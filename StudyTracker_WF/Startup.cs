using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudyTracker_WF.Startup))]
namespace StudyTracker_WF
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
