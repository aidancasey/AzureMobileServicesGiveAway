var SendGrid = require('sendgrid').SendGrid;


function insert(item, user, request) {
	request.execute({
		success: function () {
			// After the record has been inserted, send the response immediately to the client
			request.respond();
			// Send the email in the background
			sendEmail(item);
            //send push notification
			sendNotifications();
			sendNotifications1();
			sendNotifications2();
		}
	});


	function sendEmail(item) {
		var sendgrid = new SendGrid('azure_961495bd9b8317462490ad1ff672c3b7@azure.com', 'wi9gvrwz');


		sendgrid.send({
			to: item.Email,
			from: 'notifications@corkug-mobile-service.azure-mobile.net',
			subject: 'Thanks ' + item.Name + ' for your feedback!',
			text: 'Thanks for sending your feedback ! You are now in the draw to win some prizes at the end of this talk so stay where you are !'
		}, function (success, message) {
			// If the email failed to send, log it as an error so we can investigate
			if (!success) {
				console.error(message);
			}
		});
	}

	function sendNotifications() {
	    var channelTable = tables.getTable('Channel');
	    channelTable.read({
	        success: function (channels) {
	            channels.forEach(function (channel) {
	                push.wns.sendToastText04(channel.uri, {
	                    text1: item.Name
	                }, {
	                    success: function (pushResponse) {
	                        console.log("Sent push:", pushResponse);
	                    }
	                });
	            });
	        }
	    });
	}



	function sendNotifications1() {
	    var channelTable = tables.getTable('Channel');
	    channelTable.read({
	        success: function (channels) {
	            channels.forEach(function (channel) {
	                push.wns.sendToastImageAndText04(channel.uri, {

	                    image1src : 'http://api.twitter.com/1/users/profile_image/' + item.Twitter,
	                   
	                    image1alt: 'My pic',
	                    text1: "A record inserted",
	                    text2: "from " + item.Name,
	                    text3: item.Message
	                }, {
	                    success: function (pushResponse) {
	                        console.log("Sent push:", pushResponse);
	                    }
	                });
	            });
	        }
	    });
	}

	function sendNotifications2() {
	    var channelTable = tables.getTable('Channel');
	    channelTable.read({
	        success: function (channels) {
	            channels.forEach(function (channel) {
	                push.wns.sendTileWideSmallImageAndText04(channel.uri, {

	                    image1src: 'http://api.twitter.com/1/users/profile_image/' + item.Twitter,

	                    image1alt: 'My pic',
	                    text1: "A record inserted",
	                    text2: "from " + item.Name,
	                    text3: item.Message
	                }, {
	                    success: function (pushResponse) {
	                        console.log("Sent push:", pushResponse);
	                    }
	                });
	            });
	        }
	    });
	}

}