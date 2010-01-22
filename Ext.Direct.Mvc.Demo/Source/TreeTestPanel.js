Ext.ux.TreeTestPanel = Ext.extend(Ext.tree.TreePanel, {
    title: 'Tree Test Panel',
    autoScroll: true,
    frame: true,
    padding: 5,
    bodyStyle: {
        backgroundColor: '#ffffff',
        border: 'solid 1px #99BBE8'
    },
    
    initComponent: function() {
        var config = {
            loader: new Ext.tree.TreeLoader({
                directFn: Test.GetTree
            }),
        
            root: {
                id: 'root',
                text: 'Root'
            },
            
            buttons: [{
                text: 'Reload Tree',
                handler: function() {
                    this.getRootNode().reload();
                },
                scope: this
            }]
        };
        
        Ext.apply(this, Ext.apply(this.initialConfig, config));
        
        Ext.ux.TreeTestPanel.superclass.initComponent.call(this);
    }
});

Ext.reg('treetestpanel', Ext.ux.TreeTestPanel);