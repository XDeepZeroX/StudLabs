#pragma checksum "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d8501d0cc2d2f7528961918d30a26045100d5fd5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MatrixOperations_GetTable), @"mvc.1.0.view", @"/Views/MatrixOperations/GetTable.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/MatrixOperations/GetTable.cshtml", typeof(AspNetCore.Views_MatrixOperations_GetTable))]
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
#line 1 "D:\Projects\StudLab\StudLab\Views\_ViewImports.cshtml"
using StudLab;

#line default
#line hidden
#line 2 "D:\Projects\StudLab\StudLab\Views\_ViewImports.cshtml"
using StudLab.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d8501d0cc2d2f7528961918d30a26045100d5fd5", @"/Views/MatrixOperations/GetTable.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"33f0b1649f701e3fa81b8c72324a795d0164e4c3", @"/Views/_ViewImports.cshtml")]
    public class Views_MatrixOperations_GetTable : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
  
    Layout = "_MatrixOperation";
    int row = ViewBag.row;
    int column = ViewBag.column;
    int rowTwo = ViewBag.rowTwo;
    int columnTwo = ViewBag.columnTwo;

    TempData["NumRow"] = row;
    TempData["NumColumn"] = column;
    TempData["NumRowTwo"] = rowTwo;
    TempData["NumColumnTwo"] = columnTwo;
    TempData["fromDb"] = false;

#line default
#line hidden
            BeginContext(360, 181, true);
            WriteLiteral("\r\n<table class=\"table-center\">\r\n    <tbody>\r\n        <tr>\r\n            <td align=\"center\">\r\n                <b>Матрица №1</b>\r\n                <table>\r\n                    <tbody>\r\n");
            EndContext();
#line 22 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
                         for (int i = 0; i < row; i++)
                        {

#line default
#line hidden
            BeginContext(624, 70, true);
            WriteLiteral("                        <tr>\r\n                            <td> </td>\r\n");
            EndContext();
#line 26 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
                             for (int j = 0; j < column; j++)
                                {

#line default
#line hidden
            BeginContext(792, 71, true);
            WriteLiteral("                            <td><input class=\"form-control\" type=\"text\"");
            EndContext();
            BeginWriteAttribute("name", " name=\"", 863, "\"", 893, 3);
            WriteAttributeValue("", 870, "matrixOne", 870, 9, true);
#line 28 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
WriteAttributeValue("", 879, i+1, 879, 6, false);

#line default
#line hidden
#line 28 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
WriteAttributeValue("", 885, j + 1, 885, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(894, 18, true);
            WriteLiteral(" value=\"0\"></td>\r\n");
            EndContext();
#line 29 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
                                }

#line default
#line hidden
            BeginContext(947, 31, true);
            WriteLiteral("                        </tr>\r\n");
            EndContext();
#line 31 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
                        }

#line default
#line hidden
            BeginContext(1005, 293, true);
            WriteLiteral(@"                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<table class=""table-center"">
    <tbody>
        <tr>
            <td align=""center"">
                <b>Матрица №2</b>
                <table>
                    <tbody>
");
            EndContext();
#line 45 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
                         for (int i = 0; i < rowTwo; i++)
                        {

#line default
#line hidden
            BeginContext(1384, 70, true);
            WriteLiteral("                        <tr>\r\n                            <td> </td>\r\n");
            EndContext();
#line 49 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
                             for (int j = 0; j < columnTwo; j++)
                                {

#line default
#line hidden
            BeginContext(1555, 71, true);
            WriteLiteral("                            <td><input class=\"form-control\" type=\"text\"");
            EndContext();
            BeginWriteAttribute("name", " name=\"", 1626, "\"", 1656, 3);
            WriteAttributeValue("", 1633, "matrixTwo", 1633, 9, true);
#line 51 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
WriteAttributeValue("", 1642, i+1, 1642, 6, false);

#line default
#line hidden
#line 51 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
WriteAttributeValue("", 1648, j + 1, 1648, 8, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1657, 18, true);
            WriteLiteral(" value=\"0\"></td>\r\n");
            EndContext();
#line 52 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
                                }

#line default
#line hidden
            BeginContext(1710, 31, true);
            WriteLiteral("                        </tr>\r\n");
            EndContext();
#line 54 "D:\Projects\StudLab\StudLab\Views\MatrixOperations\GetTable.cshtml"
                        }

#line default
#line hidden
            BeginContext(1768, 112, true);
            WriteLiteral("                    </tbody>\r\n                </table>\r\n            </td>\r\n        </tr>\r\n    </tbody>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
