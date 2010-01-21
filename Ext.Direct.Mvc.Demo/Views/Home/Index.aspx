<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<html>
<head runat="server">
    <title>Ext.Direct.Mvc Test Application</title>
    
    <link rel="Stylesheet" type="text/css" href="http://extjs.cachefly.net/ext-3.1.0/resources/css/ext-all.css" />
    <script type="text/javascript" src="http://extjs.cachefly.net/ext-3.1.0/adapter/ext/ext-base.js"></script>
    <script type="text/javascript" src="http://extjs.cachefly.net/ext-3.1.0/ext-all.js"></script>
    
    <link rel="Stylesheet" type="text/css" href="http://extjs.cachefly.net/ext-3.1.0/examples/ux/fileuploadfield/css/fileuploadfield.css" />
    <script type="text/javascript" src="http://extjs.cachefly.net/ext-3.1.0/examples/ux/fileuploadfield/FileUploadField.js"></script>
    
    <script type="text/javascript" src="<% = Url.Content("~/Source/App.js") %>"></script>
    <script type="text/javascript" src="<% = Url.Content("~/Source/SimpleTestPanel.js") %>"></script>
    <script type="text/javascript" src="<% = Url.Content("~/Source/FormTestPanel.js") %>"></script>
    <script type="text/javascript" src="<% = Url.Content("~/Source/FileUploadPanel.js") %>"></script>
    <script type="text/javascript" src="<% = Url.Content("~/Source/TreeTestPanel.js") %>"></script>
    <script type="text/javascript" src="<% = Url.Content("~/Source/EmployeeGrid.js") %>"></script>
    
    <!-- Direct API url is configured in web.config -->
    <script type="text/javascript" src="<% = Url.Content("~/Direct/Api") %>"></script>
</head>
<body>
</body>
</html>