$(function() {

     var MobileServiceClient = WindowsAzure.MobileServiceClient,
		 client = new MobileServiceClient('https://corkug.azure-mobile.net/', 'NYUTJeqEKdbitDyuDAzrDbtmcMZLzs78'),
         todoItemTable = client.getTable('feedback1');

     $("#contact-submit").click(function () {
    
         var fullName = $('#fullName').val();
         var email = $('#email').val();
         var twitter = $('#twitter').val();
         var message = $('#message').val();

         todoItemTable.insert({ Name : fullName, Email :email, Twitter : twitter,Message : message}).then(alert('phew!'));
            
     }
      );

});