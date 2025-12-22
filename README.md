**Umbraco Forms: Mailchimp Workflow**
=======================

[![Downloads](https://img.shields.io/nuget/dt/UmbracoForm.MailChimp.WorkFlow?color=cc9900)](https://www.nuget.org/packages/UmbracoForm.MailChimp.WorkFlow/)
[![NuGet](https://img.shields.io/nuget/vpre/UmbracoForm.MailChimp.WorkFlow?color=0273B3)](https://www.nuget.org/packages/UmbracoForm.MailChimp.WorkFlow)
[![Release Build](https://img.shields.io/github/actions/workflow/status/harikrishna43/UmbracoForm.MailChimp.WorkFlow/dotnet.yml?label=Release%20Build)](https://github.com/harikrishna43/UmbracoForm.MailChimp.WorkFlow/actions)
[![License](https://img.shields.io/github/license/harikrishna43/UmbracoForm.MailChimp.WorkFlow?label=License)](https://github.com/harikrishna43/UmbracoForm.MailChimp.WorkFlow/blob/main/LICENSE)
[![GitHub Stars](https://img.shields.io/github/stars/harikrishna43/UmbracoForm.MailChimp.WorkFlow?label=Stars)](https://github.com/harikrishna43/UmbracoForm.MailChimp.WorkFlow/stargazers)

# UmbracoForm Mailchimp WorkFlow 17.0.0.1
This package is for Umbraco CMS v17.x.x and umbracoforms v17.x.x
[nuget](https://www.nuget.org/packages/UmbracoForm.MailChimp.WorkFlow)
```sh
dotnet add package UmbracoForm.MailChimp.WorkFlow --version 17.0.0.2
```

# UmbracoForm Mailchimp WorkFlow 4.0.0
This package is for Umbraco 9.0.1 and umbracoforms 9.0.1
https://www.nuget.org/packages/Our.Umbraco.Forms.MailChimpWorkflow/
```sh
dotnet add package Our.Umbraco.Forms.MailChimpWorkflow --version 4.0.0
```
# UmbracoForm Mailchimp WorkFlow 3.0.0
This package is for Umbraco 8.4.1 and umbracoforms 8.3.0 
https://www.nuget.org/packages/UmbracoForm.MailChimp.WorkFlow/
```sh
Install-Package UmbracoForm.MailChimp.WorkFlow -Version 3.0.0
```
# UmbracoForm.Mailchimp.WorkFlow 2.1.1

This package is help to manage Umbraco form with Mailchimp.

https://www.nuget.org/packages/UmbracoForm.MailChimp.WorkFlow/

A custom Workflow for Umbraco Forms that connect with MailChimp when a Form is submitted. Mailchimp properties are set based on provided field values.

IMPORTANT: Do not install this package without Umbraco Forms installed. 
This package relies on a few namespaces provided by Umbraco Forms.

Dependency :
UmbracoCMS version 7.6.x
UmbracoFroms 7.0.1

Update Version 1.0.0:
Latest update available : 1.1.0
fetures:
- Specify fieldname aliases as Forms connect mailchimp field.
- user can manage different form with different mailchimp list and account.

-Resolved bugs and make it  more user friendly.

# Updated Package 04-FEB-2020 ---
Updated 
  - umbracoCMS 7.15.3
  - Umbracoform 7.3.0
  - MailChimp.V3 4.1.1
  ```sh
Install-Package UmbracoForm.MailChimp.WorkFlow -Version 2.1.1
```
# Updated Package 07-SEP-2020 ---
Fixed issues
-Culture is not supported.
-Parameter name: name
-in is an invalid culture identifier.
  ```sh
Install-Package UmbracoForm.MailChimp.WorkFlow -Version 3.0.2
```

## Developer Notes

To generate a new nuget package

- Increase version in `UFMailchimpWorkFlowType/UFMailchimpWorkFlowType.csproj.nuspec`;
- Check dependencies list & update it if needed. `nuget` doesn't automatically generate the dependencies list
- Run command in Package Console Management
```sh
nuget pack UFMailchimpWorkFlowType/UFMailchimpWorkFlowType.csproj.nuspec
```

** Notes **
This project seems using the old format, that's why nuget package doesn't work really well
https://docs.microsoft.com/en-us/nuget/resources/check-project-format
