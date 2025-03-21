// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hybrid;

namespace Microsoft.AspNetCore.Builder;
public static class WebViewExtensions
{

    public static WebApplication UseWebView(this WebApplication builder, Action<IApplicationBuilder> configureWebview)
    {
        return builder.UseWebView("", configureWebview);
    }

    public static WebApplication UseWebView(this WebApplication builder, string webviewName, Action<IApplicationBuilder> configureWebview)
    {
        builder.UseWhen(context =>
        {
            var webViewFeature = context.Features.Get<IWebViewFeature>();
            if (webViewFeature is null) { return false; }

            var origin = webViewFeature?.Origin ?? "";

            if (context.Request.Host.Host != origin) { return false; }
            if (string.IsNullOrEmpty(webviewName))  { return true; }

            if (webViewFeature!.WebViewName == webviewName)
            {
                return true;
            }

            return false;

        }
    , configureWebview);

        return builder;
    }



}
