#pragma checksum "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\Login\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7bf70752c0ed3752f0f90087f4cba3b4ee57bbf5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Login_Profile), @"mvc.1.0.view", @"/Views/Login/Profile.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Login/Profile.cshtml", typeof(AspNetCore.Views_Login_Profile))]
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
#line 1 "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\_ViewImports.cshtml"
using Exaxxi;

#line default
#line hidden
#line 2 "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\_ViewImports.cshtml"
using Exaxxi.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7bf70752c0ed3752f0f90087f4cba3b4ee57bbf5", @"/Views/Login/Profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95bd1cf985e89aa2e253b21b425cbebda8f1af7e", @"/Views/_ViewImports.cshtml")]
    public class Views_Login_Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Exaxxi.Models.Users>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(28, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\Login\Profile.cshtml"
  
    ViewData["Title"] = "Profile";

#line default
#line hidden
            BeginContext(73, 119, true);
            WriteLiteral("\r\n<h2>Profile</h2>\r\n\r\n<div>\r\n    <h4>Users</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(193, 40, false);
#line 14 "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\Login\Profile.cshtml"
       Write(Html.DisplayNameFor(model => model.name));

#line default
#line hidden
            EndContext();
            BeginContext(233, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(277, 36, false);
#line 17 "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\Login\Profile.cshtml"
       Write(Html.DisplayFor(model => model.name));

#line default
#line hidden
            EndContext();
            BeginContext(313, 58, true);
            WriteLiteral("\r\n        </dd>               \r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(372, 41, false);
#line 20 "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\Login\Profile.cshtml"
       Write(Html.DisplayNameFor(model => model.email));

#line default
#line hidden
            EndContext();
            BeginContext(413, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(457, 37, false);
#line 23 "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\Login\Profile.cshtml"
       Write(Html.DisplayFor(model => model.email));

#line default
#line hidden
            EndContext();
            BeginContext(494, 51, true);
            WriteLiteral("\r\n        </dd>        \r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(546, 47, false);
#line 26 "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\Login\Profile.cshtml"
       Write(Html.DisplayNameFor(model => model.score_buyer));

#line default
#line hidden
            EndContext();
            BeginContext(593, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(637, 43, false);
#line 29 "C:\Users\Kiet Nguyen\Desktop\project exaxxi\Exaxxi\Exaxxi\Exaxxi\Views\Login\Profile.cshtml"
       Write(Html.DisplayFor(model => model.score_buyer));

#line default
#line hidden
            EndContext();
            BeginContext(680, 58, true);
            WriteLiteral("\r\n        </dd>               \r\n    </dl>\r\n</div>\r\n<div>\r\n");
            EndContext();
            BeginContext(804, 8, true);
            WriteLiteral("</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Exaxxi.Models.Users> Html { get; private set; }
    }
}
#pragma warning restore 1591
