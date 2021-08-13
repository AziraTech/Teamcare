using System.Collections;
using System.Collections.Generic;
using teamcare.common.Enumerations;

namespace teamcare.web.app.ViewModels
{
    public class PageMetadata
    {
        public string Title { get; set; }

        public SiteSection ActiveSiteSection { get; set; } = SiteSection.Dashboard;

        public IList<BreadcrumbItem> Breadcrumbs { get; set; }
    }

    public class BreadcrumbItem
    {
        public BreadcrumbItem(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
