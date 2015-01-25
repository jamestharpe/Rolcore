using System.ServiceModel.Syndication;

namespace Utilla.Web.Syndication
{
    public interface ISyndicationItemProvider
    {
        SyndicationItem FullItem { get; }
        SyndicationItem PartialItem { get; }
    }
}
