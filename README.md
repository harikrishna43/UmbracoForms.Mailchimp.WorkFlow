# UmbracoForm.Mailchimp.WorkFlow

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