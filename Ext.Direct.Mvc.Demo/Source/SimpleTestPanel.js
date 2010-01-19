Ext.ux.SimpleTestPanel = Ext.extend(Ext.Panel, {
    title: 'Simple Test Panel',
    autoScroll: true,
    frame: true,
    padding: 5,
    bodyStyle: {
        backgroundColor: '#ffffff',
        border: 'solid 1px #99BBE8'
    },
    buttonAlign: 'center',
        
    initComponent: function() {
        var config = {
            buttons: [{
                text: 'Say Hello',
                handler: this.onSayHello,
                scope: this
            }, {
                text: 'Echo Date',
                handler: this.onEchoDate,
                scope: this
            }, {
                text: 'Add Numbers',
                handler: this.onAddNumbers,
                scope: this
            }, {
                text: 'Echo Person',
                handler: this.onEchoPerson,
                scope: this
            }, {
                text: 'Batch',
                handler: this.onBatch,
                scope: this
            }, {
                text: 'Exception',
                handler: this.onException,
                scope: this
            }],

            tools: [{
                id: 'refresh',
                qtip: 'Clear content',
                handler: function() {
                    this.body.update('');
                },
                scope: this
            }]
        };
        
        Ext.apply(this, Ext.apply(this.initialConfig, config));
        
        Ext.ux.SimpleTestPanel.superclass.initComponent.call(this);
    },
    
    onSayHello: function() {
        Test.SayHello(function(result, response) {
            this.updateBody(result);
        }, this);
    },
    
    onEchoDate: function() {
        var date = new Date();
        Test.EchoDate(date, function(result, response) {
            this.updateBody(Date.parseDate(result, 'c').format('j-M-Y, g:i a'));
        }, this);
    },
    
    onAddNumbers: function() {
        var a = 5, b = 10;
        Test.AddNumbers(a, b, function(result, response) {
            this.updateBody(String.format('{0} + {1} = {2}', a, b, result));
        }, this);
    },
    
    onEchoPerson: function() {
        var person = {
            Name: 'John Smith',
            Email: 'john.smith@example.com',
            Gender: 'Male',
            Birthday: new Date('12/31/1969'),
            Salary: 55000
        };
        Test.EchoPerson(person, function(result, response) {
            var tpl = new Ext.Template(
                'Name: {Name}<br/>',
                'Email: {Email}<br/>',
                'Gender: {Gender}<br/>',
                'Birthday: {Birthday:date(m/d/Y)}<br/>',
                'Salary: {Salary:usMoney}'
            );
                    
            this.updateBody(tpl.apply(result));
        }, this);
    },
    
    onBatch: function() {
        this.onSayHello();
        this.onEchoDate();
        this.onAddNumbers();
        this.onEchoPerson();
    },
    
    onException: function() {
        Test.TestException();
    },
    
    updateBody: function(text) {
        if (this.body.dom.innerHTML.length > 0)
            this.body.dom.innerHTML += '<hr size="1">';
        this.body.dom.innerHTML += String.format('<div>{0}</div>', text);
        
        this.body.scroll('down', 500, true);
    }
});

Ext.reg('simpletestpanel', Ext.ux.SimpleTestPanel);