Ext.ux.EmployeeGrid = Ext.extend(Ext.grid.GridPanel, {
    title: 'Employees',
    pageSize: 10,
    frame: true,
    viewConfig: {
        autoFill: true
    },
    stripeRows: true,
    
    initComponent: function() {
        var ds = new Ext.data.DirectStore({
            directFn: Employees.Get,
            paramsAsHash: false,
            paramOrder: 'start|limit|sort|dir',
            root: 'data',
            idProperty: 'Id',
            totalProperty: 'total',
            sortInfo: {
                field: 'Name',
                direction: 'ASC'
            },
            fields: [
                {name: 'Id', type: 'int'},
                {name: 'Name', type: 'string'},
                {name: 'Email', type: 'string'},
                {name: 'HireDate', type: 'date', dateFormat: 'c'},
                {name: 'Salary', type: 'float'},
                {name: 'Active', type: 'boolean'}
            ],
            remoteSort: true
        });
        
        var pager = new Ext.PagingToolbar({
            store: ds,
            displayInfo: true,
            pageSize: this.pageSize
        });
        
        var config = {
            store: ds,
            bbar: pager,
            columns: [
                {header: 'Name', dataIndex: 'Name', sortable: true},
                {header: 'Email', dataIndex: 'Email', width: 150, sortable: true},
                {header: 'Hire Date', dataIndex: 'HireDate', width: 50, xtype: 'datecolumn', format: 'm/d/Y', sortable: true},
                {header: 'Salary', dataIndex: 'Salary', width: 50, renderer:'usMoney', align: 'right', sortable: true},
                {header: 'Active', dataIndex: 'Active', width: 50, xtype: 'booleancolumn', trueText: 'Yes', falseText: 'No', align: 'center'}
            ]
        };
        
        Ext.apply(this, Ext.apply(this.initialConfig, config));
        
        Ext.ux.EmployeeGrid.superclass.initComponent.apply(this, arguments);
    },
    
    afterRender: function() {
        this.getStore().load({
            params: {
                start: 0,
                limit: this.pageSize
            }
        });
    
        Ext.ux.EmployeeGrid.superclass.afterRender.apply(this, arguments);
    }
});

Ext.reg('employeegrid', Ext.ux.EmployeeGrid);