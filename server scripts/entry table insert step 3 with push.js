var SendGrid = require('sendgrid').SendGrid;


function insert(item, user, request) {

    //set a created date on the server
    item.createdDate = new Date();
    request.execute({
        success: function () {

            // After the record has been inserted, send the response immediately to the client
            request.respond();

            // Send the email in the background
            sendEmail(item);

            //push notification

            sendNotification(item);

        }
    });
}

function sendEmail(item) {
    var sendgrid = new SendGrid('azure_961495bd9b8317462490ad1ff672c3b7@azure.com', 'wi9gvrwz');

    sendgrid.send({
        to: item.Email,
        from: 'notifications@irishazureheads-mobile-service.azure-mobile.net',
        subject: 'Thanks ' + item.Name + ' for your entry!',
        text: 'Thanks for entering the competition ! You are now in the draw to win some cool prizes at the end of this talk so stay where you are !'
    }, function (success, message) {
        // If the email failed to send, log it as an error so we can investigate
        if (!success) {
            console.error(message);
        }
    });
}

function sendNotification(item) {
    var channelTable = tables.getTable('Channel');
    channelTable.read({
        success: function (channels) {
            channels.forEach(function (channel) {
                push.wns.sendToastImageAndText04(channel.uri, {

                    image1src: 'http://api.twitter.com/1/users/profile_image/' + item.Twitter,

                    image1alt: item.Twitter,
                    text1: "A new message ",
                    text2: "from " + item.Name,
                    text3: item.Message
                }, {
                    success: function (pushResponse) {
                        console.log("Sending push notification to : " + item.Name, pushResponse);
                    }
                });
            });
        }
    });
}
