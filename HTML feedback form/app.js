﻿$(function() {

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

     //function refreshTodoItems() {

	 //    var query = todoItemTable;

	 //    query.read().then(function(todoItems) {
	 //   	 listItems = $.map(todoItems, function(item) {
	 //   		 return $('<li>')
	 //   			 .attr('data-todoitem-id', item.id)
	 //   			 .append($('<button class="item-delete">Delete</button>'))
	 //   			 .append($('<input type="checkbox" class="item-complete">').prop('checked', item.complete))
	 //   			 .append($('<div>').append($('<input class="item-text">').val(item.text)));
	 //   	 });
				   
	 //   	 $('#todo-items').empty().append(listItems).toggle(listItems.length > 0);
	 //   	 $('#summary').html('<strong>' + todoItems.length + '</strong> item(s)');
	 //    });
	 //}

	
    //function getTodoItemId(formElement) {
    //    return Number($(formElement).closest('li').attr('data-todoitem-id'));
    //}

	// Handle inserts.
    
     //$('#add-item').submit(function(evt) {
     //    var textbox = $('#new-item-text'),
     //        itemText = textbox.val();
     //    if (itemText !== '') {
     //        todoItemTable.insert({ text: itemText, complete: false }).then(refreshTodoItems);
     //    }
     //    textbox.val('').focus();
     //    evt.preventDefault();
     //});

    // Handle updates.
	
	 //$(document.body).on('change', '.item-text', function() {
	 //   	 var newText = $(this).val();
	 //   	 todoItemTable.update({ id: getTodoItemId(this), text: newText });
	 //    });

	 //    $(document.body).on('change', '.item-complete', function() {
	 //   	 var isComplete = $(this).prop('checked');
	 //   	 todoItemTable.update({ id: getTodoItemId(this), complete: isComplete }).then(refreshTodoItems);
	 //    });


    // Handle deletes.
	
	 
	 //$(document.body).on('click', '.item-delete', function () {
     //    todoItemTable.del({ id: getTodoItemId(this) }).then(refreshTodoItems);
     //});
	

    // On initial load, start by fetching the current data
   // refreshTodoItems();
});