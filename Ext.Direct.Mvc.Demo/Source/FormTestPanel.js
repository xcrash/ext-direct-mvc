Ext.ux.FormTestPanel = Ext.extend(Ext.form.FormPanel, {
    title: 'Form Test Panel',
    frame: true,
    padding: 5,
    bodyStyle: {
        backgroundColor: '#ffffff',
        border: 'solid 1px #99BBE8'
    },
    labelWidth: 80,
    
    initComponent: function() {
        var config = {
            api: {
                load: Test.LoadPerson,
                submit: Test.SavePerson
            },
    
            items: [{
                name: 'Id',
                xtype: 'hidden',
                value: 100
            }, {
                name: 'Name',
                xtype: 'textfield',
                fieldLabel: 'Name',
                anchor: '0'
            }, {
                name: 'Email',
                xtype: 'textfield',
                fieldLabel: 'Email',
                anchor: '0'
            }, {
                name: 'Gender',
                xtype: 'combo',
                fieldLabel: 'Gender',
                store: [[1,'Male'], [2,'Female']],
                mode: 'local',
                triggerAction: 'all',
                editable: false,
                width: 100
            }, {
                name: 'Birthday',
                xtype: 'datefield',
                fieldLabel: 'Birthday',
                altFormats: 'c',
                width: 100
            }, {
                name: 'Salary',
                xtype: 'numberfield',
                fieldLabel: 'Salary',
                decimalPrecision: 2,
                width: 100
            }],
        
            buttons: [{
                text: 'Load',
                handler: function () {
                    this.getForm().load();
                },
                scope: this
            }, {
                text: 'Submit',
                handler: function () {
                    this.getForm().submit({
                        success: function(form, action) {
                            var person = action.result.data;
                            var tpl = new Ext.XTemplate(
                                'The following data was successfully submitted:<br/><br/>',
                                '* Name: {Name}<br/>',
                                '* Email: {Email}<br/>',
                                '* Gender: {Gender}<br/>',
                                '* Birthday: {[Date.parseDate(values.Birthday,"c").format("m/d/Y")]}<br/>',
                                '* Salary: {Salary:usMoney}'
                            );
                            var html = tpl.apply(person);
                            Ext.Msg.alert('Form Test Panel', html);
                        }
                    });
                },
                scope: this
            }],
            
            tools: [{
                id: 'refresh',
                qtip: 'Reset the form',
                handler: function() {
                    this.getForm().reset();
                },
                scope: this
            }]
        };
        
        Ext.apply(this, Ext.apply(this.initialConfig, config));
    
        Ext.ux.FormTestPanel.superclass.initComponent.call(this);
    }
});

Ext.reg('formtestpanel', Ext.ux.FormTestPanel);