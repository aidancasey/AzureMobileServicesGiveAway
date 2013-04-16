var updatesTable = tables.getTable('Updates');
var request = require('request');

function GetTweets() {   
    // Check what is the last tweet we stored when the job last ran
    // and ask Twitter to only give us more recent tweets
    
    // 'http://search.twitter.com/search.json?q=%23Neo4J&rpp=100&result_type=mixed'
    appendLastTweetId(
        'http://search.twitter.com/search.json?q=%23windowsazure&result_type=recent',
        function twitterUrlReady(url){
            request(url, function tweetsLoaded (error, response, body) {
                if (!error && response.statusCode == 200) {
                    var results = JSON.parse(body).results;
                    if(results){
                        console.log('Fetched new results from Twitter');
                        results.forEach(function visitResult(tweet){
                            if(!filterOutTweet(tweet)){
                                var update = {
                                    twitterId: tweet.id,
                                    text: tweet.text,
                                    author: tweet.from_user,
                                    date: tweet.created_at
                                };
                                updatesTable.insert(update);
                            }
                        });
                    }            
                } else { 
                    console.error('Could not contact Twitter');
                }
            });


    });
 }

// Find the largest (most recent) tweet ID we have already stored
// (if we have stored any) and ask Twitter to only return more
// recent ones
function appendLastTweetId(url, callback){
    updatesTable
    .orderByDescending('twitterId')
    .read({success: function readUpdates(updates){
        if(updates.length){
            callback(url + '&since_id=' + (updates[0].twitterId + 1));
        } else {
            callback(url);
        }
    }});
}

function filterOutTweet(tweet){
    // Remove retweets and replies
   // return (tweet.text.indexOf('RT') === 0 || tweet.to_user_id);
}