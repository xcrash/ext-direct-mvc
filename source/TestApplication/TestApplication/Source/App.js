Ext.onReady(function() {
    Ext.BLANK_IMAGE_URL = 'http://extjs.cachefly.net/ext-3.0.3/resources/images/default/s.gif';
    Ext.Direct.addProvider(Ext.app.REMOTING_API);
    Ext.QuickTips.init();
    
    new Ext.Container({
        renderTo: document.body,
        height: 300,
        layout: {
            type: 'hbox',
            padding: '5',
            align: 'stretch',
            defaultMargins: {top:5,right:5,bottom:0,left:5}
        },
        items: [{
            xtype: 'simpletestpanel',
            width: 510
        }, {
            xtype: 'formtestpanel',
            width: 310
        }, {
            xtype: 'fileuploadpanel',
            width: 310
        }]
    });
    
    new Ext.Container({
        renderTo: document.body,
        height: 300,
        layout: {
            type: 'hbox',
            padding: '5',
            align: 'stretch',
            defaultMargins: {top:0,right:5,bottom:5,left:5}
        },
        items: [{
            xtype: 'treetestpanel',
            width: 350
        }, {
            xtype: 'employeegrid',
            width: 790
        }]
    });
    
    Ext.Direct.on('exception', function(e) {
        // Default to a user friendly message.
        var message = 'Error occured. Unable to process request.';
        if (e.where) {
            // If in debug mode, display the real error message and stack trace.
            message = String.format('{0}<br/><br/>The exception was thrown from {1}.{2}()<br/><br/><b>Stack Trace:</b><br/>{3}', e.message, e.action, e.method, e.where);
        }
        Ext.Msg.alert('Exception', message);
    });
});