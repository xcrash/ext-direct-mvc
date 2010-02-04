Ext.onReady(function() {
    Ext.BLANK_IMAGE_URL = 'http://extjs.cachefly.net/ext-3.1.0/resources/images/default/s.gif';
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
        var title = 'Direct Exception', message;
        
        if (Ext.isDefined(e.where)) {
            // Detailed error message for developer
            message = String.format('<b>{0}</b><p>The exception was thrown from {1}.{2}()</p><pre>{3}</pre>', Ext.util.Format.nl2br(e.message), e.action, e.method, e.where);
            var w = new Ext.Window({
                title: title,
                width: 600,
                height: 400,
                modal: true,
                layout: 'fit',
                border: false,
                maximizable: true,
                items: {
                    html: message,
                    autoScroll: true,
                    preventBodyReset: true,
                    bodyStyle: 'font-size:12px',
                    padding: 5
                },
                buttons: [{
                    text: 'OK',
                    handler: function() {
                        w.close();
                    }
                }],
                buttonAlign: 'center',
                defaultButton: 0
            }).show();                            
        } else {
            // User friendly message for end user
            message = 'Error occured. Unable to process request.';
            Ext.Msg.alert(title, message);
        }
    });
});