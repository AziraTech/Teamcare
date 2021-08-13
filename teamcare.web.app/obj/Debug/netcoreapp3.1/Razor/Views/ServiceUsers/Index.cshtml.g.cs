#pragma checksum "C:\Users\NK\source\repos\teamcare.web.app\src\teamcare.web.app\Views\ServiceUsers\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "764f0aee43725136850ccace6816a4f3065f41d0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ServiceUsers_Index), @"mvc.1.0.view", @"/Views/ServiceUsers/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\NK\source\repos\teamcare.web.app\src\teamcare.web.app\Views\_ViewImports.cshtml"
using teamcare.web.app;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"764f0aee43725136850ccace6816a4f3065f41d0", @"/Views/ServiceUsers/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aabac6f09acc30f5996e90dc9ae8be515452bd3d", @"/Views/_ViewImports.cshtml")]
    public class Views_ServiceUsers_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<teamcare.business.Models.ServiceUserModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ServiceUserCard", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ServiceUserListItem", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ServiceUserCreate", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/page_script/serviceUser.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\NK\source\repos\teamcare.web.app\src\teamcare.web.app\Views\ServiceUsers\Index.cshtml"
  
	ViewData["Title"] = "Patients List";
	Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<!--begin::Toolbar-->
<div class=""d-flex flex-wrap flex-stack pb-7"">
	<!--begin::Title-->
	<div class=""d-flex flex-wrap align-items-center my-1"">
		<h3 class=""fw-bolder me-5 my-1"">Users (38)</h3>
		<!--begin::Search-->
		<div class=""d-flex align-items-center position-relative my-1"">
			<!--begin::Svg Icon | path: icons/duotone/General/Search.svg-->
			<span class=""svg-icon svg-icon-3 position-absolute ms-3"">
				<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
					<g stroke=""none"" stroke-width=""1"" fill=""none"" fill-rule=""evenodd"">
						<rect x=""0"" y=""0"" width=""24"" height=""24"" />
						<path d=""M14.2928932,16.7071068 C13.9023689,16.3165825 13.9023689,15.6834175 14.2928932,15.2928932 C14.6834175,14.9023689 15.3165825,14.9023689 15.7071068,15.2928932 L19.7071068,19.2928932 C20.0976311,19.6834175 20.0976311,20.3165825 19.7071068,20.7071068 C19.3165825,21.0976311 18.6834175,21.0976311 18.2928932,20.7071068 ");
            WriteLiteral(@"L14.2928932,16.7071068 Z"" fill=""#000000"" fill-rule=""nonzero"" opacity=""0.3"" />
						<path d=""M11,16 C13.7614237,16 16,13.7614237 16,11 C16,8.23857625 13.7614237,6 11,6 C8.23857625,6 6,8.23857625 6,11 C6,13.7614237 8.23857625,16 11,16 Z M11,18 C7.13400675,18 4,14.8659932 4,11 C4,7.13400675 7.13400675,4 11,4 C14.8659932,4 18,7.13400675 18,11 C18,14.8659932 14.8659932,18 11,18 Z"" fill=""#000000"" fill-rule=""nonzero"" />
					</g>
				</svg>
			</span>
			<!--end::Svg Icon-->
			<input type=""text"" id=""kt_filter_search"" class=""form-control form-control-white form-control-sm w-150px ps-9"" placeholder=""Search"" />
		</div>
		<!--end::Search-->
	</div>
	<!--end::Title-->
	<!--begin::Controls-->
	<div class=""d-flex flex-wrap my-1"">
		<!--begin::Tab nav-->
		<ul class=""nav nav-pills me-6 mb-2 mb-sm-0"">
			<li class=""nav-item m-0"">
				<a class=""btn btn-sm btn-icon btn-light btn-color-muted btn-active-primary me-3 active"" data-bs-toggle=""tab"" href=""#kt_project_users_card_pane"">
					<!--begin::Svg Icon | pa");
            WriteLiteral(@"th: icons/duotone/Layout/Layout-4-blocks-2.svg-->
					<span class=""svg-icon svg-icon-2"">
						<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
							<g stroke=""none"" stroke-width=""1"" fill=""none"" fill-rule=""evenodd"">
								<rect x=""5"" y=""5"" width=""5"" height=""5"" rx=""1"" fill=""#000000"" />
								<rect x=""14"" y=""5"" width=""5"" height=""5"" rx=""1"" fill=""#000000"" opacity=""0.3"" />
								<rect x=""5"" y=""14"" width=""5"" height=""5"" rx=""1"" fill=""#000000"" opacity=""0.3"" />
								<rect x=""14"" y=""14"" width=""5"" height=""5"" rx=""1"" fill=""#000000"" opacity=""0.3"" />
							</g>
						</svg>
					</span>
					<!--end::Svg Icon-->
				</a>
			</li>
			<li class=""nav-item m-0"">
				<a class=""btn btn-sm btn-icon btn-light btn-color-muted btn-active-primary"" data-bs-toggle=""tab"" href=""#kt_project_users_table_pane"">
					<!--begin::Svg Icon | path: icons/duotone/Layout/Layout-horizontal.svg-->
					<span class=""svg-icon svg");
            WriteLiteral(@"-icon-2"">
						<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
							<g stroke=""none"" stroke-width=""1"" fill=""none"" fill-rule=""evenodd"">
								<rect x=""0"" y=""0"" width=""24"" height=""24"" />
								<rect fill=""#000000"" opacity=""0.3"" x=""4"" y=""5"" width=""16"" height=""6"" rx=""1.5"" />
								<rect fill=""#000000"" x=""4"" y=""13"" width=""16"" height=""6"" rx=""1.5"" />
							</g>
						</svg>
					</span>
					<!--end::Svg Icon-->
				</a>
			</li>
		</ul>
		<!--end::Tab nav-->
	</div>
	<!--end::Controls-->
</div>
<!--end::Toolbar-->
<!--begin::Tab Content-->
<div class=""tab-content"">
	<!--begin::Tab pane-->
	<div id=""kt_project_users_card_pane"" class=""tab-pane fade show active"">
		<!--begin::Row-->
		<div class=""row g-6 g-xl-9"">
");
#nullable restore
#line 76 "C:\Users\NK\source\repos\teamcare.web.app\src\teamcare.web.app\Views\ServiceUsers\Index.cshtml"
             if (Model != null)
			{
				foreach (var item in Model)
				{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "764f0aee43725136850ccace6816a4f3065f41d09217", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 80 "C:\Users\NK\source\repos\teamcare.web.app\src\teamcare.web.app\Views\ServiceUsers\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = item;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 81 "C:\Users\NK\source\repos\teamcare.web.app\src\teamcare.web.app\Views\ServiceUsers\Index.cshtml"
				}
			}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"		</div>
		<!--end::Row-->
	</div>
	<!--end::Tab pane-->
	<!--begin::Tab pane-->
	<div id=""kt_project_users_table_pane"" class=""tab-pane fade"">
		<!--begin::Card-->
		<div class=""card card-flush"">
			<!--begin::Card body-->
			<div class=""card-body pt-0"">
				<!--begin::Table container-->
				<div class=""table-responsive"">
					<!--begin::Table-->
					<table id=""kt_project_users_table"" class=""table table-row-bordered table-row-dashed gy-4 align-middle fw-bolder"">
						<!--begin::Head-->
						<thead class=""fs-7 text-gray-400 text-uppercase"">
							<tr>
								<th class=""min-w-250px"">Manager</th>
								<th class=""min-w-150px"">Date</th>
								<th class=""min-w-90px"">Amount</th>
								<th class=""min-w-90px"">Status</th>
								<th class=""min-w-50px text-end"">Details</th>
							</tr>
						</thead>
						<!--end::Head-->
						<!--begin::Body-->
						<tbody class=""fs-6"">
");
#nullable restore
#line 110 "C:\Users\NK\source\repos\teamcare.web.app\src\teamcare.web.app\Views\ServiceUsers\Index.cshtml"
                             if (Model != null)
							{
								foreach (var item in Model)
								{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t\t\t\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "764f0aee43725136850ccace6816a4f3065f41d012297", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 114 "C:\Users\NK\source\repos\teamcare.web.app\src\teamcare.web.app\Views\ServiceUsers\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = item;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 115 "C:\Users\NK\source\repos\teamcare.web.app\src\teamcare.web.app\Views\ServiceUsers\Index.cshtml"
								}
							}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"						</tbody>
						<!--end::Body-->
					</table>
					<!--end::Table-->
				</div>
				<!--end::Table container-->
			</div>
			<!--end::Card body-->
		</div>
		<!--end::Card-->
	</div>
	<!--end::Tab pane-->
</div>
<!--end::Tab Content-->
<!-- ------------------------------------- Model Portion ------------------------------------- -->
<!--begin::Modals-->
<!--begin::Modal - Create Service Users-->
<div class=""modal fade"" id=""kt_modal_create_app"" tabindex=""-1"" aria-hidden=""true"">
	<!--begin::Modal dialog-->
	<div class=""modal-dialog modal-dialog-centered mw-900px"">
		<!--begin::Modal content-->
		<div class=""modal-content"">
			<!--begin::Modal header-->
			<div class=""modal-header"">
				<!--begin::Modal title-->
				<h2>Create Service User's Information</h2>
				<!--end::Modal title-->
				<!--begin::Close-->
				<div class=""btn btn-sm btn-icon btn-active-color-primary"" data-bs-dismiss=""modal"">
					<!--begin::Svg Icon | path: icons/duotone/Navigation/Close.svg-->
					<span c");
            WriteLiteral(@"lass=""svg-icon svg-icon-1"">
						<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""24px"" height=""24px"" viewBox=""0 0 24 24"" version=""1.1"">
							<g transform=""translate(12.000000, 12.000000) rotate(-45.000000) translate(-12.000000, -12.000000) translate(4.000000, 4.000000)"" fill=""#000000"">
								<rect fill=""#000000"" x=""0"" y=""7"" width=""16"" height=""2"" rx=""1"" />
								<rect fill=""#000000"" opacity=""0.5"" transform=""translate(8.000000, 8.000000) rotate(-270.000000) translate(-8.000000, -8.000000)"" x=""0"" y=""7"" width=""16"" height=""2"" rx=""1"" />
							</g>
						</svg>
					</span>
					<!--end::Svg Icon-->
				</div>
				<!--end::Close-->
			</div>
			<!--end::Modal header-->
			<!--begin::Modal body-->
			<div class=""modal-body scroll-y py-lg-10 px-lg-10"">
				");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "764f0aee43725136850ccace6816a4f3065f41d016055", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\t\t\t</div>\r\n\t\t\t<!--end::Modal body-->\r\n\t\t</div>\r\n\t\t<!--end::Modal content-->\r\n\t</div>\r\n\t<!--end::Modal dialog-->\r\n</div>\r\n<!--end::Modal - Create Service Users-->\r\n<!--end::Modals-->\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "764f0aee43725136850ccace6816a4f3065f41d017385", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<teamcare.business.Models.ServiceUserModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
