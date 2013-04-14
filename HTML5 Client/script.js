$(function() {

     var MobileServiceClient = WindowsAzure.MobileServiceClient,
		 client = new MobileServiceClient('https://irishazureheads.azure-mobile.net/', 'vtGmfCuERKZUykSqHaUcMqXGXlWLbO76'),
         todoItemTable = client.getTable('Entry');

     $("#contact-submit").click(function (evt) {
    
         var fullName = $('#fullName').val();
         var email = $('#email').val();
         var twitter = $('#twitter').val();
         var message = $('#message').val();

         todoItemTable.insert({ Name: fullName, Email: email, Twitter: twitter, Message: message }).then(alert('phew!'));
       //  todoItemTable.insert({ Name: fullName }).then(alert('right this should work !!'));
         evt.preventDefault();
            
     }
      );

});