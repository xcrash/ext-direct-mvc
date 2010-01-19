Ext.ux.FileUploadPanel = Ext.extend(Ext.form.FormPanel, {
    title: 'File Upload Panel',
    frame: true,
    padding: 5,
    bodyStyle: {
        backgroundColor: '#ffffff',
        border: 'solid 1px #99BBE8'
    },
    labelWidth: 80,
    defaults: {
        anchor: '0'
    },
    
    initComponent: function() {
        var config = {
            api: {
                submit: Test.UploadFiles
            },
            
            fileUpload: true,
            
            items: [{
                xtype: 'textfield',
                name: 'firstName',
                fieldLabel: 'First Name'
            }, {
                xtype: 'textfield',
                name: 'lastName',
                fieldLabel: 'Last Name'
            }, {
                xtype: 'fileuploadfield',
                fieldLabel: 'File #1'
            }, {
                xtype: 'fileuploadfield',
                fieldLabel: 'File #2'
            }, {
                xtype: 'fileuploadfield',
                fieldLabel: 'File #3'
            }],
            
            buttons: [{
                text: 'Submit',
                handler: function() {
                    this.getForm().submit({
                        success: function(form, action) {
                            Ext.Msg.alert('File Upload Panel', 'Files have been successfully uploaded.<br/>You can find them in Uploaded folder of this application.');
                        }
                    });
                },
                scope: this
            }]
        };
        Ext.apply(this, Ext.apply(this.initialConfig, config));
        
        Ext.ux.FileUploadPanel.superclass.initComponent.call(this);
    }
});

Ext.reg('fileuploadpanel', Ext.ux.FileUploadPanel);